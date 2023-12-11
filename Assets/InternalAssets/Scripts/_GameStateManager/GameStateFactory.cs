using ModestTree;
using TestWork.GameStates.States;
using TestWork.ProjectSettings;

namespace TestWork.GameStates
{
    public class GameStateFactory
    {
        private readonly GameState.Factory _gamePlayFactory;
        private readonly MenuState.Factory _menuStateFactory;
        private readonly GameRestartState.Factory _gameRestartStateFactory;

        public GameStateFactory(GameState.Factory gamePlayFactory,
            MenuState.Factory menuStateFactory,
            GameRestartState.Factory gameRestartStateFactory)
        {
            _gamePlayFactory = gamePlayFactory;
            _menuStateFactory = menuStateFactory;
            _gameRestartStateFactory = gameRestartStateFactory;
        }

        internal GameStateEntity CreateState(Enumerators.GameStateTypes gameState)
        {
            switch (gameState)
            {
                case Enumerators.GameStateTypes.GAMEPLAY_START:
                    return _gamePlayFactory.Create();

                case Enumerators.GameStateTypes.MENU:
                    return _menuStateFactory.Create();
                
                case Enumerators.GameStateTypes.GAMEPLAY_LOSE:
                    return _gameRestartStateFactory.Create();
            }

            throw Assert.CreateException("Code should not be reached");
        }
    }
}