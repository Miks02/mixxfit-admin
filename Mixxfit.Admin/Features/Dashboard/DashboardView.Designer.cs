namespace Mixxfit.Admin.Features.Dashboard
{
    partial class DashboardView
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

        #region Component Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle altRowStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle ageStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle createdStyle = new DataGridViewCellStyle();
            tlpStats = new TableLayoutPanel();
            cardUsers = new Common.Controls.StatCard();
            cardWorkouts = new Common.Controls.StatCard();
            cardExercises = new Common.Controls.StatCard();
            cardWeightEntries = new Common.Controls.StatCard();
            cardMostCommonType = new Common.Controls.StatCard();
            cardAverageAge = new Common.Controls.StatCard();
            tlpMain = new TableLayoutPanel();
            pnlGridCard = new Common.Controls.CardPanel();
            dgUsers = new DataGridView();
            colName = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colAge = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colCreated = new DataGridViewTextBoxColumn();
            colWorkouts = new DataGridViewTextBoxColumn();
            colWeightEntries = new DataGridViewTextBoxColumn();
            pnlFilters = new Panel();
            tbSearch = new TextBox();
            cbSort = new ComboBox();
            chkDeleted = new CheckBox();
            btnRefetch = new Common.Controls.RoundedButton();
            pnlPager = new Panel();
            lblTotal = new Label();
            btnPrev = new Common.Controls.RoundedButton();
            lblPage = new Label();
            btnNext = new Common.Controls.RoundedButton();
            pnlSide = new Panel();
            gbUserOptions = new Common.Controls.CardGroupBox();
            tlpUser = new TableLayoutPanel();
            lblName = new Label();
            lblEmail = new Label();
            lblCreatedCaption = new Label();
            lblCreated = new Label();
            lblDeletedCaption = new Label();
            lblDeleted = new Label();
            tlpActions = new TableLayoutPanel();
            btnActivate = new Common.Controls.RoundedButton();
            btnDeactivate = new Common.Controls.RoundedButton();
            btnDelete = new Common.Controls.RoundedButton();
            lblSelectUser = new Label();
            tmrSearch = new System.Windows.Forms.Timer(components);
            tlpStats.SuspendLayout();
            tlpMain.SuspendLayout();
            pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgUsers).BeginInit();
            pnlFilters.SuspendLayout();
            pnlPager.SuspendLayout();
            pnlSide.SuspendLayout();
            gbUserOptions.SuspendLayout();
            tlpUser.SuspendLayout();
            tlpActions.SuspendLayout();
            SuspendLayout();
            //
            // tlpStats
            //
            tlpStats.BackColor = Color.Transparent;
            tlpStats.ColumnCount = 3;
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpStats.Controls.Add(cardUsers, 0, 0);
            tlpStats.Controls.Add(cardWorkouts, 1, 0);
            tlpStats.Controls.Add(cardExercises, 2, 0);
            tlpStats.Controls.Add(cardWeightEntries, 0, 1);
            tlpStats.Controls.Add(cardMostCommonType, 1, 1);
            tlpStats.Controls.Add(cardAverageAge, 2, 1);
            tlpStats.Dock = DockStyle.Top;
            tlpStats.Location = new Point(16, 16);
            tlpStats.Name = "tlpStats";
            tlpStats.RowCount = 2;
            tlpStats.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpStats.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpStats.Size = new Size(1664, 264);
            tlpStats.TabIndex = 0;
            //
            // cardUsers
            //
            cardUsers.BackColor = Color.Transparent;
            cardUsers.Dock = DockStyle.Fill;
            cardUsers.Location = new Point(8, 8);
            cardUsers.Margin = new Padding(8);
            cardUsers.MinimumSize = new Size(120, 90);
            cardUsers.Name = "cardUsers";
            cardUsers.Padding = new Padding(20, 28, 16, 12);
            cardUsers.Size = new Size(538, 116);
            cardUsers.TabIndex = 0;
            cardUsers.Title = "Total users";
            //
            // cardWorkouts
            //
            cardWorkouts.BackColor = Color.Transparent;
            cardWorkouts.Dock = DockStyle.Fill;
            cardWorkouts.Location = new Point(562, 8);
            cardWorkouts.Margin = new Padding(8);
            cardWorkouts.MinimumSize = new Size(120, 90);
            cardWorkouts.Name = "cardWorkouts";
            cardWorkouts.Padding = new Padding(20, 28, 16, 12);
            cardWorkouts.Size = new Size(538, 116);
            cardWorkouts.TabIndex = 1;
            cardWorkouts.Title = "Total workouts";
            //
            // cardExercises
            //
            cardExercises.BackColor = Color.Transparent;
            cardExercises.Dock = DockStyle.Fill;
            cardExercises.Location = new Point(1116, 8);
            cardExercises.Margin = new Padding(8);
            cardExercises.MinimumSize = new Size(120, 90);
            cardExercises.Name = "cardExercises";
            cardExercises.Padding = new Padding(20, 28, 16, 12);
            cardExercises.Size = new Size(540, 116);
            cardExercises.TabIndex = 2;
            cardExercises.Title = "Total exercises";
            //
            // cardWeightEntries
            //
            cardWeightEntries.BackColor = Color.Transparent;
            cardWeightEntries.Dock = DockStyle.Fill;
            cardWeightEntries.Location = new Point(8, 140);
            cardWeightEntries.Margin = new Padding(8);
            cardWeightEntries.MinimumSize = new Size(120, 90);
            cardWeightEntries.Name = "cardWeightEntries";
            cardWeightEntries.Padding = new Padding(20, 28, 16, 12);
            cardWeightEntries.Size = new Size(538, 116);
            cardWeightEntries.TabIndex = 3;
            cardWeightEntries.Title = "Total weight entries";
            //
            // cardMostCommonType
            //
            cardMostCommonType.BackColor = Color.Transparent;
            cardMostCommonType.Dock = DockStyle.Fill;
            cardMostCommonType.Location = new Point(562, 140);
            cardMostCommonType.Margin = new Padding(8);
            cardMostCommonType.MinimumSize = new Size(120, 90);
            cardMostCommonType.Name = "cardMostCommonType";
            cardMostCommonType.Padding = new Padding(20, 28, 16, 12);
            cardMostCommonType.Size = new Size(538, 116);
            cardMostCommonType.TabIndex = 4;
            cardMostCommonType.Title = "Most common exercise type";
            //
            // cardAverageAge
            //
            cardAverageAge.BackColor = Color.Transparent;
            cardAverageAge.Dock = DockStyle.Fill;
            cardAverageAge.Location = new Point(1116, 140);
            cardAverageAge.Margin = new Padding(8);
            cardAverageAge.MinimumSize = new Size(120, 90);
            cardAverageAge.Name = "cardAverageAge";
            cardAverageAge.Padding = new Padding(20, 28, 16, 12);
            cardAverageAge.Size = new Size(540, 116);
            cardAverageAge.TabIndex = 5;
            cardAverageAge.Title = "Average user age";
            //
            // tlpMain
            //
            tlpMain.BackColor = Color.Transparent;
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 420F));
            tlpMain.Controls.Add(pnlGridCard, 0, 0);
            tlpMain.Controls.Add(pnlSide, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(16, 280);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(1664, 516);
            tlpMain.TabIndex = 1;
            //
            // pnlGridCard
            //
            pnlGridCard.BackColor = Color.Transparent;
            pnlGridCard.Controls.Add(dgUsers);
            pnlGridCard.Controls.Add(pnlPager);
            pnlGridCard.Controls.Add(pnlFilters);
            pnlGridCard.Dock = DockStyle.Fill;
            pnlGridCard.Location = new Point(8, 8);
            pnlGridCard.Margin = new Padding(8);
            pnlGridCard.Name = "pnlGridCard";
            pnlGridCard.Padding = new Padding(16);
            pnlGridCard.Size = new Size(1228, 544);
            pnlGridCard.TabIndex = 0;
            //
            // pnlFilters
            //
            pnlFilters.BackColor = Color.White;
            pnlFilters.Controls.Add(btnRefetch);
            pnlFilters.Controls.Add(chkDeleted);
            pnlFilters.Controls.Add(cbSort);
            pnlFilters.Controls.Add(tbSearch);
            pnlFilters.Dock = DockStyle.Top;
            pnlFilters.Location = new Point(16, 16);
            pnlFilters.Name = "pnlFilters";
            pnlFilters.Padding = new Padding(0, 8, 0, 8);
            pnlFilters.Size = new Size(1196, 52);
            pnlFilters.TabIndex = 0;
            //
            // tbSearch
            //
            tbSearch.BackColor = Color.FromArgb(248, 250, 252);
            tbSearch.BorderStyle = BorderStyle.FixedSingle;
            tbSearch.Font = new Font("Segoe UI", 11F);
            tbSearch.ForeColor = Color.FromArgb(15, 23, 42);
            tbSearch.Location = new Point(0, 8);
            tbSearch.Name = "tbSearch";
            tbSearch.PlaceholderText = "Search by name or email";
            tbSearch.Size = new Size(360, 27);
            tbSearch.TabIndex = 0;
            tbSearch.TextChanged += tbSearch_TextChanged;
            //
            // cbSort
            //
            cbSort.BackColor = Color.FromArgb(248, 250, 252);
            cbSort.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSort.FlatStyle = FlatStyle.Flat;
            cbSort.Font = new Font("Segoe UI", 11F);
            cbSort.ForeColor = Color.FromArgb(15, 23, 42);
            cbSort.FormattingEnabled = true;
            cbSort.Items.AddRange(new object[] { "Newest first", "Oldest first", "Name", "Email" });
            cbSort.Location = new Point(376, 8);
            cbSort.Name = "cbSort";
            cbSort.Size = new Size(170, 28);
            cbSort.TabIndex = 1;
            cbSort.SelectedIndex = 0;
            cbSort.SelectedIndexChanged += cbSort_SelectedIndexChanged;
            //
            // chkDeleted
            //
            chkDeleted.AutoSize = true;
            chkDeleted.Font = new Font("Segoe UI", 10.5F);
            chkDeleted.ForeColor = Color.FromArgb(51, 65, 85);
            chkDeleted.Location = new Point(566, 11);
            chkDeleted.Name = "chkDeleted";
            chkDeleted.Size = new Size(125, 23);
            chkDeleted.TabIndex = 2;
            chkDeleted.Text = "Show deleted";
            chkDeleted.UseVisualStyleBackColor = true;
            chkDeleted.CheckedChanged += chkDeleted_CheckedChanged;
            //
            // btnRefetch
            //
            btnRefetch.BackColor = Color.FromArgb(30, 41, 59);
            btnRefetch.CornerRadius = 8;
            btnRefetch.DisabledBackColor = Color.FromArgb(226, 232, 240);
            btnRefetch.DisabledForeColor = Color.FromArgb(148, 163, 184);
            btnRefetch.Dock = DockStyle.Right;
            btnRefetch.FlatAppearance.BorderSize = 0;
            btnRefetch.FlatStyle = FlatStyle.Flat;
            btnRefetch.Font = new Font("Segoe UI Semibold", 10F);
            btnRefetch.ForeColor = Color.White;
            btnRefetch.HoverColor = Color.FromArgb(51, 65, 85);
            btnRefetch.Name = "btnRefetch";
            btnRefetch.PressedColor = Color.FromArgb(15, 23, 42);
            btnRefetch.Size = new Size(110, 36);
            btnRefetch.TabIndex = 3;
            btnRefetch.Text = "Refetch";
            btnRefetch.UseVisualStyleBackColor = false;
            btnRefetch.Click += btnRefetch_Click;
            //
            // pnlPager
            //
            pnlPager.BackColor = Color.White;
            pnlPager.Controls.Add(lblTotal);
            pnlPager.Controls.Add(btnPrev);
            pnlPager.Controls.Add(lblPage);
            pnlPager.Controls.Add(btnNext);
            pnlPager.Dock = DockStyle.Bottom;
            pnlPager.Location = new Point(16, 472);
            pnlPager.Name = "pnlPager";
            pnlPager.Padding = new Padding(0, 12, 0, 0);
            pnlPager.Size = new Size(1196, 56);
            pnlPager.TabIndex = 2;
            //
            // lblTotal
            //
            lblTotal.Dock = DockStyle.Fill;
            lblTotal.Font = new Font("Segoe UI", 10F);
            lblTotal.ForeColor = Color.FromArgb(100, 116, 139);
            lblTotal.Name = "lblTotal";
            lblTotal.TabIndex = 0;
            lblTotal.TextAlign = ContentAlignment.MiddleLeft;
            //
            // btnPrev
            //
            btnPrev.BackColor = Color.FromArgb(30, 41, 59);
            btnPrev.CornerRadius = 8;
            btnPrev.DisabledBackColor = Color.FromArgb(226, 232, 240);
            btnPrev.DisabledForeColor = Color.FromArgb(148, 163, 184);
            btnPrev.Dock = DockStyle.Right;
            btnPrev.Enabled = false;
            btnPrev.FlatAppearance.BorderSize = 0;
            btnPrev.FlatStyle = FlatStyle.Flat;
            btnPrev.Font = new Font("Segoe UI Semibold", 10F);
            btnPrev.ForeColor = Color.White;
            btnPrev.HoverColor = Color.FromArgb(51, 65, 85);
            btnPrev.Name = "btnPrev";
            btnPrev.PressedColor = Color.FromArgb(15, 23, 42);
            btnPrev.Size = new Size(100, 44);
            btnPrev.TabIndex = 1;
            btnPrev.Text = "Previous";
            btnPrev.UseVisualStyleBackColor = false;
            btnPrev.Click += btnPrev_Click;
            //
            // lblPage
            //
            lblPage.Dock = DockStyle.Right;
            lblPage.Font = new Font("Segoe UI Semibold", 10F);
            lblPage.ForeColor = Color.FromArgb(30, 41, 59);
            lblPage.Name = "lblPage";
            lblPage.Size = new Size(140, 44);
            lblPage.TabIndex = 2;
            lblPage.Text = "Page 1 of 1";
            lblPage.TextAlign = ContentAlignment.MiddleCenter;
            //
            // btnNext
            //
            btnNext.BackColor = Color.FromArgb(30, 41, 59);
            btnNext.CornerRadius = 8;
            btnNext.DisabledBackColor = Color.FromArgb(226, 232, 240);
            btnNext.DisabledForeColor = Color.FromArgb(148, 163, 184);
            btnNext.Dock = DockStyle.Right;
            btnNext.Enabled = false;
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Font = new Font("Segoe UI Semibold", 10F);
            btnNext.ForeColor = Color.White;
            btnNext.HoverColor = Color.FromArgb(51, 65, 85);
            btnNext.Name = "btnNext";
            btnNext.PressedColor = Color.FromArgb(15, 23, 42);
            btnNext.Size = new Size(100, 44);
            btnNext.TabIndex = 3;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            //
            // dgUsers
            //
            dgUsers.AllowUserToAddRows = false;
            dgUsers.AllowUserToDeleteRows = false;
            dgUsers.AllowUserToResizeRows = false;
            dgUsers.AutoGenerateColumns = false;
            dgUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgUsers.BackgroundColor = Color.White;
            dgUsers.BorderStyle = BorderStyle.None;
            dgUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = Color.FromArgb(248, 250, 252);
            headerStyle.Font = new Font("Segoe UI Semibold", 9.5F);
            headerStyle.ForeColor = Color.FromArgb(100, 116, 139);
            headerStyle.Padding = new Padding(8, 0, 8, 0);
            headerStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
            headerStyle.SelectionForeColor = Color.FromArgb(100, 116, 139);
            headerStyle.WrapMode = DataGridViewTriState.False;
            dgUsers.ColumnHeadersDefaultCellStyle = headerStyle;
            dgUsers.ColumnHeadersHeight = 40;
            dgUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgUsers.Columns.AddRange(new DataGridViewColumn[] { colName, colEmail, colAge, colStatus, colCreated, colWorkouts, colWeightEntries });
            rowStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            rowStyle.BackColor = Color.White;
            rowStyle.Font = new Font("Segoe UI", 10F);
            rowStyle.ForeColor = Color.FromArgb(30, 41, 59);
            rowStyle.Padding = new Padding(8, 0, 8, 0);
            rowStyle.SelectionBackColor = Color.FromArgb(254, 243, 199);
            rowStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            rowStyle.WrapMode = DataGridViewTriState.False;
            dgUsers.DefaultCellStyle = rowStyle;
            altRowStyle.BackColor = Color.FromArgb(248, 250, 252);
            altRowStyle.SelectionBackColor = Color.FromArgb(254, 243, 199);
            altRowStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dgUsers.AlternatingRowsDefaultCellStyle = altRowStyle;
            dgUsers.Dock = DockStyle.Fill;
            dgUsers.EnableHeadersVisualStyles = false;
            dgUsers.GridColor = Color.FromArgb(241, 245, 249);
            dgUsers.Location = new Point(16, 68);
            dgUsers.MultiSelect = false;
            dgUsers.Name = "dgUsers";
            dgUsers.ReadOnly = true;
            dgUsers.RowHeadersVisible = false;
            dgUsers.RowTemplate.Height = 44;
            dgUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgUsers.Size = new Size(1196, 460);
            dgUsers.TabIndex = 1;
            dgUsers.CellFormatting += dgUsers_CellFormatting;
            dgUsers.SelectionChanged += dgUsers_SelectionChanged;
            //
            // colName
            //
            colName.DataPropertyName = "FullName";
            colName.FillWeight = 22F;
            colName.HeaderText = "Name";
            colName.Name = "colName";
            colName.ReadOnly = true;
            //
            // colEmail
            //
            colEmail.DataPropertyName = "Email";
            colEmail.FillWeight = 30F;
            colEmail.HeaderText = "Email";
            colEmail.Name = "colEmail";
            colEmail.ReadOnly = true;
            //
            // colAge
            //
            ageStyle.NullValue = "—";
            colAge.DataPropertyName = "Age";
            colAge.DefaultCellStyle = ageStyle;
            colAge.FillWeight = 8F;
            colAge.HeaderText = "Age";
            colAge.Name = "colAge";
            colAge.ReadOnly = true;
            //
            // colStatus
            //
            colStatus.DataPropertyName = "AccountStatus";
            colStatus.FillWeight = 12F;
            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            //
            // colCreated
            //
            createdStyle.Format = "dd MMM yyyy";
            colCreated.DataPropertyName = "CreatedAt";
            colCreated.DefaultCellStyle = createdStyle;
            colCreated.FillWeight = 14F;
            colCreated.HeaderText = "Created";
            colCreated.Name = "colCreated";
            colCreated.ReadOnly = true;
            //
            // colWorkouts
            //
            colWorkouts.DataPropertyName = "WorkoutCount";
            colWorkouts.FillWeight = 10F;
            colWorkouts.HeaderText = "Workouts";
            colWorkouts.Name = "colWorkouts";
            colWorkouts.ReadOnly = true;
            //
            // colWeightEntries
            //
            colWeightEntries.DataPropertyName = "WeightEntryCount";
            colWeightEntries.FillWeight = 12F;
            colWeightEntries.HeaderText = "Weight entries";
            colWeightEntries.Name = "colWeightEntries";
            colWeightEntries.ReadOnly = true;
            //
            // pnlSide
            //
            pnlSide.BackColor = Color.Transparent;
            pnlSide.Controls.Add(gbUserOptions);
            pnlSide.Controls.Add(lblSelectUser);
            pnlSide.Dock = DockStyle.Fill;
            pnlSide.Location = new Point(1252, 8);
            pnlSide.Margin = new Padding(8);
            pnlSide.Name = "pnlSide";
            pnlSide.Size = new Size(404, 544);
            pnlSide.TabIndex = 1;
            //
            // gbUserOptions
            //
            gbUserOptions.BackColor = Color.Transparent;
            gbUserOptions.Controls.Add(tlpUser);
            gbUserOptions.Controls.Add(tlpActions);
            gbUserOptions.Dock = DockStyle.Fill;
            gbUserOptions.Font = new Font("Segoe UI Semibold", 12F);
            gbUserOptions.ForeColor = Color.FromArgb(15, 23, 42);
            gbUserOptions.Location = new Point(0, 0);
            gbUserOptions.Name = "gbUserOptions";
            gbUserOptions.Padding = new Padding(20, 0, 20, 20);
            gbUserOptions.Size = new Size(404, 544);
            gbUserOptions.TabIndex = 0;
            gbUserOptions.TabStop = false;
            gbUserOptions.Text = "User options";
            gbUserOptions.Visible = false;
            //
            // tlpUser
            //
            tlpUser.BackColor = Color.Transparent;
            tlpUser.ColumnCount = 1;
            tlpUser.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpUser.Controls.Add(lblName, 0, 0);
            tlpUser.Controls.Add(lblEmail, 0, 1);
            tlpUser.Controls.Add(lblCreatedCaption, 0, 2);
            tlpUser.Controls.Add(lblCreated, 0, 3);
            tlpUser.Controls.Add(lblDeletedCaption, 0, 4);
            tlpUser.Controls.Add(lblDeleted, 0, 5);
            tlpUser.Dock = DockStyle.Fill;
            tlpUser.Location = new Point(20, 56);
            tlpUser.Name = "tlpUser";
            tlpUser.RowCount = 7;
            tlpUser.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpUser.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlpUser.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpUser.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlpUser.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpUser.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlpUser.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpUser.Size = new Size(364, 312);
            tlpUser.TabIndex = 0;
            //
            // lblName
            //
            lblName.AutoEllipsis = true;
            lblName.Dock = DockStyle.Fill;
            lblName.Font = new Font("Segoe UI Semibold", 16F);
            lblName.ForeColor = Color.FromArgb(15, 23, 42);
            lblName.Margin = new Padding(0);
            lblName.Name = "lblName";
            lblName.TabIndex = 0;
            lblName.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblEmail
            //
            lblEmail.AutoEllipsis = true;
            lblEmail.Dock = DockStyle.Fill;
            lblEmail.Font = new Font("Segoe UI", 10.5F);
            lblEmail.ForeColor = Color.FromArgb(100, 116, 139);
            lblEmail.Margin = new Padding(0);
            lblEmail.Name = "lblEmail";
            lblEmail.TabIndex = 1;
            lblEmail.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblCreatedCaption
            //
            lblCreatedCaption.Dock = DockStyle.Fill;
            lblCreatedCaption.Font = new Font("Segoe UI Semibold", 9F);
            lblCreatedCaption.ForeColor = Color.FromArgb(100, 116, 139);
            lblCreatedCaption.Margin = new Padding(0);
            lblCreatedCaption.Name = "lblCreatedCaption";
            lblCreatedCaption.TabIndex = 2;
            lblCreatedCaption.Text = "CREATED";
            lblCreatedCaption.TextAlign = ContentAlignment.BottomLeft;
            //
            // lblCreated
            //
            lblCreated.AutoEllipsis = true;
            lblCreated.Dock = DockStyle.Fill;
            lblCreated.Font = new Font("Segoe UI", 11F);
            lblCreated.ForeColor = Color.FromArgb(30, 41, 59);
            lblCreated.Margin = new Padding(0);
            lblCreated.Name = "lblCreated";
            lblCreated.TabIndex = 3;
            lblCreated.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblDeletedCaption
            //
            lblDeletedCaption.Dock = DockStyle.Fill;
            lblDeletedCaption.Font = new Font("Segoe UI Semibold", 9F);
            lblDeletedCaption.ForeColor = Color.FromArgb(220, 38, 38);
            lblDeletedCaption.Margin = new Padding(0);
            lblDeletedCaption.Name = "lblDeletedCaption";
            lblDeletedCaption.TabIndex = 4;
            lblDeletedCaption.Text = "DELETED";
            lblDeletedCaption.TextAlign = ContentAlignment.BottomLeft;
            lblDeletedCaption.Visible = false;
            //
            // lblDeleted
            //
            lblDeleted.AutoEllipsis = true;
            lblDeleted.Dock = DockStyle.Fill;
            lblDeleted.Font = new Font("Segoe UI", 11F);
            lblDeleted.ForeColor = Color.FromArgb(30, 41, 59);
            lblDeleted.Margin = new Padding(0);
            lblDeleted.Name = "lblDeleted";
            lblDeleted.TabIndex = 5;
            lblDeleted.TextAlign = ContentAlignment.MiddleLeft;
            lblDeleted.Visible = false;
            //
            // tlpActions
            //
            tlpActions.BackColor = Color.White;
            tlpActions.ColumnCount = 1;
            tlpActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpActions.Controls.Add(btnActivate, 0, 0);
            tlpActions.Controls.Add(btnDeactivate, 0, 1);
            tlpActions.Controls.Add(btnDelete, 0, 2);
            tlpActions.Dock = DockStyle.Bottom;
            tlpActions.Location = new Point(20, 368);
            tlpActions.Name = "tlpActions";
            tlpActions.RowCount = 3;
            tlpActions.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tlpActions.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tlpActions.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tlpActions.Size = new Size(364, 156);
            tlpActions.TabIndex = 1;
            //
            // btnActivate
            //
            btnActivate.BackColor = Color.FromArgb(16, 185, 129);
            btnActivate.CornerRadius = 8;
            btnActivate.DisabledBackColor = Color.FromArgb(226, 232, 240);
            btnActivate.DisabledForeColor = Color.FromArgb(148, 163, 184);
            btnActivate.Dock = DockStyle.Fill;
            btnActivate.FlatAppearance.BorderSize = 0;
            btnActivate.FlatStyle = FlatStyle.Flat;
            btnActivate.Font = new Font("Segoe UI Semibold", 10.5F);
            btnActivate.ForeColor = Color.White;
            btnActivate.HoverColor = Color.FromArgb(5, 150, 105);
            btnActivate.Margin = new Padding(0, 4, 0, 4);
            btnActivate.Name = "btnActivate";
            btnActivate.PressedColor = Color.FromArgb(4, 120, 87);
            btnActivate.TabIndex = 0;
            btnActivate.Text = "Activate user";
            btnActivate.UseVisualStyleBackColor = false;
            //
            // btnDeactivate
            //
            btnDeactivate.BackColor = Color.FromArgb(251, 191, 36);
            btnDeactivate.CornerRadius = 8;
            btnDeactivate.DisabledBackColor = Color.FromArgb(226, 232, 240);
            btnDeactivate.DisabledForeColor = Color.FromArgb(148, 163, 184);
            btnDeactivate.Dock = DockStyle.Fill;
            btnDeactivate.FlatAppearance.BorderSize = 0;
            btnDeactivate.FlatStyle = FlatStyle.Flat;
            btnDeactivate.Font = new Font("Segoe UI Semibold", 10.5F);
            btnDeactivate.ForeColor = Color.FromArgb(15, 23, 42);
            btnDeactivate.HoverColor = Color.FromArgb(245, 158, 11);
            btnDeactivate.Margin = new Padding(0, 4, 0, 4);
            btnDeactivate.Name = "btnDeactivate";
            btnDeactivate.PressedColor = Color.FromArgb(217, 119, 6);
            btnDeactivate.TabIndex = 1;
            btnDeactivate.Text = "Deactivate user";
            btnDeactivate.UseVisualStyleBackColor = false;
            //
            // btnDelete
            //
            btnDelete.BackColor = Color.FromArgb(239, 68, 68);
            btnDelete.CornerRadius = 8;
            btnDelete.DisabledBackColor = Color.FromArgb(226, 232, 240);
            btnDelete.DisabledForeColor = Color.FromArgb(148, 163, 184);
            btnDelete.Dock = DockStyle.Fill;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI Semibold", 10.5F);
            btnDelete.ForeColor = Color.White;
            btnDelete.HoverColor = Color.FromArgb(220, 38, 38);
            btnDelete.Margin = new Padding(0, 4, 0, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.PressedColor = Color.FromArgb(185, 28, 28);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete user";
            btnDelete.UseVisualStyleBackColor = false;
            //
            // lblSelectUser
            //
            lblSelectUser.Dock = DockStyle.Fill;
            lblSelectUser.Font = new Font("Segoe UI", 13F);
            lblSelectUser.ForeColor = Color.FromArgb(100, 116, 139);
            lblSelectUser.Location = new Point(0, 0);
            lblSelectUser.Name = "lblSelectUser";
            lblSelectUser.Size = new Size(404, 544);
            lblSelectUser.TabIndex = 1;
            lblSelectUser.Text = "Select a user";
            lblSelectUser.TextAlign = ContentAlignment.MiddleCenter;
            //
            // tmrSearch
            //
            tmrSearch.Interval = 400;
            tmrSearch.Tick += tmrSearch_Tick;
            //
            // DashboardView
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            Controls.Add(tlpMain);
            Controls.Add(tlpStats);
            Name = "DashboardView";
            Padding = new Padding(16);
            Size = new Size(1696, 812);
            tlpStats.ResumeLayout(false);
            tlpMain.ResumeLayout(false);
            pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgUsers).EndInit();
            pnlFilters.ResumeLayout(false);
            pnlFilters.PerformLayout();
            pnlPager.ResumeLayout(false);
            pnlSide.ResumeLayout(false);
            gbUserOptions.ResumeLayout(false);
            tlpUser.ResumeLayout(false);
            tlpActions.ResumeLayout(false);
            ResumeLayout(false);
            tlpStats.PerformLayout();
            tlpMain.PerformLayout();
        }

        #endregion

        private TableLayoutPanel tlpStats;
        private Common.Controls.StatCard cardUsers;
        private Common.Controls.StatCard cardWorkouts;
        private Common.Controls.StatCard cardExercises;
        private Common.Controls.StatCard cardWeightEntries;
        private Common.Controls.StatCard cardMostCommonType;
        private Common.Controls.StatCard cardAverageAge;
        private TableLayoutPanel tlpMain;
        private Common.Controls.CardPanel pnlGridCard;
        private Panel pnlFilters;
        private TextBox tbSearch;
        private ComboBox cbSort;
        private CheckBox chkDeleted;
        private Common.Controls.RoundedButton btnRefetch;
        private Panel pnlPager;
        private Label lblTotal;
        private Common.Controls.RoundedButton btnPrev;
        private Label lblPage;
        private Common.Controls.RoundedButton btnNext;
        private DataGridView dgUsers;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colAge;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colCreated;
        private DataGridViewTextBoxColumn colWorkouts;
        private DataGridViewTextBoxColumn colWeightEntries;
        private Panel pnlSide;
        private Label lblSelectUser;
        private Common.Controls.CardGroupBox gbUserOptions;
        private TableLayoutPanel tlpUser;
        private Label lblName;
        private Label lblEmail;
        private Label lblCreatedCaption;
        private Label lblCreated;
        private Label lblDeletedCaption;
        private Label lblDeleted;
        private TableLayoutPanel tlpActions;
        private Common.Controls.RoundedButton btnActivate;
        private Common.Controls.RoundedButton btnDeactivate;
        private Common.Controls.RoundedButton btnDelete;
        private System.Windows.Forms.Timer tmrSearch;
    }
}
