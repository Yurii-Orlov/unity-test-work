using System;
using TestWork.Game;
using TestWork.Game.Player;
using TestWork.GameStates;
using TestWork.ProjectSettings;
using Zenject;

namespace TestWork.Managers
{
    public class GameManager : IInitializable, IDisposable
    {

        private PlayerFacade _playerFacade;
        private GameStateManager _gameStateManager;
        private GamePlayTimer _gamePlayTimer;

        public bool IsGameRunning { get; private set; }

        [Inject]
        private void Construct(PlayerFacade playerFacade, GameStateManager gameStateManager, GamePlayTimer gamePlayTimer)
        {
            _gameStateManager = gameStateManager;
            _playerFacade = playerFacade;
            _gamePlayTimer = gamePlayTimer;
            _playerFacade.OnTakeDamage += OnPlayerTakeDamage;
        }

        private void OnPlayerTakeDamage(float health, bool isDead)
        {
            if (isDead)
            {
                _gamePlayTimer.StopTimer();
                StopGame();
                _gameStateManager.ChangeState(Enumerators.GameStateTypes.GAMEPLAY_LOSE);
            }
        }

        private void StopGame()
        {
            IsGameRunning = false;
        }

        public void Dispose()
        {
            if (_playerFacade != null)
            {
                _playerFacade.OnTakeDamage -= OnPlayerTakeDamage;
            }
        }

        public void Initialize()
        {
            _gamePlayTimer.StartTimer();
        }

    }
}