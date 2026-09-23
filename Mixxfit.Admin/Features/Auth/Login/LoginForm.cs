using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mixxfit.Admin.Common;
using Mixxfit.Admin.Features.Auth.Login;

namespace Mixxfit.Admin.Features.Auth
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _auth;

        public LoginForm(AuthService auth)
        {
            _auth = auth;
            InitializeComponent();
            pbLogo.Image = Properties.Resources.LogoTransparent;

        }

        public async void OnLogin(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            btnLogin.Enabled = false;

            var validationErrors = GetValidationErrors(tbEmail.Text, tbPassword.Text);
            if (validationErrors is not null)
            {
                MessageBox.Show(validationErrors, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnLogin.Enabled = true;
                UseWaitCursor = false;
                return;
            }

            var request = new LoginRequest
            {
                Email = tbEmail.Text,
                Password = tbPassword.Text
            };

            var problemDetails = await _auth.LoginAsync(request);

            if (problemDetails is not null)
            {
                UseWaitCursor = false;
                btnLogin.Enabled = true;
                ShowServerError(problemDetails);
                return;
            }

            if (!_auth.IsAdmin)
            {
                UseWaitCursor = false;
                btnLogin.Enabled = true;
                MessageBox.Show("You do not have administrative privileges.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            MessageBox.Show("Welcome!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UseWaitCursor = false;
            btnLogin.Enabled = true;
            DialogResult = DialogResult.OK;
        }

        private string? GetValidationErrors(string email, string password)
        {
            if(string.IsNullOrWhiteSpace(email))
                return "Email is required.";
            if(string.IsNullOrWhiteSpace(password))
                return "Password is required.";
            return null;
        }

        private void ShowServerError(ProblemDetails problemDetails)
        {
            if (problemDetails.Status == 400)
            {
                MessageBox.Show("Email address format is not valid.", "Validation error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (problemDetails.ErrorCode == "Auth.LoginFailed")
            {
                MessageBox.Show("Login failed. Please check your email and password.", "Validation error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Unexpected error occurred. Please try again later.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

    }
}
