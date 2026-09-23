using System;
using System.Collections.Generic;
using System.Text;

namespace Mixxfit.Admin.Features.Auth.Login
{
    public record LoginRequest
    {
        public string Email { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
