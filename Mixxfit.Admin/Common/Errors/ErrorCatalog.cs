namespace Mixxfit.Admin.Common.Errors
{
    public static class ErrorCatalog
    {
        public static string Describe(ProblemDetails problem)
        {
            if (problem.Errors is { Count: > 0 })
            {
                return string.Join(
                    Environment.NewLine,
                    problem.Errors.Values.SelectMany(messages => messages));
            }

            var byCode = problem.ErrorCode switch
            {
                "Auth.LoginFailed" => "Invalid email or password.",
                "Auth.SessionExpired" => "Your session has expired. Please log in again.",
                "Auth.ExpiredToken" => "Your session has expired. Please log in again.",
                "Auth.AccountLocked" => "This account is locked. Try again later.",
                "Admin.NotFound" => "Admin not found.",
                "User.NotFound" => "User not found.",
                "User.AlreadySuspended" => "This user is already suspended.",
                "User.AlreadyActive" => "This user is already active.",
                "User.AlreadyDeleted" => "This user has already been deleted.",
                _ => null
            };

            if (byCode is not null)
                return byCode;

            var byStatus = problem.Status switch
            {
                400 => "Invalid input.",
                401 => "Authentication failed.",
                403 => "You don't have access to this resource.",
                404 => "Not found.",
                408 => "The request timed out.",
                409 => "This item already exists.",
                429 => "Too many requests. Please slow down.",
                503 => "The API is unavailable.",
                0 => null,
                _ => "An unexpected server error occurred."
            };

            return byStatus ?? problem.Detail ?? "An unexpected error occurred.";
        }
    }
}
