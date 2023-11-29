namespace _3_XO
{
    partial class Game
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
            MBGStripe = new PictureBox();
            MButtonStart = new Button();
            MButtonSettings = new Button();
            MButtonAbout = new Button();
            MButtonExit = new Button();
            MBGMain = new PictureBox();
            SBGStripe = new PictureBox();
            vsComputer = new CheckBox();
            Hints = new CheckBox();
            SettingsApply = new Button();
            ((System.ComponentModel.ISupportInitialize)MBGStripe).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MBGMain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SBGStripe).BeginInit();
            SuspendLayout();
            // 
            // MBGStripe
            // 
            MBGStripe.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            MBGStripe.BackColor = Color.MidnightBlue;
            MBGStripe.Location = new Point(230, 0);
            MBGStripe.Margin = new Padding(0);
            MBGStripe.Name = "MBGStripe";
            MBGStripe.Size = new Size(500, 1080);
            MBGStripe.TabIndex = 0;
            MBGStripe.TabStop = false;
            MBGStripe.Tag = "Menu";
            // 
            // MButtonStart
            // 
            MButtonStart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            MButtonStart.AutoSize = true;
            MButtonStart.BackColor = Color.MidnightBlue;
            MButtonStart.BackgroundImageLayout = ImageLayout.None;
            MButtonStart.FlatAppearance.BorderSize = 0;
            MButtonStart.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
            MButtonStart.FlatStyle = FlatStyle.Flat;
            MButtonStart.Font = new Font("Modern No. 20", 28F, FontStyle.Bold, GraphicsUnit.Point);
            MButtonStart.ForeColor = Color.Tomato;
            MButtonStart.Location = new Point(230, 220);
            MButtonStart.Margin = new Padding(0);
            MButtonStart.Name = "MButtonStart";
            MButtonStart.Size = new Size(500, 160);
            MButtonStart.TabIndex = 1;
            MButtonStart.Tag = "Menu";
            MButtonStart.Text = "Играть";
            MButtonStart.UseVisualStyleBackColor = false;
            MButtonStart.Click += MBStart_Click;
            // 
            // MButtonSettings
            // 
            MButtonSettings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            MButtonSettings.AutoSize = true;
            MButtonSettings.BackColor = Color.MidnightBlue;
            MButtonSettings.BackgroundImageLayout = ImageLayout.None;
            MButtonSettings.FlatAppearance.BorderSize = 0;
            MButtonSettings.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
            MButtonSettings.FlatStyle = FlatStyle.Flat;
            MButtonSettings.Font = new Font("Modern No. 20", 28F, FontStyle.Bold, GraphicsUnit.Point);
            MButtonSettings.ForeColor = Color.Tomato;
            MButtonSettings.Location = new Point(230, 380);
            MButtonSettings.Margin = new Padding(0);
            MButtonSettings.Name = "MButtonSettings";
            MButtonSettings.Size = new Size(500, 160);
            MButtonSettings.TabIndex = 1;
            MButtonSettings.Tag = "Menu";
            MButtonSettings.Text = "Настройки";
            MButtonSettings.UseVisualStyleBackColor = false;
            MButtonSettings.Click += MBSettings_Click;
            // 
            // MButtonAbout
            // 
            MButtonAbout.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            MButtonAbout.AutoSize = true;
            MButtonAbout.BackColor = Color.MidnightBlue;
            MButtonAbout.BackgroundImageLayout = ImageLayout.None;
            MButtonAbout.FlatAppearance.BorderSize = 0;
            MButtonAbout.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
            MButtonAbout.FlatStyle = FlatStyle.Flat;
            MButtonAbout.Font = new Font("Modern No. 20", 28F, FontStyle.Bold, GraphicsUnit.Point);
            MButtonAbout.ForeColor = Color.Tomato;
            MButtonAbout.Location = new Point(230, 540);
            MButtonAbout.Margin = new Padding(0);
            MButtonAbout.Name = "MButtonAbout";
            MButtonAbout.Size = new Size(500, 160);
            MButtonAbout.TabIndex = 1;
            MButtonAbout.Tag = "Menu";
            MButtonAbout.Text = "Об игре";
            MButtonAbout.UseVisualStyleBackColor = false;
            MButtonAbout.Click += MBAbout_Click;
            // 
            // MButtonExit
            // 
            MButtonExit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            MButtonExit.AutoSize = true;
            MButtonExit.BackColor = Color.MidnightBlue;
            MButtonExit.BackgroundImageLayout = ImageLayout.None;
            MButtonExit.FlatAppearance.BorderSize = 0;
            MButtonExit.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
            MButtonExit.FlatStyle = FlatStyle.Flat;
            MButtonExit.Font = new Font("Modern No. 20", 28F, FontStyle.Bold, GraphicsUnit.Point);
            MButtonExit.ForeColor = Color.Tomato;
            MButtonExit.Location = new Point(230, 700);
            MButtonExit.Margin = new Padding(0);
            MButtonExit.Name = "MButtonExit";
            MButtonExit.Size = new Size(500, 160);
            MButtonExit.TabIndex = 1;
            MButtonExit.Tag = "Menu";
            MButtonExit.Text = "Выйти";
            MButtonExit.UseVisualStyleBackColor = false;
            MButtonExit.Click += MBExit_Click;
            // 
            // MBGMain
            // 
            MBGMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MBGMain.BackColor = Color.DarkBlue;
            MBGMain.Location = new Point(0, 0);
            MBGMain.Name = "MBGMain";
            MBGMain.Size = new Size(192, 108);
            MBGMain.TabIndex = 2;
            MBGMain.TabStop = false;
            MBGMain.Tag = "Menu";
            // 
            // SBGStripe
            // 
            SBGStripe.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            SBGStripe.BackColor = Color.MidnightBlue;
            SBGStripe.Enabled = false;
            SBGStripe.Location = new Point(1190, 0);
            SBGStripe.Margin = new Padding(0);
            SBGStripe.Name = "SBGStripe";
            SBGStripe.Size = new Size(500, 1080);
            SBGStripe.TabIndex = 3;
            SBGStripe.TabStop = false;
            SBGStripe.Tag = "Menu";
            SBGStripe.Visible = false;
            // 
            // vsComputer
            // 
            vsComputer.AutoSize = true;
            vsComputer.BackColor = Color.MidnightBlue;
            vsComputer.Checked = true;
            vsComputer.CheckState = CheckState.Checked;
            vsComputer.Enabled = false;
            vsComputer.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            vsComputer.ForeColor = Color.Tomato;
            vsComputer.Location = new Point(1339, 280);
            vsComputer.Margin = new Padding(0);
            vsComputer.Name = "vsComputer";
            vsComputer.Size = new Size(218, 45);
            vsComputer.TabIndex = 4;
            vsComputer.Text = "vs Computer";
            vsComputer.UseVisualStyleBackColor = false;
            vsComputer.Visible = false;
            vsComputer.CheckedChanged += vsComputer_CheckedChanged;
            // 
            // Hints
            // 
            Hints.AutoSize = true;
            Hints.BackColor = Color.MidnightBlue;
            Hints.Enabled = false;
            Hints.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            Hints.ForeColor = Color.Tomato;
            Hints.Location = new Point(1339, 440);
            Hints.Margin = new Padding(0);
            Hints.Name = "Hints";
            Hints.Size = new Size(200, 45);
            Hints.TabIndex = 5;
            Hints.Text = "Show Hints";
            Hints.UseVisualStyleBackColor = false;
            Hints.Visible = false;
            // 
            // SettingsApply
            // 
            SettingsApply.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            SettingsApply.AutoSize = true;
            SettingsApply.BackColor = Color.MidnightBlue;
            SettingsApply.BackgroundImageLayout = ImageLayout.None;
            SettingsApply.Enabled = false;
            SettingsApply.FlatAppearance.BorderSize = 0;
            SettingsApply.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
            SettingsApply.FlatStyle = FlatStyle.Flat;
            SettingsApply.Font = new Font("Modern No. 20", 28F, FontStyle.Bold, GraphicsUnit.Point);
            SettingsApply.ForeColor = Color.Tomato;
            SettingsApply.Location = new Point(1190, 700);
            SettingsApply.Margin = new Padding(0);
            SettingsApply.Name = "SettingsApply";
            SettingsApply.Size = new Size(500, 160);
            SettingsApply.TabIndex = 6;
            SettingsApply.Tag = "Menu";
            SettingsApply.Text = "Применить";
            SettingsApply.UseVisualStyleBackColor = false;
            SettingsApply.Visible = false;
            SettingsApply.Click += SettingsApply_Click;
            // 
            // Game
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.Disable;
            ClientSize = new Size(1920, 1080);
            ControlBox = false;
            Controls.Add(SettingsApply);
            Controls.Add(Hints);
            Controls.Add(vsComputer);
            Controls.Add(MButtonExit);
            Controls.Add(MButtonAbout);
            Controls.Add(MButtonSettings);
            Controls.Add(MButtonStart);
            Controls.Add(MBGStripe);
            Controls.Add(SBGStripe);
            Controls.Add(MBGMain);
            Cursor = Cursors.Cross;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            MinimizeBox = false;
            Name = "Game";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Game";
            WindowState = FormWindowState.Maximized;
            Load += Game_Load;
            Paint += Game_Paint;
            KeyDown += Game_KeyDown;
            KeyPress += Game_KeyPress;
            MouseClick += Game_MouseClick;
            ((System.ComponentModel.ISupportInitialize)MBGStripe).EndInit();
            ((System.ComponentModel.ISupportInitialize)MBGMain).EndInit();
            ((System.ComponentModel.ISupportInitialize)SBGStripe).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox MBGMain;
        private PictureBox MBGStripe;
        private Button MButtonStart;
        private Button MButtonSettings;
        private Button MButtonAbout;
        private Button MButtonExit;
        private PictureBox SBGStripe;
        private CheckBox vsComputer;
        private CheckBox Hints;
        private Button SettingsApply;
    }
}