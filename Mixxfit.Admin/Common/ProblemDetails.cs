using System;
using System.Collections.Generic;
using System.Text;

namespace Mixxfit.Admin.Common
{
    public record ProblemDetails
    {
        public int Status { get; init; }
        public string Detail { get; init; } = string.Empty;
        public string ErrorCode { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string[] Errors { get; init; } = [];
    }
}
