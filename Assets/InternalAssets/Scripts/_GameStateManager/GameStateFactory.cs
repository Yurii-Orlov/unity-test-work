using ModestTree;
using TestWork.GameStates.States;
using TestWork.ProjectSettings;

namespace TestWork.GameStates
{
    public class GameStateFactory
    {
        private readonly GameState.Factory _gamePlayFactory;
        private readonly MenuState.Factory _menuStateFactory;

        public GameStateFactory(GameState.Factory gamePlayFactory,
            MenuState.Factory menuStateFactory)
        {
            _gamePlayFactory = gamePlayFactory;
            _menuStateFactory = menuStateFactory;
        }

        internal GameStateEntity CreateState(Enumerators.GameStateTypes gameState)
        {
            switch (gameState)
            {
                case Enumerators.GameStateTypes.START_GAMEPLAY:
                    return _gamePlayFactory.Create();

                case Enumerators.GameStateTypes.MENU:
                    return _menuStateFactory.Create();
            }

            throw Assert.CreateException("Code should not be reached");
        }
    }
}