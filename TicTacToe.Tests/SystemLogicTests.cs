using Xunit;
using TicTacToe.UI.Core;
using TicTacToe.UI.Services;

namespace TicTacToe.Tests
{
    public class SystemLogicTests
    {
        [Fact]
        public void AchievementManager_ShouldInitializeWithCorrectProgress()
        {
            var manager = AchievementManager.Instance;

            
            var unlocked = manager.GetUnlockedAchievements();

           
            Assert.NotNull(unlocked);
        }

        [Fact]
        public void AchievementManager_TotalProgress_ShouldBeBetween0And100()
        {
            var manager = AchievementManager.Instance;
            int progress = manager.GetTotalProgress();

            Assert.True(progress >= 0 && progress <= 100);
        }

        [Fact]
        public void CommandHistory_InitialState_CannotUndoOrRedo()
        {
            var history = new CommandHistory();

            Assert.False(history.CanUndo);
            Assert.False(history.CanRedo);
        }

        [Fact]
        public void ScoreService_IsSingleton_ReturnsSameInstance()
        {
            var instance1 = ScoreService.Instance;
            var instance2 = ScoreService.Instance;
            Assert.Same(instance1, instance2);
        }
    }
}