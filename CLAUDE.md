# MixxFit.Admin

Windows desktop administration client for the MixxFit platform. WinForms on .NET 10.

Administration lives here instead of in the Angular app. It is used by a single operator (the
project owner) to inspect users and platform statistics. It is not distributed to end users.

Root namespace is `Mixxfit.Admin` (lowercase `f`) — match it in every new file.

## Tech stack

- .NET 10, `net10.0-windows`, `<UseWindowsForms>true</UseWindowsForms>`
- WinForms (Designer-based, not hand-written layout)
- `System.Net.Http` + `System.Net.Http.Json`, `System.Text.Json`
- `Microsoft.Extensions.Configuration.Json` for `appsettings.json`

Windows-only by design. Do not add cross-platform shims.

## Related repositories

| Repo | Role |
|---|---|
| `Miks02/mixxfit-api` | .NET 10 Web API + PostgreSQL. The only data source for this app. |
| `Miks02/mixxfit-web` | Angular 21 end-user client. Unrelated to this app. |

HTTP only. No database access, no project reference to either repo — see "Hard rules".

The admin endpoints live on the API's `feature/admin-endpoints` branch.

## Architecture

Two layers, and the boundary is strict.

**`MixxFitApiClient` is transport.** A `sealed` singleton that owns the one `HttpClient`, holds
the access token, and knows nothing about features or endpoints. It is **not `partial`** — no
feature code accumulates inside it.

**Services own their feature.** Each feature folder has a service that takes the client in its
constructor and exposes that feature's endpoints. Views talk to services, never to the client.

```
Mixxfit.Admin/
├── Api/
│   └── MixxFitApiClient.cs          # singleton, HTTP only
├── Common/
│   ├── ProblemDetails.cs
│   ├── PagedResult.cs
│   ├── UserDetailsDto.cs
│   ├── Controls/
│   │   └── RoundedButton.cs
│   ├── Enums/                       # mirrors the API's Domain/Enums
│   └── Errors/
│       └── ErrorCatalog.cs          # static: ProblemDetails -> message
├── Features/
│   ├── Auth/
│   │   ├── AuthService.cs
│   │   ├── AuthResponse.cs
│   │   └── Login/
│   │       ├── LoginForm.cs
│   │       └── LoginRequest.cs
│   ├── Dashboard/
│   └── Users/
├── Main.cs                          # hosts and swaps views
├── Program.cs
└── appsettings.json
```

Adding a screen means adding a folder with a service, its DTOs and a view. It must not require
editing `MixxFitApiClient`.

### Why not MVP

MVP is the textbook WinForms pattern and is deliberately **not** used. Its payoff is testable UI
logic; this tool has a handful of screens doing "fetch, show, post a change" and no UI tests.
Thin code-behind with a per-feature service is the intended design. No presenters, no MVVM.

### Navigation

`Main` holds a content `Panel`. Each screen is a `UserControl` docked into it. No routing
framework, and no `IAdminView` abstraction — the click handler has the concrete type, so it
calls the load method directly:

```csharp
private async void btnUsers_Click(object? sender, EventArgs e)
{
    var view = new UsersView();
    view.Init(_dashboard);

    ShowView(view);
    await view.LoadDataAsync();
}

private void ShowView(UserControl view)
{
    pnlContent.Controls.Clear();
    view.Dock = DockStyle.Fill;
    pnlContent.Controls.Add(view);
}
```

Introduce a common interface only if a host-level action (a global "Refresh" button) ever needs
to call a method on whichever view is active without knowing its type.

Add/edit flows are separate modal `Form`s opened with `ShowDialog()`, exposing their result via
a public property and setting `DialogResult.OK` on save.

`UserControl`s must keep a **parameterless constructor** or the Designer breaks — inject through
an `Init(...)` method. `Form`s may take constructor parameters; the Designer instantiates the
base class for those.

## Error handling

**No exceptions for API failures. No `Result<T>`. No interceptors.** The error body the server
returns is what the caller gets back.

`MixxFitApiClient` returns a tuple; `Problem` is `null` on success:

```csharp
Task<(T? Data, ProblemDetails? Problem)> SendAsync<T>(...)   // with a payload
Task<ProblemDetails?> SendAsync(...)                          // without one
```

Call sites check one thing:

```csharp
var (data, problem) = await _dashboard.GetAdminDashboardAsync(_page, PageSize);

if (problem is not null)
{
    MessageBox.Show(ErrorCatalog.Describe(problem), "Error");
    return;
}

dgvUsers.DataSource = data!.Users.Items;
```

`HttpRequestException` and `TaskCanceledException` are the only things .NET throws here, and
`SendCoreAsync` converts them into a `ProblemDetails` with status 503 or 408. A caller never
writes `try`/`catch` for an API call.

Use `MessageBox` directly. Do not build a notification service or wrap it.

