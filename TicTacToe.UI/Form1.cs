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
        private int _movesCounter = 0;

        private IMoveStrategy _botStrategy;
        private bool _isVsComputer = true;
        private readonly ILogger _logger = new FileLogger();
        private readonly TournamentManager _tournamentManager = new TournamentManager();
        private readonly CommandHistory _history = new CommandHistory();

        public Form1(string playerName)
        {
            InitializeComponent();
            _playerName = playerName;
            this.Text = $"TicTacToe - {_playerName}";

            _gameEngine = new GameEngine();
            _player1 = new Player(_playerName, 'X');
            _player2 = new Player("Êîìï'þòåð", 'O');
            _currentPlayer = _player1;

            _botStrategy = StrategyFactory.CreateStrategy(1);

            // ÑÒÀÐÒ ÀÍÀË²ÒÈÊÈ: Ïî÷èíàºìî â³äë³ê ÷àñó ãðè
            AnalyticsService.Instance.StartRoundTimer();

            UpdateStatusLabel();
            RefreshUndoRedoButtons();
        }

        // Öåé ìåòîä âèïðàâëÿº ïîìèëêó CS0103 ó Designer.cs
        private void Form1_Load(object sender, EventArgs e)
        {
            _logger.LogInfo("Ôîðìà ãðè óñï³øíî çàâàíòàæåíà.");
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
                _history.PushMove(_gameEngine.GetBoard(), row, col, _currentPlayer.Symbol);

                // ÐÅªÑÒÐÓªÌÎ Õ²Ä Â ÀÍÀË²ÒÈÖ²
                AnalyticsService.Instance.RegisterMove();

                if (_currentPlayer == _player1) _movesCounter++;

                button.Text = _currentPlayer.Symbol.ToString();
                button.Enabled = false;

                char winnerSymbol = _gameEngine.CheckWinner();
                RefreshUndoRedoButtons();

                if (winnerSymbol != '\0')
                {
                    _ = EndGame(winnerSymbol);
                    return false;
                }

                if (IsBoardFull())
                {
                    _ = EndGame('\0');
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
                var move = _botStrategy.GetNextMove(_gameEngine.GetBoard(), _player2.Symbol);
                string btnName = $"btn{move.row}{move.col}";
                Control[] controls = this.Controls.Find(btnName, true);

                if (controls.Length > 0 && controls[0] is Button botButton)
                {
                    HandleMove(botButton);
                }
            }
            catch (Exception ex) { _logger.LogError("Áîò ïîìèëèâñÿ", ex); }
        }

        private async Task EndGame(char winner)
        {
            if (winner == 'X')
            {
                HighlightStatus(Color.Green);
                await FlashGameBoard(Color.LimeGreen);
                AchievementManager.Instance.CheckFastWin(_movesCounter);
                AchievementManager.Instance.CheckCorners(_gameEngine.GetBoard(), 'X');
            }
            else if (winner == 'O')
            {
                HighlightStatus(Color.Red);
                await FlashGameBoard(Color.Tomato);
            }

            // ÇÁÅÐ²ÃÀªÌÎ ÄÀÍ² ÀÍÀË²ÒÈÊÈ ÒÀ ÃÅÍÅÐÓªÌÎ ÇÂ²Ò
            AnalyticsService.Instance.SaveSession(_playerName, _tournamentManager.TotalScore / 100, 0, _movesCounter);
            string advice = AnalyticsService.Instance.GetPerformanceAdvice(_tournamentManager.TotalScore / 100, 0);

            _movesCounter = 0;
            _history.Clear();
            _tournamentManager.RegisterWin(winner);
            UpdateStatusLabel();

            if (_tournamentManager.IsTournamentActive && winner == 'X')
            {
                MessageBox.Show($"{GetRoundTitle()} ïðîéäåíî!\n\nÏîðàäà: {advice}");
                PrepareNextRound();
                AnalyticsService.Instance.StartRoundTimer(); // Ñêèäàºìî òàéìåð äëÿ íîâîãî ðàóíäó
            }
            else
            {
                MessageBox.Show($"Ãðà çàâåðøåíà!\n\n{advice}");
                ScoreService.Instance.SaveScore(new Player(_playerName, 'X'), _tournamentManager.TotalScore);
                this.Close();
            }
        }

        private void btnUndo_Click_1(object sender, EventArgs e)
        {
            try
            {
                var botMove = _history.Undo();
                if (botMove != null) ApplyUndoToUI(botMove);

                var playerMove = _history.Undo();
                if (playerMove != null) ApplyUndoToUI(playerMove);

                _gameEngine.SetBoard(GetCurrentBoardFromUI());
                if (_movesCounter > 0) _movesCounter--;
                RefreshUndoRedoButtons();
            }
            catch { }
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            var playerMove = _history.Redo();
            if (playerMove != null)
            {
                ApplyRedoToUI(playerMove);
                var botMove = _history.Redo();
                if (botMove != null) ApplyRedoToUI(botMove);
            }
            _gameEngine.SetBoard(GetCurrentBoardFromUI());
            RefreshUndoRedoButtons();
        }

        private void ApplyUndoToUI(MoveSnapshot snapshot)
        {
            string btnName = $"btn{snapshot.Row}{snapshot.Col}";
            var btn = (Button)this.Controls.Find(btnName, true).FirstOrDefault();
            if (btn != null) { btn.Text = ""; btn.Enabled = true; btn.BackColor = Color.White; }
        }

        private void ApplyRedoToUI(MoveSnapshot snapshot)
        {
            string btnName = $"btn{snapshot.Row}{snapshot.Col}";
            var btn = (Button)this.Controls.Find(btnName, true).FirstOrDefault();
            if (btn != null) { btn.Text = snapshot.Symbol.ToString(); btn.Enabled = false; }
        }

        private void RefreshUndoRedoButtons()
        {
            if (btnUndo != null) btnUndo.Enabled = _history.CanUndo;
            if (btnRedo != null) btnRedo.Enabled = _history.CanRedo;
        }

        private char[,] GetCurrentBoardFromUI()
        {
            char[,] board = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    string btnName = $"btn{i}{j}";
                    var btn = (Button)this.Controls.Find(btnName, true).FirstOrDefault();
                    board[i, j] = string.IsNullOrEmpty(btn?.Text) ? '\0' : btn.Text[0];
                }
            }
            return board;
        }

        private void UpdateStatusLabel()
        {
            lblTournamentInfo.Text = $"{GetRoundTitle()} | Áàëè: {_tournamentManager.TotalScore}";
            int progress = (_tournamentManager.CurrentRound - 1) * 33;
            pbProgress.Value = Math.Min(progress, 100);
        }

        private string GetRoundTitle() => _tournamentManager.CurrentRound switch { 1 => "ÐÀÓÍÄ 1", 2 => "ÐÀÓÍÄ 2", 3 => "Ô²ÍÀË", _ => "ÃÐÀ" };

        private void PrepareNextRound()
        {
            _gameEngine.ResetBoard();
            _history.Clear();
            foreach (Control c in this.Controls) if (c is Button b && b.Name.StartsWith("btn")) { b.Text = ""; b.Enabled = true; b.BackColor = Color.White; }
        }

        private bool IsBoardFull() => _gameEngine.GetBoard().Cast<char>().All(c => c != '\0');

        private async void HighlightStatus(Color c) { lblTournamentInfo.ForeColor = c; await Task.Delay(1000); lblTournamentInfo.ForeColor = Color.Black; }

        private async Task FlashGameBoard(Color c)
        {
            var btns = this.Controls.OfType<Button>().Where(b => b.Name.StartsWith("btn")).ToList();
            foreach (var b in btns) b.BackColor = c;
            await Task.Delay(500);
            foreach (var b in btns) b.BackColor = Color.White;
        }
    }
}