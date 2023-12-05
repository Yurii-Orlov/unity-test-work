using TestWork.Managers;
using Zenject;

namespace TestWork.Game.Player
{
    public class PlayerController 
    {
        private readonly GameManager _gameManager;

        public PlayerController(GameManager gameManager)
        {
            _gameManager = gameManager;
            Init();
        }

        private void Init()
        {
            
        }

        public class Factory : PlaceholderFactory<PlayerController>
        {

        }
    }
}