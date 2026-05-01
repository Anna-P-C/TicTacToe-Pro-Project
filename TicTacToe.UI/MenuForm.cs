using System;
using System.Windows.Forms;
using TicTacToe.UI.Services;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.UI
{
    public partial class MenuForm : Form
    {
        private readonly ScoreService _scoreService = ScoreService.Instance;

        public MenuForm()
        {
            InitializeComponent();
            LoadTopPlayers();
        }

        private void LoadTopPlayers()
        {

            var topPlayers = _scoreService.GetPlayers()
                .OrderByDescending(p => p.Score)
                .Take(5)
                .ToList();

            dgvLeaderboard.DataSource = topPlayers;

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string playerName = txtName.Text.Trim();


            if (string.IsNullOrWhiteSpace(playerName))
            {
                MessageBox.Show("Будь ласка, введіть своє ім'я перед початком гри!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (playerName.Length < 2)
            {
                MessageBox.Show("Ім'я занадто коротке (мінімум 2 символи).", "Увага");
                return;
            }


            var gameForm = new Form1(playerName);
            this.Hide();
            gameForm.ShowDialog();


            this.Show();
            LoadTopPlayers();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

        }
    }
}