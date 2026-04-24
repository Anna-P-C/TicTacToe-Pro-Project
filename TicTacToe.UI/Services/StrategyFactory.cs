using TicTacToe.UI.Core;

namespace TicTacToe.UI.Services
{
   
    /// Патерн Factory Method.
    
    public static class StrategyFactory
    {
        public static IMoveStrategy CreateStrategy(string type)
        {
         
            switch (type.ToLower())
            {
                case "ai":
                case "expert":
                    return new MinimaxStrategy();

                
                default:
                    return new MinimaxStrategy();
            }
        }
    }
}