### `ProblemDetails`

The API produces two different error shapes, and `ProblemDetails` must deserialize both.

**Business errors** — `ResultExtensions.ToProblemResult`, which sets `errorCode` as an extension
(serialized at the top level) and hardcodes `detail`:

```json
{
  "title": "urn:mixxfit-api:error:Auth.LoginFailed",
  "detail": "An error occurred while processing your request.",
  "status": 401,
  "errorCode": "Auth.LoginFailed"
}
```

**Validation errors** — `ValidationFilter` calls `TypedResults.ValidationProblem`, which emits an
`errors` **dictionary** keyed by camelCase field name, and **no** `errorCode`:

```json
{
  "title": "Validation failed",
  "status": 400,
  "errors": { "pageSize": ["Page size must be between 1 and 100"] }
}
```

So `Errors` must be `Dictionary<string, string[]>?`, never `string[]` — the wrong type throws
during deserialization, `ReadProblemAsync` swallows it, and the whole problem body is silently
lost leaving only a bare status code.

### `ErrorCatalog`

The API **does not send a human-readable message** for business errors — `ToProblemResult`
discards `Error.Description` and hardcodes `detail`. Only the code survives. So the client owns
its wording.

`ErrorCatalog` is a **static class**, not a service: a switch on `ErrorCode`, then a status-code
fallback, mirroring `handleErrors` in the web repo's `http-error-interceptor.ts`. Keep the code
strings identical to the API's `Domain/ErrorCatalog`, and add entries only when a screen needs
one. For a 400, prefer flattening `Errors` into the message.

## `MixxFitApiClient`

### Wiring

Initialized once in `Program.Main`. Services receive the client through their constructor — they
never reach for `Instance` internally, so a service stays independent of how it is created.

```csharp
MixxFitApiClient.Initialize(config["Api:BaseUrl"]!);

var auth = new AuthService(MixxFitApiClient.Instance);
MixxFitApiClient.Instance.RefreshCallback = auth.TryRefreshAsync;
```

`RefreshCallback` is the one non-obvious piece. The client must refresh on a 401 but must not
know the auth endpoints — that is `AuthService`'s job. The delegate breaks the circle.

Every auth call passes `allowRefresh: false`, or a failed refresh recurses. On login a 401 means
wrong credentials, not an expired session.

### HttpClient rules

Not style preferences; breaking these causes real bugs.

**One long-lived `HttpClient`.** Never `using var client = new HttpClient()` per call.

**Do NOT use `IHttpClientFactory`.** Auth depends on a `CookieContainer` that must survive
between login and refresh. The factory rotates the primary handler periodically, which silently
discards the refresh-token cookie and logs the operator out at random.

**`BaseAddress` ends with `/`; relative URLs must NOT start with `/`.** A leading slash discards
the base path, dropping the required `/api` segment. `SendOnceAsync` calls `url.TrimStart('/')`
as a guard, but write URLs without it anyway — `"auth/login"`, not `"/auth/login"`.

**Shared JSON options, both settings required.** The API serializes camelCase and writes enums as
strings. Without both, deserialization silently yields nulls and defaults.

**Pass the runtime type when serializing a body:** `JsonContent.Create(body, body.GetType(), …)`.
`body` is declared `object?`, so the generic overload would infer `T = object` and serialize `{}`.

**A fresh `HttpRequestMessage` per attempt** — it cannot be reused.

### Authentication

- `POST auth/login` returns `{ accessToken, user }`. The refresh token is set as an HTTP-only
  cookie and is `[JsonIgnore]`-d out of the body.
- `POST auth/refresh-token` takes **no body** — the cookie travels via `CookieContainer`.
- On 401, refresh once and retry once. Never loop.
- Nothing is persisted, so there is never a token at startup: `LoginForm` always shows first,
  before `Application.Run`. There is no "do we have a token" check to write.

### Admin check

`UserDetailsDto.Roles` comes back on the login response, so admin-ness is read from it directly —
no JWT decoding:

```csharp
IsAdmin = data.User.Roles.Any(r => r.Equals("Admin", StringComparison.OrdinalIgnoreCase));
```

This is **UX only** — it decides whether to open the admin window instead of letting every click
return 403. The server's `AdminOnly` policy (`RequireRole("Admin")`) is the actual enforcement.

## API surface

All endpoints sit under the API's `api/` route group.

- Production: `https://api.getmixxfit.com/api/`
- Local dev: `https://localhost:7250/api/`

### Available today

| Method | Route | Policy | Returns |
|---|---|---|---|
| POST | `auth/login` | anonymous | `{ accessToken, user }` |
| POST | `auth/refresh-token` | cookie | `{ accessToken, user }` |
| POST | `auth/logout` | authenticated | 204 |
| GET | `dashboard/admin?page=&pageSize=` | `AdminOnly` | `GetAdminDashboardResponse` |

