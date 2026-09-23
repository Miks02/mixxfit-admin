using Mixxfit.Admin.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mixxfit.Admin.Features.Auth
{
    public class AuthResponse
    {
        public string AccessToken { get; init; } = null!;
        public UserDetailsDto User { get; init; } = null!;
    }
}
