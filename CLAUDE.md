# MixxFit.Admin

Windows desktop administration client for the MixxFit platform. WinForms on .NET 10.

This app exists so administration does not have to live in the Angular app. It is used by a
single operator (the project owner) to inspect users, moderate accounts, and maintain the
system exercise catalog. It is not distributed to end users.

## Tech stack

- .NET 10, `net10.0-windows`, `<UseWindowsForms>true</UseWindowsForms>`
- WinForms (Designer-based, not hand-written layout)
- `System.Net.Http` + `System.Net.Http.Json` for API access
- `System.Text.Json` for serialization

Windows-only by design. Do not add cross-platform shims.

## Related repositories

| Repo                 | Role                                                             |
| -------------------- | ---------------------------------------------------------------- |
| `Miks02/mixxfit-api` | .NET 10 Web API + PostgreSQL. The only data source for this app. |
| `Miks02/mixxfit-web` | Angular 21 end-user client. Unrelated to this app.               |

This app talks to the API over HTTP only. It has no database access and no project reference
to either repo — see "Hard rules".

## Architecture

Organised **by feature**, mirroring the API's vertical slice layout. Adding a screen means
adding a folder; it should not require touching existing ones.

```
MixxFit.Admin/
├── Api/
│   ├── MixxFitApiClient.cs          # HttpClient, auth, refresh, send pipeline
│   └── ApiException.cs              # the single exception type for API failures
├── Common/
│   ├── PagedResult.cs               # mirrors the API's PagedResult<T>
│   ├── ProblemDetails.cs
│   ├── IAdminView.cs
│   ├── ViewBase.cs                  # RunAsync helper
│   └── Errors/                      # mirrors the API's Domain/ErrorCatalog
│       ├── ErrorCatalog.cs          # code -> message lookup
│       ├── AuthError.cs
│       ├── UserError.cs
│       └── GeneralError.cs
├── Features/
│   ├── Dashboard/
│   │   └── DashboardView.cs
│   ├── Users/
│   │   ├── UsersView.cs
│   │   ├── UserDto.cs
│   │   └── MixxFitApiClient.Users.cs
│   └── Exercises/
│       ├── ExercisesView.cs
│       ├── ExerciseDto.cs
│       ├── ExerciseEditForm.cs
│       └── MixxFitApiClient.Exercises.cs
├── MainForm.cs
├── Program.cs
└── appsettings.json
```

`MixxFitApiClient` is a **`partial class`**. Its core (HttpClient, token, refresh, send
pipeline) lives in `Api/`; per-feature request methods live in that feature's folder as
`MixxFitApiClient.<Feature>.cs`. One client, one auth path, feature-local surface.

### Why not MVP

MVP is the textbook WinForms pattern, and it is deliberately **not** used here. Its payoff is
testable UI logic; this tool has three screens of "fetch list, show it, post a change" and no
UI tests. The cost — an interface and a presenter per screen — buys nothing. Thin code-behind
with `MixxFitApiClient` as the only boundary is the intended design. Do not introduce
presenters, and do not introduce MVVM (that is WPF's pattern; WinForms binding lacks commands
and a real DataContext).

### Navigation

`MainForm` holds a content `Panel`. Each screen is a `UserControl` docked into it. There is no
routing framework and none is needed.

```csharp
private async Task ShowViewAsync(UserControl view)
{
    pnlContent.Controls.Clear();
    view.Dock = DockStyle.Fill;
    pnlContent.Controls.Add(view);

    if (view is IAdminView adminView)
        await adminView.LoadDataAsync();
}
```

Add/edit flows are separate modal `Form`s opened with `ShowDialog()`, exposing their result
through a public property and setting `DialogResult.OK` on save.

### View lifecycle

Views implement `IAdminView` and load data through an explicit `LoadDataAsync()` call from the
host. Do not load in the `Load` event — for `UserControl` it fires at a point that is easy to
get wrong.

