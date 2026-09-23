using System;
using System.Collections.Generic;
using System.Text;

namespace Mixxfit.Admin.Common
{
    public class UserDetailsDto
    {
        public string FullName { get; init; } = null!;
        public string UserName { get; init; } = null!;
        public string Email { get; init; } = null!;
        public DateTime? DateOfBirth { get; init; }
        public IReadOnlyList<string> Roles { get; init; } = [];
    }
}
