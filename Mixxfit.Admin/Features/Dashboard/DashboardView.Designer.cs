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
            tlpStats = new TableLayoutPanel();
            cardUsers = new Mixxfit.Admin.Common.Controls.StatCard();
            cardWorkouts = new Mixxfit.Admin.Common.Controls.StatCard();
            cardExercises = new Mixxfit.Admin.Common.Controls.StatCard();
            cardWeightEntries = new Mixxfit.Admin.Common.Controls.StatCard();
            cardMostCommonType = new Mixxfit.Admin.Common.Controls.StatCard();
            cardAverageAge = new Mixxfit.Admin.Common.Controls.StatCard();
            dgUsers = new DataGridView();
            label1 = new Label();
            groupBox1 = new GroupBox();
            tlpStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgUsers).BeginInit();
            groupBox1.SuspendLayout();
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
            tlpStats.Size = new Size(976, 260);
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
            cardUsers.Size = new Size(309, 114);
            cardUsers.TabIndex = 0;
            cardUsers.Title = "Total users";
            // 
            // cardWorkouts
            // 
            cardWorkouts.BackColor = Color.Transparent;
            cardWorkouts.Dock = DockStyle.Fill;
            cardWorkouts.Location = new Point(333, 8);
            cardWorkouts.Margin = new Padding(8);
            cardWorkouts.MinimumSize = new Size(120, 90);
            cardWorkouts.Name = "cardWorkouts";
            cardWorkouts.Padding = new Padding(20, 28, 16, 12);
            cardWorkouts.Size = new Size(309, 114);
            cardWorkouts.TabIndex = 1;
            cardWorkouts.Title = "Total workouts";
            // 
            // cardExercises
            // 
            cardExercises.BackColor = Color.Transparent;
            cardExercises.Dock = DockStyle.Fill;
            cardExercises.Location = new Point(658, 8);
            cardExercises.Margin = new Padding(8);
            cardExercises.MinimumSize = new Size(120, 90);
            cardExercises.Name = "cardExercises";
            cardExercises.Padding = new Padding(20, 28, 16, 12);
            cardExercises.Size = new Size(310, 114);
            cardExercises.TabIndex = 2;
            cardExercises.Title = "Total exercises";
            // 
            // cardWeightEntries
            // 
            cardWeightEntries.BackColor = Color.Transparent;
            cardWeightEntries.Dock = DockStyle.Fill;
            cardWeightEntries.Location = new Point(8, 138);
            cardWeightEntries.Margin = new Padding(8);
            cardWeightEntries.MinimumSize = new Size(120, 90);
            cardWeightEntries.Name = "cardWeightEntries";
            cardWeightEntries.Padding = new Padding(20, 28, 16, 12);
            cardWeightEntries.Size = new Size(309, 114);
            cardWeightEntries.TabIndex = 3;
            cardWeightEntries.Title = "Total weight entries";
            // 
            // cardMostCommonType
            // 
            cardMostCommonType.BackColor = Color.Transparent;
            cardMostCommonType.Dock = DockStyle.Fill;
            cardMostCommonType.Location = new Point(333, 138);
            cardMostCommonType.Margin = new Padding(8);
            cardMostCommonType.MinimumSize = new Size(120, 90);
            cardMostCommonType.Name = "cardMostCommonType";
            cardMostCommonType.Padding = new Padding(20, 28, 16, 12);
            cardMostCommonType.Size = new Size(309, 114);
            cardMostCommonType.TabIndex = 4;
            cardMostCommonType.Title = "Most common exercise type";
            // 
            // cardAverageAge
            // 
            cardAverageAge.BackColor = Color.Transparent;
            cardAverageAge.Dock = DockStyle.Fill;
            cardAverageAge.Location = new Point(658, 138);
            cardAverageAge.Margin = new Padding(8);
            cardAverageAge.MinimumSize = new Size(120, 90);
            cardAverageAge.Name = "cardAverageAge";
            cardAverageAge.Padding = new Padding(20, 28, 16, 12);
            cardAverageAge.Size = new Size(310, 114);
            cardAverageAge.TabIndex = 5;
            cardAverageAge.Title = "Average user age";
            // 
            // dgUsers
            // 
            dgUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgUsers.Location = new Point(24, 326);
            dgUsers.Name = "dgUsers";
            dgUsers.Size = new Size(800, 325);
            dgUsers.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(99, 38);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(674, 312);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(315, 339);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            groupBox1.Visible = false;
            // 
            // DashboardView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            Controls.Add(groupBox1);
            Controls.Add(dgUsers);
            Controls.Add(tlpStats);
            Name = "DashboardView";
            Padding = new Padding(16);
            Size = new Size(1008, 670);
            tlpStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgUsers).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpStats;
        private Common.Controls.StatCard cardUsers;
        private Common.Controls.StatCard cardWorkouts;
        private Common.Controls.StatCard cardExercises;
        private Common.Controls.StatCard cardWeightEntries;
        private Common.Controls.StatCard cardMostCommonType;
        private Common.Controls.StatCard cardAverageAge;
        private DataGridView dgUsers;
        private Label label1;
        private GroupBox groupBox1;
    }
}
