using Mixxfit.Admin.Features.Auth;

namespace Mixxfit.Admin
{
    public partial class Main : Form
    {
        private readonly AuthService _authService;
        public Main(AuthService authService)
        {
            _authService = authService;
            InitializeComponent();
        }
    }
}
