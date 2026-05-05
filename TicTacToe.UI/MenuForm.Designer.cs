namespace TicTacToe.UI
{
    partial class MenuForm
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
            lblTitle = new Label();
            txtName = new TextBox();
            label2F2 = new Label();
            btnStart = new Button();
            dgvLeaderboard = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvLeaderboard).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(327, 26);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(133, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Хрестики-Нулики:";
            // 
            // txtName
            // 
            txtName.Location = new Point(62, 262);
            txtName.Name = "txtName";
            txtName.Size = new Size(125, 27);
            txtName.TabIndex = 1;
            // 
            // label2F2
            // 
            label2F2.AutoSize = true;
            label2F2.Location = new Point(62, 230);
            label2F2.Name = "label2F2";
            label2F2.Size = new Size(133, 20);
            label2F2.TabIndex = 2;
            label2F2.Text = "Введіть ваше ім'я:";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(221, 335);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(94, 29);
            btnStart.TabIndex = 3;
            btnStart.Text = "Почати Турнір";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // dgvLeaderboard
            // 
            dgvLeaderboard.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLeaderboard.Location = new Point(432, 71);
            dgvLeaderboard.Name = "dgvLeaderboard";
            dgvLeaderboard.RowHeadersWidth = 51;
            dgvLeaderboard.Size = new Size(300, 188);
            dgvLeaderboard.TabIndex = 4;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvLeaderboard);
            Controls.Add(btnStart);
            Controls.Add(label2F2);
            Controls.Add(txtName);
            Controls.Add(lblTitle);
            Name = "MenuForm";
            Text = "MenuForm";
            Load += MenuForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLeaderboard).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private TextBox txtName;
        private Label label2F2;
        private Button btnStart;
        private DataGridView dgvLeaderboard;
    }
}