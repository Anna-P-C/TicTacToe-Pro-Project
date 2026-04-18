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
            _player1 = new Player("ֳנאגוצü 1", 'X');
            _player2 = new Player("ֳנאגוצü 2", 'O');
            _currentPlayer = _player1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int row = int.Parse(button.Name.Substring(3, 1));
            int col = int.Parse(button.Name.Substring(4, 1));

            if (_gameEngine.MakeMove(row, col, _currentPlayer.Symbol))
            {
                button.Text = _currentPlayer.Symbol.ToString();
                button.Enabled = false; 

                char winner = _gameEngine.CheckWinner();
                if (winner != '\0')
                {
                    MessageBox.Show($"ֿונול³ד דנאגוצü: {winner}!");
                    Application.Restart();
                }
                else
                {
                    _currentPlayer = (_currentPlayer == _player1) ? _player2 : _player1;
                }
            }
        }
    }
}