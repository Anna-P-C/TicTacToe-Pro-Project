using TicTacToe.UI.Core;

namespace TicTacToe.UI.Services
{
    public static class StrategyFactory
    {
        public static IMoveStrategy CreateStrategy(int level)
        {
            return level switch
            {
                1 => new EasyStrategy(),
                2 => new MediumStrategy(),
                3 => new MinimaxStrategy(),
                _ => new EasyStrategy()
            };
        }
    }
}