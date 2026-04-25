using System;
using System.Drawing;
using System.Windows.Forms;
using TicTacToe.UI.Services;
using TicTacToe.UI.Models;

namespace TicTacToe.UI
{
    public partial class LeaderboardForm : Form
    {
      
        private readonly ScoreService _scoreService;

        public LeaderboardForm()
        {
            InitializeComponent();

           
            _scoreService = ScoreService.Instance;

            LoadStatistics();
        }

      
        private void LoadStatistics()
        {
            
            var topPlayers = _scoreService.GetTopPlayers();

      
            ListBox listBox = new ListBox();
            listBox.Dock = DockStyle.Fill;
            listBox.Font = new Font("Segoe UI", 12, FontStyle.Regular);

            int rank = 1;
         
            foreach (var player in topPlayers)
            {
                listBox.Items.Add($"{rank}. {player.Name} — Перемог: {player.Score}");
                rank++;
            }

           
            this.Controls.Add(listBox);
            this.Text = "Рейтинг найкращих гравців";
            this.Size = new Size(300, 400);
        }
    }
}