using TestWork.ProjectSettings;
using UnityEngine;
using Zenject;

namespace TestWork.GameStates
{
    public class GameStateManager : MonoBehaviour, IInitializable, ITickable
    {
        private GameStateFactory _gameStateFactory;
        private GameStateEntity _gameStateEntity;

        [Inject]
        public void Construct(GameStateFactory gameStateFactory)
        {
            _gameStateFactory = gameStateFactory;
        }

        public async void ChangeState(Enumerators.GameStateTypes state)
        {
            if (_gameStateEntity != null)
            {
                await _gameStateEntity.Dispose();
            }
            
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