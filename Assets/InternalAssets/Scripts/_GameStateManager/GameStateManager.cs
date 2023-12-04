using UnityEngine;
using Zenject;

namespace OrCor.States
{
    public class GameStateManager : MonoBehaviour, IInitializable, ITickable
    {
        [SerializeField]
        private Enumerators.GameStateTypes _currentGameState = Enumerators.GameStateTypes.START_GAMEPLAY;

        [SerializeField] private Enumerators.GameStateTypes _previousGameState;

        private GameStateFactory _gameStateFactory;
        private GameStateEntity _gameStateEntity;

        [Inject]
        public void Construct(GameStateFactory gameStateFactory)
        {
            _gameStateFactory = gameStateFactory;
        }

        internal void ChangeState(Enumerators.GameStateTypes state)
        {
            if (_gameStateEntity != null)
            {
                _gameStateEntity.Dispose();
                _gameStateEntity = null;
            }

            _previousGameState = _currentGameState;
            _currentGameState = state;

            _gameStateEntity = _gameStateFactory.CreateState(state);
            _gameStateEntity.Initialize();
            _gameStateEntity.Start();
        }


        public void Initialize()
        {
            ChangeState(Enumerators.GameStateTypes.MENU);
        }

        public void Tick()
        {
            _gameStateEntity?.Tick();
        }
    }
}