`GET dashboard/admin` returns statistics **and** a paged user list in one payload:

```csharp
public record AdminDashboardResponse
{
    public PagedResult<AdminDashboardUserDto> Users { get; init; } = null!;
    public int TotalExercises { get; init; }
    public int TotalWorkouts { get; init; }
    public ExerciseType? MostCommonExerciseType { get; init; }
    public int TotalWeightEntries { get; init; }
    public double? AverageUserAge { get; init; }
}
```

There is **no separate users endpoint**. The Users view pages through this same route and reads
only `Users`; the Dashboard view reads the statistics and may show the first page of users.

Server-side validation: `page > 0`, `pageSize` between 1 and 100. Violations come back as a 400
validation problem, not a business error.

### Not available yet

The API has **no endpoint to change `AccountStatus` or to delete another user.** `DELETE users`
deletes the *calling* user via `ICurrentUserProvider`, so it is not an admin operation.

Do not write client service methods for moderation actions until those endpoints exist. The
Users view is **read-only** for now.

## Shared contracts

DTOs are **hand-written copies** of the API's shapes. Deliberate: the projects deploy separately
and must version independently, and a project reference would drag ASP.NET Core dependencies into
a desktop app. When a response shape changes, update it here by hand.

`PagedResult<T>` mirrors the API's, with one required difference:

```csharp
public record PagedResult<T>(List<T> Items, int Page, int PageSize, int TotalCount, int PaginatedCount)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
```

`Items` **must** be `List<T>`, not `IReadOnlyList<T>` as on the server — `DataGridView.DataSource`
requires `IList`, which `IReadOnlyList<T>` does not implement. The grid then binds to nothing and
reports no error.

Enums are mirrored in `Common/Enums/` and must match the API member-for-member, because they
travel as strings:

```csharp
public enum AccountStatus { Active = 1, Suspended = 2 }
public enum ExerciseType { Other = 0, WeightLifting = 1, BodyWeight = 2, Cardio = 3, Stretching = 4 }
```

`AccountStatus` has only these two members — there is no `Banned`.

## WinForms conventions

**`async void` only on event handlers**, never elsewhere. API errors do not throw, so handlers do
not need `try`/`catch` for them; real work lives in `async Task` methods the handler awaits.

**Never `.Result` or `.Wait()`.** WinForms has a `SynchronizationContext`; blocking on a task from
the UI thread deadlocks the app permanently.

**Progress: `UseWaitCursor = true` in a `try`/`finally`**, not `Cursor = Cursors.WaitCursor`.
`Cursor` applies to one control, so child controls with their own cursor override it;
`UseWaitCursor` covers the form and all children. Also disable the triggering button — the cursor
does not prevent a double click.

**Refreshing a grid is a plain `DataSource` assignment.** There is no change detection. After a
mutation, re-fetch and reassign rather than mutating the bound list in place.

**Paging is manual.** Keep the current page in a field, reload on change, drive the navigation
buttons from `TotalPages`.

## Packages

Little is shared with the backend — most of what this client needs ships in the BCL. Do not add
packages just to mirror the API. Where one is genuinely needed, **match the API's version**.

| Package | Version | Use |
|---|---|---|
| `Microsoft.Extensions.Configuration.Json` | — | `appsettings.json` |
| `FluentValidation` | 12.1.1 | Only if dialog validation outgrows plain checks. Same version as the API. |
| `Serilog` + `Serilog.Sinks.File` | — | Local log file, if diagnosing failed requests needs it. |

## Configuration

WinForms has no `appsettings.json` by default. It needs a copy-to-output entry in the `.csproj`:

```xml
<ItemGroup>
  <None Update="appsettings.json">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

```csharp
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json", optional: true)
    .Build();
```

`SetBasePath(AppContext.BaseDirectory)` is required, not decorative: without it the path resolves
against the *working directory*, which is not always the exe folder, producing a confusing
`FileNotFoundException`.

## Hard rules

- **No database access.** No `Microsoft.EntityFrameworkCore`, no `Npgsql`, no connection strings.
  Everything goes through the API — bypassing it would skip validation, authorization and
  business rules that live in the API handlers.
- **No `Microsoft.AspNetCore.*` packages.** Server-side only.
- **No project reference to `MixxFit.API`.**
- **`MixxFitApiClient` stays `sealed` and endpoint-free.** Endpoints belong in feature services.
- **Never ignore `*.resx` or `*.Designer.cs` in git.** They are Designer output; the forms do not
  build without them.
- **No secrets in the repo.** `appsettings.json` holds the base URL and nothing else. Credentials
  are typed at login and never persisted.

## Build and run

```bash
dotnet build
dotnet run --project Mixxfit.Admin
```

Requires Windows. Designer work requires Visual Studio; Rider's WinForms designer support is
limited.
