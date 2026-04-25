using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.UI.Core;
using TicTacToe.UI.Models;
using TicTacToe.UI.Services;

namespace TicTacToe.UI
{
    public partial class Form1 : Form
    {
        private readonly IGameLogic _gameEngine;
        private Player _player1;
        private Player _player2;
        private Player _currentPlayer;
        private string _playerName;

        private IMoveStrategy _botStrategy;
        private bool _isVsComputer = true;
        private readonly ILogger _logger = new FileLogger();
        private readonly TournamentManager _tournamentManager = new TournamentManager();

        public Form1(string playerName)
        {
            InitializeComponent();
            _playerName = playerName;

           
            this.Text = $"Гравець: {_playerName} | Рівень: 1";

            _gameEngine = new GameEngine();
            _player1 = new Player(_playerName, 'X'); 
            _player2 = new Player("Комп'ютер", 'O');
            _currentPlayer = _player1;

            _botStrategy = StrategyFactory.CreateStrategy(1);

            
            UpdateStatusLabel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender is not Button button) return;

            if (HandleMove(button))
            {
                if (_isVsComputer && _gameEngine.CheckWinner() == '\0' && !IsBoardFull())
                {
                    MakeBotMove();
                }
            }
        }

        private bool HandleMove(Button button)
        {
            int row = int.Parse(button.Name.Substring(3, 1));
            int col = int.Parse(button.Name.Substring(4, 1));

            if (_gameEngine.MakeMove(row, col, _currentPlayer.Symbol))
            {
                button.Text = _currentPlayer.Symbol.ToString();
                button.Enabled = false;

                char winnerSymbol = _gameEngine.CheckWinner();

                if (winnerSymbol != '\0')
                {
                    
                    EndGame(winnerSymbol);
                    return false;
                }

                if (IsBoardFull())
                {
                   
                    EndGame('\0');
                    return false;
                }

                _currentPlayer = (_currentPlayer == _player1) ? _player2 : _player1;
                return true;
            }
            return false;
        }

        private void MakeBotMove()
        {
            try
            {
                int currentLevel = _tournamentManager.CurrentRound;
                _botStrategy = StrategyFactory.CreateStrategy(currentLevel);

                _logger.LogInfo($"Бот використовує стратегію: {_botStrategy.GetStrategyName()}");

                var move = _botStrategy.GetNextMove(_gameEngine.GetBoard(), _player2.Symbol);

                string btnName = $"btn{move.row}{move.col}";
                Control[] controls = this.Controls.Find(btnName, true);

                if (controls.Length > 0 && controls[0] is Button botButton)
                {
                    HandleMove(botButton);
                    _logger.LogMove("Комп'ютер", move.row, move.col);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Помилка під час ходу бота", ex);
            }
        }

        private bool IsBoardFull()
        {
            var board = _gameEngine.GetBoard();
            foreach (var cell in board)
            {
                if (cell == '\0') return false;
            }
            return true;
        }

        private void EndGame(char winner)
        {
          
            if (winner == 'X') HighlightStatus(Color.Green);
            else if (winner == 'O') HighlightStatus(Color.Red);

            _tournamentManager.RegisterWin(winner);
            _logger.LogInfo($"Кінець раунду. Переможець: {winner}. Поточні бали: {_tournamentManager.TotalScore}");

           
            UpdateStatusLabel();

            if (_tournamentManager.IsTournamentActive && winner == 'X')
            {
                MessageBox.Show($"Вітаю! Ти пройшла рівень {_tournamentManager.CurrentRound - 1}! Наступний етап чекає.", "Раунд пройдено");
                PrepareNextRound();
                UpdateStatusLabel();
            }
            else
            {
                string resultMsg = winner == 'X' ? "Ти стала чемпіоном!" : (winner == '\0' ? "Нічия!" : "Бот виявився сильнішим.");
                MessageBox.Show($"{resultMsg}\nТвій фінальний рахунок: {_tournamentManager.TotalScore}", "Фінал турніру");

                ShowSaveRecordWindow(_tournamentManager.TotalScore);

                
                this.Close();
            }
        }

        private void PrepareNextRound()
        {
            _gameEngine.ResetBoard();
            _currentPlayer = _player1; 

            foreach (Control control in this.Controls)
            {
                if (control is Button btn && btn.Name.StartsWith("btn"))
                {
                    btn.Text = "";
                    btn.Enabled = true;
                    btn.BackColor = Color.White;
                }
            }
            _logger.LogInfo($"Підготовка до раунду {_tournamentManager.CurrentRound}");
        }

        private void ShowSaveRecordWindow(int score)
        {
            ScoreService.Instance.SaveScore(new Player(_playerName, 'X'), _tournamentManager.TotalScore);
            MessageBox.Show($"Ваш результат {score} збережено!");
        }

        private void UpdateStatusLabel()
        {
           
            lblTournamentInfo.Text = $"Раунд: {_tournamentManager.CurrentRound} | Бали: {_tournamentManager.TotalScore}";

            this.Text = $"Гравець: {_playerName} | Раунд: {_tournamentManager.CurrentRound}";

          
            int progress = (_tournamentManager.CurrentRound - 1) * 33;
            if (!_tournamentManager.IsTournamentActive && _tournamentManager.TotalScore > 0) progress = 100;

            pbProgress.Value = Math.Min(progress, 100);

            _logger.LogInfo($"Оновлено UI. Поточний прогрес турніру: {progress}%");
        }

        private async void HighlightStatus(Color highlightColor)
        {
            try
            {
                Color originalColor = lblTournamentInfo.ForeColor;
                lblTournamentInfo.ForeColor = highlightColor;

                _logger.LogInfo($"Візуальний ефект: зміна кольору на {highlightColor.Name}");

                await Task.Delay(1500);

                
                if (!lblTournamentInfo.IsDisposed)
                {
                    lblTournamentInfo.ForeColor = originalColor;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Помилка при виконанні ефекту підсвічування", ex);
            }
        }
    }
}