using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;
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

        private IMoveStrategy _botStrategy;
        private bool _isVsComputer = true;
        private readonly ILogger _logger = new FileLogger();
        private readonly TournamentManager _tournamentManager = new TournamentManager();

        public Form1()
        {
            InitializeComponent();

            _gameEngine = new GameEngine();
            _player1 = new Player("Гравець", 'X');
            _player2 = new Player("Комп'ютер", 'O');
            _currentPlayer = _player1;

            _botStrategy = StrategyFactory.CreateStrategy(1);
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
                    ScoreService.Instance.SaveScore(_currentPlayer);
                    MessageBox.Show($"Переміг {_currentPlayer.Name}!", "Кінець", MessageBoxButtons.OK);
                    Application.Restart();
                    return false;
                }

                if (IsBoardFull())
                {
                    MessageBox.Show("Нічия!", "Кінець", MessageBoxButtons.OK);
                    Application.Restart();
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
            _tournamentManager.RegisterWin(winner);
            _logger.LogInfo($"Кінець раунду. Переможець: {winner}. Поточні бали: {_tournamentManager.TotalScore}");

            if (_tournamentManager.IsTournamentActive && winner == 'X')
            {
                MessageBox.Show($"Вітаю! Ти пройшла рівень {_tournamentManager.GetCurrentDifficultyName()}! Наступний етап чекає.");
                PrepareNextRound();
            }
            else
            {
                string resultMsg = winner == 'X' ? "Ти стала чемпіоном!" : "Бот виявився сильнішим. Спробуй ще раз!";
                MessageBox.Show($"{resultMsg}\nТвій фінальний рахунок: {_tournamentManager.TotalScore}");


                ShowSaveRecordWindow(_tournamentManager.TotalScore);
                Application.Restart();
            }
        }
        private void PrepareNextRound()
        {
            _gameEngine.ResetBoard();
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

            string playerName = "Гравець";
            ScoreService.Instance.SaveScore(new Player(playerName, 'X'));
            MessageBox.Show($"Ваш результат {score} збережено!");
        }
        private void UpdateStatusLabel()
        {

            lblTournamentInfo.Text = $"Раунд: {_tournamentManager.CurrentRound} | Бали: {_tournamentManager.TotalScore}";

            
            pbProgress.Value = (_tournamentManager.CurrentRound - 1) * 33 + 10;

            _logger.LogInfo($"Інтерфейс оновлено: Раунд {_tournamentManager.CurrentRound}");
        }
    }
}
