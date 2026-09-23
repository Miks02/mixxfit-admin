using Mixxfit.Admin.Common.Controls;

namespace Mixxfit.Admin.Features.Auth
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            pbLogo = new PictureBox();
            pnlEmail = new Panel();
            tbEmail = new TextBox();
            lblEmailIcon = new Label();
            pnlPassword = new Panel();
            tbPassword = new TextBox();
            lblPasswordIcon = new Label();
            btnLogin = new RoundedButton();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            pnlEmail.SuspendLayout();
            pnlPassword.SuspendLayout();
            SuspendLayout();
            // 
            // pbLogo
            // 
            pbLogo.BackColor = Color.Transparent;
            pbLogo.Image = Properties.Resources.LogoTransparent;
            pbLogo.Location = new Point(83, 12);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(226, 210);
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogo.TabIndex = 0;
            pbLogo.TabStop = false;
            // 
            // pnlEmail
            // 
            pnlEmail.Anchor = AnchorStyles.None;
            pnlEmail.BackColor = Color.White;
            pnlEmail.BorderStyle = BorderStyle.FixedSingle;
            pnlEmail.Controls.Add(tbEmail);
            pnlEmail.Controls.Add(lblEmailIcon);
            pnlEmail.Location = new Point(50, 264);
            pnlEmail.Name = "pnlEmail";
            pnlEmail.Padding = new Padding(0, 0, 10, 0);
            pnlEmail.Size = new Size(300, 48);
            pnlEmail.TabIndex = 1;
            // 
            // tbEmail
            // 
            tbEmail.BackColor = Color.White;
            tbEmail.BorderStyle = BorderStyle.None;
            tbEmail.Font = new Font("Segoe UI", 12F);
            tbEmail.Location = new Point(44, 13);
            tbEmail.Margin = new Padding(0);
            tbEmail.Name = "tbEmail";
            tbEmail.PlaceholderText = "Email";
            tbEmail.Size = new Size(244, 22);
            tbEmail.TabIndex = 1;
            // 
            // lblEmailIcon
            // 
            lblEmailIcon.Dock = DockStyle.Left;
            lblEmailIcon.Font = new Font("Segoe MDL2 Assets", 13F);
            lblEmailIcon.ForeColor = Color.Orange;
            lblEmailIcon.Location = new Point(0, 0);
            lblEmailIcon.Name = "lblEmailIcon";
            lblEmailIcon.Size = new Size(44, 46);
            lblEmailIcon.TabIndex = 0;
            lblEmailIcon.Text = "";
            lblEmailIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlPassword
            // 
            pnlPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            pnlPassword.BackColor = Color.White;
            pnlPassword.BorderStyle = BorderStyle.FixedSingle;
            pnlPassword.Controls.Add(tbPassword);
            pnlPassword.Controls.Add(lblPasswordIcon);
            pnlPassword.Location = new Point(50, 330);
            pnlPassword.Name = "pnlPassword";
            pnlPassword.Padding = new Padding(0, 0, 10, 0);
            pnlPassword.Size = new Size(300, 48);
            pnlPassword.TabIndex = 2;
            // 
            // tbPassword
            // 
            tbPassword.BackColor = Color.White;
            tbPassword.BorderStyle = BorderStyle.None;
            tbPassword.Font = new Font("Segoe UI", 12F);
            tbPassword.Location = new Point(44, 13);
            tbPassword.Margin = new Padding(0);
            tbPassword.Name = "tbPassword";
            tbPassword.PlaceholderText = "Lozinka";
            tbPassword.Size = new Size(244, 22);
            tbPassword.TabIndex = 1;
            tbPassword.UseSystemPasswordChar = true;
            // 
            // lblPasswordIcon
            // 
            lblPasswordIcon.Dock = DockStyle.Left;
            lblPasswordIcon.Font = new Font("Segoe MDL2 Assets", 13F);
            lblPasswordIcon.ForeColor = Color.Orange;
            lblPasswordIcon.Location = new Point(0, 0);
            lblPasswordIcon.Name = "lblPasswordIcon";
            lblPasswordIcon.Size = new Size(44, 46);
            lblPasswordIcon.TabIndex = 0;
            lblPasswordIcon.Text = "";
            lblPasswordIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.Orange;
            btnLogin.CornerRadius = 12;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.DisabledBackColor = Color.FromArgb(217, 150, 0);
            btnLogin.DisabledForeColor = Color.FromArgb(120, 120, 120);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.HoverColor = Color.FromArgb(255, 178, 51);
            btnLogin.Location = new Point(50, 412);
            btnLogin.Name = "btnLogin";
            btnLogin.PressedColor = Color.FromArgb(217, 132, 0);
            btnLogin.Size = new Size(300, 52);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += OnLogin;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(400, 508);
            Controls.Add(btnLogin);
            Controls.Add(pnlPassword);
            Controls.Add(pnlEmail);
            Controls.Add(pbLogo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(416, 547);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mixxfit - Login";
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            pnlEmail.ResumeLayout(false);
            pnlEmail.PerformLayout();
            pnlPassword.ResumeLayout(false);
            pnlPassword.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbLogo;
        private Panel pnlEmail;
        private TextBox tbEmail;
        private Label lblEmailIcon;
        private Panel pnlPassword;
        private TextBox tbPassword;
        private Label lblPasswordIcon;
        private RoundedButton btnLogin;
    }
}
