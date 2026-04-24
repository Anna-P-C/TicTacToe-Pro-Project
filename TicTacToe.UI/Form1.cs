using TicTacToe.UI.Core;
using TicTacToe.UI.Models;
using TicTacToe.UI.Services;
using System;
using System.Windows.Forms;

namespace TicTacToe.UI
{
    public partial class Form1 : Form
    {
        private readonly IGameLogic _gameEngine;
        private Player _player1;
        private Player _player2;
        private Player _currentPlayer;
        public Form1()
        {
            InitializeComponent();

            _gameEngine = new GameEngine();
            _player1 = new Player("Гравець 1", 'X');
            _player2 = new Player("Гравець 2", 'O');
            _currentPlayer = _player1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender is not Button button) return;

            int row = int.Parse(button.Name.Substring(3, 1));
            int col = int.Parse(button.Name.Substring(4, 1));

            bool moveSuccess = _gameEngine.MakeMove(row, col, _currentPlayer.Symbol);

            if (moveSuccess)
            {
                button.Text = _currentPlayer.Symbol.ToString();
                button.Enabled = false;

                char winnerSymbol = _gameEngine.CheckWinner();

                if (winnerSymbol != '\0')
                {
                    //Singleton ScoreService
                    ScoreService.Instance.SaveScore(_currentPlayer);

                    string message = $"Вітаємо! {(_currentPlayer.Symbol == 'X' ? "Хрестики" : "Нулики")} перемогли!\n" +
                                     $"Результат збережено в рейтинг.";

                    MessageBox.Show(message, "Кінець матчу", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Restart();
                }
                else
                {
                    
                    if (_currentPlayer == _player1)
                    {
                        _currentPlayer = _player2;
                    }
                    else
                    {
                        _currentPlayer = _player1;
                    }
                }
            }
        }
    }
}