`UserControl`s must keep a **parameterless constructor** or the WinForms Designer breaks.
Inject the API client through an `Init(MixxFitApiClient api)` method, never the constructor.

## Error handling

### No `Result<T>` on the client

The API's `Result<T>` is deliberately **not** ported. It exists server-side to keep exceptions
out of control flow across many handlers. Here there is one shape of call site — a view asks
for data and either renders it or reports a failure — and a result wrapper would cost the same
lines as a try/catch while losing stack traces and making a swallowed failure easy to write.

### One exception type

`MixxFitApiClient` throws **`ApiException`** for any non-success response. There is no
exception type per error kind. It carries what the server actually sends:

```csharp
public class ApiException(int statusCode, string? errorCode, string? title) : Exception
{
    public int StatusCode { get; } = statusCode;
    public string? ErrorCode { get; } = errorCode;   // e.g. "Auth.InvalidCurrentPassword"
}
```

### Views never write try/catch

`ViewBase.RunAsync` wraps every call, so the boilerplate exists once:

```csharp
protected async Task RunAsync(Func<Task> action)
{
    try
    {
        Cursor = Cursors.WaitCursor;
        await action();
    }
    catch (ApiException ex)
    {
        MessageBox.Show(ErrorCatalog.Describe(ex), "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
    catch (HttpRequestException)
    {
        MessageBox.Show("API nije dostupan.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    finally { Cursor = Cursors.Default; }
}
```

Call sites stay clean:

```csharp
private async void btnNext_Click(object sender, EventArgs e)
{
    _page++;
    await RunAsync(LoadPageAsync);
}
```

### Why `Common/Errors` exists

The API's `ProblemDetails` **does not carry a human-readable message.** `ToProblemResult`
hardcodes `Detail` to `"An error occurred while processing your request."` and discards
`Error.Description`. What does reach the client is the code, as a top-level `errorCode`
property:

```json
{
  "title": "urn:mixxfit-api:error:Auth.InvalidCurrentPassword",
  "detail": "An error occurred while processing your request.",
  "status": 400,
  "errorCode": "Auth.InvalidCurrentPassword"
}
```

So the client owns its own message text. `Common/Errors/` mirrors the API's
`Domain/ErrorCatalog/` — same code strings, client-side wording — and `ErrorCatalog.Describe`
maps a code to what the operator sees, with a status-based fallback for unknown codes.

Keep the code strings **identical** to the API's. When a code is added or renamed server-side,
mirror it here.

If the API is ever changed to send `Detail = error.Description`, this catalog can shrink to
just the codes the client branches on programmatically.

## Shared contracts

DTOs and shared types are **hand-written copies** of the API's shapes. This duplication is
deliberate: the two projects deploy separately and must version independently, and a project
reference would drag ASP.NET Core dependencies into a desktop app. When a response shape
changes, update it here by hand.

`PagedResult<T>` mirrors the API's, with one required difference:

```csharp
public record PagedResult<T>(List<T> Items, int Page, int PageSize, int TotalCount, int PaginatedCount)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
```

`Items` **must** be `List<T>`, not `IReadOnlyList<T>` as on the server — `DataGridView.DataSource`
requires `IList`, and `IReadOnlyList<T>` does not implement it. The grid then binds to nothing
and reports no error.

## API

Base URLs — all endpoints sit under an `api/` route group on the server:

- Production: `https://api.getmixxfit.com/api/`
- Local dev: `https://localhost:7250/api/`

Store the base URL in `appsettings.json`, never hardcode it in a form.

### HttpClient rules

These are not style preferences; breaking them causes real bugs.

**One long-lived `HttpClient`** for the whole app, held by `MixxFitApiClient`. Never
`using var client = new HttpClient()` per call.

**Do NOT use `IHttpClientFactory`.** Authentication depends on a `CookieContainer` that must
survive between login and refresh. The factory rotates the primary handler periodically, which
silently discards the refresh-token cookie and logs the operator out.

