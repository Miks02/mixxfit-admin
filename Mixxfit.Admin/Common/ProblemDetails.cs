using System;
using System.Collections.Generic;
using System.Text;

namespace Mixxfit.Admin.Common
{
    public record ProblemDetails
    {
        public int Status { get; init; }
        public string? Detail { get; init; }
        public string? ErrorCode { get; init; }
        public string? Title { get; init; }
        public Dictionary<string, string[]>? Errors { get; init; }
    }
}
