using System.Windows.Forms;
using System.Drawing;

namespace TicTacToe.UI
{
    partial class Form1
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
            btn00 = new Button();
            btn01 = new Button();
            btn02 = new Button();
            btn10 = new Button();
            btn11 = new Button();
            btn20 = new Button();
            btn21 = new Button();
            btn22 = new Button();
            btn12 = new Button();
            lblTournamentInfo = new Label();
            pbProgress = new ProgressBar();
            btnUndo = new Button();
            btnRedo = new Button();
            SuspendLayout();
            // 
            // btn00
            // 
            btn00.Location = new Point(61, 43);
            btn00.Name = "btn00";
            btn00.Size = new Size(75, 75);
            btn00.TabIndex = 0;
            btn00.UseVisualStyleBackColor = true;
            btn00.Click += OnButtonClick;
            // 
            // btn01
            // 
            btn01.Location = new Point(248, 43);
            btn01.Name = "btn01";
            btn01.Size = new Size(75, 75);
            btn01.TabIndex = 1;
            btn01.UseVisualStyleBackColor = true;
            btn01.Click += OnButtonClick;
            // 
            // btn02
            // 
            btn02.Location = new Point(414, 43);
            btn02.Name = "btn02";
            btn02.Size = new Size(75, 75);
            btn02.TabIndex = 2;
            btn02.UseVisualStyleBackColor = true;
            btn02.Click += OnButtonClick;
            // 
            // btn10
            // 
            btn10.Location = new Point(61, 187);
            btn10.Name = "btn10";
            btn10.Size = new Size(75, 75);
            btn10.TabIndex = 3;
            btn10.UseVisualStyleBackColor = true;
            btn10.Click += OnButtonClick;
            // 
            // btn11
            // 
            btn11.Location = new Point(248, 187);
            btn11.Name = "btn11";
            btn11.Size = new Size(75, 75);
            btn11.TabIndex = 4;
            btn11.UseVisualStyleBackColor = true;
            btn11.Click += OnButtonClick;
            // 
            // btn20
            // 
            btn20.Location = new Point(61, 319);
            btn20.Name = "btn20";
            btn20.Size = new Size(75, 75);
            btn20.TabIndex = 5;
            btn20.UseVisualStyleBackColor = true;
            btn20.Click += OnButtonClick;
            // 
            // btn21
            // 
            btn21.Location = new Point(248, 319);
            btn21.Name = "btn21";
            btn21.Size = new Size(75, 75);
            btn21.TabIndex = 6;
            btn21.UseVisualStyleBackColor = true;
            btn21.Click += OnButtonClick;
            // 
            // btn22
            // 
            btn22.Location = new Point(414, 333);
            btn22.Name = "btn22";
            btn22.Size = new Size(75, 75);
            btn22.TabIndex = 7;
            btn22.UseVisualStyleBackColor = true;
            btn22.Click += OnButtonClick;
            // 
            // btn12
            // 
            btn12.Location = new Point(414, 187);
            btn12.Name = "btn12";
            btn12.Size = new Size(75, 75);
            btn12.TabIndex = 8;
            btn12.UseVisualStyleBackColor = true;
            btn12.Click += OnButtonClick;
            // 
            // lblTournamentInfo
            // 
            lblTournamentInfo.AutoSize = true;
            lblTournamentInfo.Location = new Point(578, 73);
            lblTournamentInfo.Name = "lblTournamentInfo";
            lblTournamentInfo.Size = new Size(0, 20);
            lblTournamentInfo.TabIndex = 9;
            // 
            // pbProgress
            // 
            pbProgress.Location = new Point(537, 122);
            pbProgress.Name = "pbProgress";
            pbProgress.Size = new Size(125, 29);
            pbProgress.TabIndex = 10;
            // 
            // btnUndo
            // 
            btnUndo.Location = new Point(83, 466);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(128, 29);
            btnUndo.TabIndex = 11;
            btnUndo.Text = "Відмінити хід";
            btnUndo.UseVisualStyleBackColor = true;
            btnUndo.Click += btnUndo_Click_1;
            // 
            // btnRedo
            // 
            btnRedo.Location = new Point(370, 466);
            btnRedo.Name = "btnRedo";
            btnRedo.Size = new Size(119, 29);
            btnRedo.TabIndex = 12;
            btnRedo.Text = "Повернути хід";
            btnRedo.UseVisualStyleBackColor = true;
            btnRedo.Click += btnRedo_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(707, 532);
            Controls.Add(btnRedo);
            Controls.Add(btnUndo);
            Controls.Add(pbProgress);
            Controls.Add(lblTournamentInfo);
            Controls.Add(btn12);
            Controls.Add(btn22);
            Controls.Add(btn21);
            Controls.Add(btn20);
            Controls.Add(btn11);
            Controls.Add(btn10);
            Controls.Add(btn02);
            Controls.Add(btn01);
            Controls.Add(btn00);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn00;
        private Button btn01;
        private Button btn02;
        private Button btn10;
        private Button btn11;
        private Button btn20;
        private Button btn21;
        private Button btn22;
        private Button btn12;
        private Label lblTournamentInfo;
        private ProgressBar pbProgress;
        private Button btnUndo;
        private Button btnRedo;
    }
}
