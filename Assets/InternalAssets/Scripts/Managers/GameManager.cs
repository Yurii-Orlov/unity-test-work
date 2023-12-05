using System;
using TestWork.Bindings.ScriptableInstallers;
using Zenject;

namespace TestWork.Managers
{
    public class GameManager : IInitializable, IDisposable
    {

        private event Action RestartGame;
        private event Action StartGame;
        
        public bool IsGameRunning { get; private set; }

        [Inject] private TestSettings _testSettings;


        public void PauseGame()
        {
        }

        public void RestartGameHandler()
        {
            RestartGame?.Invoke();
        }

        public void StartGameHandler()
        {
            StartGame?.Invoke();
        }

        public void StopGame()
        {
            IsGameRunning = false;
        }

        public void Dispose()
        {
        }

        public void Initialize()
        {

        }
    }
}