```csharp
var handler = new HttpClientHandler { CookieContainer = new CookieContainer() };
_http = new HttpClient(handler) { BaseAddress = new Uri(baseUrl) };
```

**`BaseAddress` ends with `/`; relative URLs must NOT start with `/`.** A leading slash
discards the base path, so `"/auth/login"` resolves without the required `/api` segment.
Always `"auth/login"`.

**Shared JSON options, both settings required.** The API serializes camelCase and writes enums
as strings. Without both, deserialization silently yields nulls and defaults.

```csharp
private static readonly JsonSerializerOptions JsonOptions =
    new(JsonSerializerDefaults.Web) { Converters = { new JsonStringEnumConverter() } };
```

**A fresh `HttpRequestMessage` per attempt** — it cannot be reused, so the 401-retry path must
build a new one.

### Authentication

- `POST auth/login` returns the access token in the body. The refresh token is set as an
  HTTP-only cookie and is deliberately `[JsonIgnore]`-d out of the body.
- The access token goes on `DefaultRequestHeaders.Authorization` as `Bearer`.
- `POST auth/refresh-token` takes **no body** — the cookie travels via `CookieContainer`.
- On `401`, refresh once and retry the original request once. Do not loop.
- Cookies are in memory only, so the operator logs in on each app start. This is intended.

### Authorization

Admin endpoints require the `Admin` role, enforced server-side via
`RequireAuthorization(policy => policy.RequireRole("Admin"))`.

Being a desktop app provides **no** security. Never describe it as a security boundary in code
or docs. Handle `403` with a clear message rather than assuming it cannot happen.

## WinForms conventions

**`async void` only on event handlers.** An unhandled exception from `async void` terminates
the process, so every handler body goes through `RunAsync` (or its own try/catch). Real work
lives in `async Task` methods.

**Never `.Result` or `.Wait()`.** WinForms has a `SynchronizationContext`; blocking on a task
from the UI thread deadlocks the app permanently. Always `await`.

**Refreshing a grid is a plain `DataSource` assignment.** There is no change detection. After
a mutation, re-fetch and reassign rather than mutating the bound list in place.

**Paging is manual.** Keep the current page in a field, reload on change, and drive the
navigation buttons from `TotalPages`.

Disable the triggering button while a request is in flight.

## Packages

There is little to share with the backend — nearly everything this client needs
(`System.Net.Http.Json`, `System.Text.Json`) ships in the BCL. Do not add packages just to
mirror the API. Where one is genuinely needed, **match the API's version**.

| Package                                   | Version | Use                                                                                        |
| ----------------------------------------- | ------- | ------------------------------------------------------------------------------------------ |
| `FluentValidation`                        | 12.1.1  | Only if dialog validation outgrows plain checks. Same version as the API.                  |
| `Serilog` + `Serilog.Sinks.File`          | —       | Local log file. The API uses `Serilog.AspNetCore`; the sink differs, the style should not. |
| `Microsoft.Extensions.Configuration.Json` | —       | Reading `appsettings.json`.                                                                |

## Hard rules

- **No database access.** No `Microsoft.EntityFrameworkCore`, no `Npgsql`, no connection
  strings. Every read and write goes through the API — bypassing it would skip validation,
  authorization, and business rules that live in the API handlers.
- **No `Microsoft.AspNetCore.*` packages.** Server-side only.
- **No project reference to `MixxFit.API`.**
- **Never ignore `*.resx` or `*.Designer.cs` in git.** They are Designer output and the forms
  do not build without them.
- **No secrets in the repo.** `appsettings.json` holds the base URL and nothing else.
  Credentials are typed at login and never persisted.

## Build and run

```bash
dotnet build
dotnet run --project MixxFit.Admin
```

Requires Windows. Designer work requires Visual Studio; Rider's WinForms designer support is
limited.
