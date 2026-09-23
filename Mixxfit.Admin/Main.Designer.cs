namespace Mixxfit.Admin
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            btnLogout = new Mixxfit.Admin.Common.Controls.RoundedButton();
            lblTitle = new Label();
            pnlContent = new Panel();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(251, 191, 36);
            pnlHeader.Controls.Add(btnLogout);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1357, 64);
            pnlHeader.TabIndex = 0;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.BackColor = Color.FromArgb(30, 41, 59);
            btnLogout.CornerRadius = 8;
            btnLogout.DisabledBackColor = Color.FromArgb(100, 116, 139);
            btnLogout.DisabledForeColor = Color.FromArgb(203, 213, 225);
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI Semibold", 10F);
            btnLogout.ForeColor = Color.White;
            btnLogout.HoverColor = Color.FromArgb(51, 65, 85);
            btnLogout.Location = new Point(1237, 14);
            btnLogout.Name = "btnLogout";
            btnLogout.PressedColor = Color.FromArgb(15, 23, 42);
            btnLogout.Size = new Size(96, 36);
            btnLogout.TabIndex = 1;
            btnLogout.Text = "Log out";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 15F);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(24, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(143, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "MixxFit Admin";
            // 
            // pnlContent
            // 
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 64);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1357, 812);
            pnlContent.TabIndex = 1;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1357, 876);
            Controls.Add(pnlContent);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(700, 400);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MixxFit";
            Shown += Main_Shown;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Common.Controls.RoundedButton btnLogout;
        private Panel pnlContent;
    }
}
