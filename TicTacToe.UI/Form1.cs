using TicTacToe.UI.Core;
using TicTacToe.UI.Models;
using TicTacToe.UI.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

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

        public Form1()
        {
            InitializeComponent();

            _gameEngine = new GameEngine();
            _player1 = new Player("Гравець", 'X');
            _player2 = new Player("Комп'ютер", 'O');
            _currentPlayer = _player1;

            _botStrategy = StrategyFactory.CreateStrategy("expert");
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
            var move = _botStrategy.GetNextMove(_gameEngine.GetBoard(), _player2.Symbol);
            string btnName = $"btn{move.row}{move.col}";

            Control[] controls = this.Controls.Find(btnName, true);
            if (controls.Length > 0 && controls[0] is Button botButton)
            {
                HandleMove(botButton);
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
    }
}