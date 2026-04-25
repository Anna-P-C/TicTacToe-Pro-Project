namespace TicTacToe.UI.Services
{
    public class TournamentManager
    {
    
        public int CurrentRound { get; set; } = 1;
        public int TotalScore { get; set; } = 0;
        public bool IsTournamentActive { get; set; } = true;

        public void RegisterWin(char winnerSymbol)
        {
            if (winnerSymbol == 'X')
            {
                TotalScore += CurrentRound switch
                {
                    1 => 100,
                    2 => 300,
                    3 => 1000,
                    _ => 0
                };

                if (CurrentRound < 3)
                {
                    CurrentRound++;
                }
                else
                {
                    IsTournamentActive = false;
                }
            }
            else if (winnerSymbol == 'O')
            {
                IsTournamentActive = false;
            }
        }

        public string GetCurrentDifficultyName() => CurrentRound switch
        {
            1 => "Новачок (Easy)",
            2 => "Профі (Medium)",
            3 => "Майстер (Minimax)",
            _ => "Unknown"
        };

  
        public void ResetTournament()
        {
            CurrentRound = 1;
            TotalScore = 0;
            IsTournamentActive = true;
        }
    }
}