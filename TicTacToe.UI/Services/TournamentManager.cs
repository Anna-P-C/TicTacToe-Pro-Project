namespace TicTacToe.UI.Services
{
    public class TournamentManager
    {
        public int CurrentRound { get; set; } = 1;
        public int TotalScore { get; set; } = 0;
        public bool IsTournamentActive { get; set; } = true;

        
        public int PlayerWins { get; private set; } = 0;
        public int BotWins { get; private set; } = 0;
        public int Draws { get; private set; } = 0;

        public void RegisterWin(char winnerSymbol)
        {
            if (winnerSymbol == 'X')
            {
                PlayerWins++;
                TotalScore += CurrentRound switch { 1 => 100, 2 => 300, 3 => 1000, _ => 0 };
            }
            else if (winnerSymbol == 'O')
            {
                BotWins++; 
            }
            else
            {
                Draws++; 
            }

            if (CurrentRound < 3)
            {
                CurrentRound++;
                IsTournamentActive = true;
            }
            else
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
            CurrentRound = 1; TotalScore = 0; IsTournamentActive = true;
            PlayerWins = 0; BotWins = 0; Draws = 0;
        }
    }
}