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

This app talks to the API over HTTP only. It has no database access and no shared code
with either repo — see "Hard rules" below.

## API

Base URLs (all endpoints are nested under an `api/` route group on the server):

- Production: `https://api.getmixxfit.com/api/`
- Local dev: `https://localhost:7250/api/`

Store the base URL in `appsettings.json`, never hardcode it in a form.

### HttpClient rules

These are not style preferences. Breaking them causes real bugs.

**One long-lived `HttpClient` for the whole app lifetime.**
Create it once, hold it in `MixxFitApiClient`, never `using var client = new HttpClient()`
per call.

**Do NOT use `IHttpClientFactory`.**
Authentication depends on a `CookieContainer` that must survive between login and token
refresh. `IHttpClientFactory` rotates the primary handler periodically, which silently
discards the refresh-token cookie and logs the operator out. A single client is the correct
choice here.

```csharp
var handler = new HttpClientHandler { CookieContainer = new CookieContainer() };
_http = new HttpClient(handler) { BaseAddress = new Uri(baseUrl) };
```

**`BaseAddress` must end with `/`, and relative URLs must NOT start with `/`.**
A leading slash discards the base path, so `"/auth/login"` resolves to
`https://api.getmixxfit.com/auth/login` and drops the required `/api` segment.
Always write `"auth/login"`, never `"/auth/login"`.

**JSON options are shared and must include both settings.**
The API serializes camelCase and writes enums as strings (`JsonStringEnumConverter` is
registered server-side). Without both, deserialization silently yields nulls and defaults.

```csharp
private static readonly JsonSerializerOptions JsonOptions =
    new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };
```

**A fresh `HttpRequestMessage` per attempt.**
`HttpRequestMessage` cannot be reused. The 401-retry path must construct a new one.

### Authentication

- `POST auth/login` returns an access token in the body. The refresh token is set as an
  HTTP-only cookie and is deliberately `[JsonIgnore]`-d out of the response body.
- The access token goes on `DefaultRequestHeaders.Authorization` as `Bearer`.
- `POST auth/refresh-token` takes **no body** — the cookie travels automatically via
  `CookieContainer`.
- On `401`, refresh once and retry the original request once. Do not loop.
- Cookies live in memory only, so the operator logs in on each app start. This is intended.

### Authorization

Admin endpoints require the `Admin` role, enforced server-side via
`RequireAuthorization(policy => policy.RequireRole("Admin"))`.

The fact that this is a desktop app provides **no** security. Never describe it as a
security boundary in code comments or documentation. Handle `403` with a clear message
rather than assuming it cannot happen.

### Errors

The API returns RFC 7807 `ProblemDetails` for business errors and field-level validation
errors. Parse it and surface `title`/`detail` to the operator instead of showing a raw
status code or a bare exception message.

## Architecture

```
MixxFit.Admin/
├── Api/
│   ├── MixxFitApiClient.cs      # the only place HttpClient is touched
│   ├── ApiException.cs          # wraps ProblemDetails responses
│   └── Dtos/                    # hand-written request/response models
├── Views/                       # one UserControl per screen
│   ├── DashboardView.cs
│   ├── UsersView.cs
│   └── ExercisesView.cs
├── Dialogs/                     # modal Forms
│   ├── LoginForm.cs
│   └── ExerciseEditForm.cs
├── Common/
│   └── IAdminView.cs
├── MainForm.cs                  # hosts and swaps views
├── Program.cs
└── appsettings.json
```

### Navigation

`MainForm` holds a content `Panel`. Each screen is a `UserControl` that is docked into that
panel. There is no routing framework and none is needed.

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

Views implement `IAdminView` and load data through an explicit `LoadDataAsync()` call from
the host. Do not load data in the `Load` event — for `UserControl` it fires at a point that
is easy to get wrong, and explicit is clearer.

`UserControl`s must keep a **parameterless constructor** or the WinForms Designer breaks.
Inject the API client through an `Init(MixxFitApiClient api)` method instead of the
constructor.

## WinForms conventions

**`async void` is permitted only on event handlers**, and every one must wrap its body in
try/catch — an unhandled exception from `async void` terminates the process. Real work goes
in `async Task` methods that the handler awaits.

**Never call `.Result` or `.Wait()`.** WinForms has a `SynchronizationContext`; blocking on
a task from the UI thread deadlocks the app permanently. Always `await`.

**Binding to `DataGridView` requires `IList`.** Declare collection properties on DTOs as
`List<T>`, not `IReadOnlyList<T>` — the server uses `IReadOnlyList<T>` in `PagedResult<T>`,
but that does not implement `IList` and the grid binds to nothing, with no error shown.

**Refreshing the grid is a plain `DataSource` assignment.** There is no change detection.
After any mutation, re-fetch and reassign rather than mutating the bound list in place.

**Paging is manual.** The API returns `PagedResult<T>` with `Page`, `PageSize`,
`TotalCount` and `TotalPages`. Keep the current page in a field, reload on change, and
enable/disable the navigation buttons from `TotalPages`.

Show progress with `Cursor = Cursors.WaitCursor` in a `try`/`finally`, and disable the
triggering button while a request is in flight.

## DTOs

DTOs are **hand-written copies** of the API's response shapes. This duplication is
deliberate.

API Repository link: https://github.com/MixxFit/mixxfit-api

## Packages

Realistically there is little to share with the backend — nearly everything this client
needs (`System.Net.Http.Json`, `System.Text.Json`) ships in the BCL. Do not add packages
just to mirror the API.

Where a package is genuinely needed, **match the API's version** so behavior is consistent:

| Package                                   | Version | Use                                                                                                                               |
| ----------------------------------------- | ------- | --------------------------------------------------------------------------------------------------------------------------------- |
| `FluentValidation`                        | 12.1.1  | Only if dialog input validation grows beyond trivial checks. Same version as the API.                                             |
| `Serilog` + `Serilog.Sinks.File`          | —       | Local log file for diagnosing failed requests. The API uses `Serilog.AspNetCore`; the sink differs, the logging style should not. |
| `Microsoft.Extensions.Configuration.Json` | —       | Reading `appsettings.json`.                                                                                                       |

## Hard rules

- **No database access.** No `Microsoft.EntityFrameworkCore`, no `Npgsql`, no connection
  strings. Every read and write goes through the API. Bypassing it would skip validation,
  authorization, and business rules that live in the API handlers.
- **No `Microsoft.AspNetCore.*` packages.** Server-side only.
- **No project reference to `MixxFit.API`.**
- **No secrets in the repo.** `appsettings.json` holds the base URL and nothing else.
  Credentials are typed at login and never persisted.

## Build and run

```bash
dotnet build
dotnet run --project MixxFit.Admin
```
