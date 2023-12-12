using System.Threading.Tasks;
using TestWork.Game;
using TestWork.Managers;
using TestWork.UI.LoadingPopup;
using UniRx;
using Zenject;

namespace TestWork.GameStates.States
{

	public class GameRestartState : GameStateEntity
	{
		private readonly CompositeDisposable _disposable;
        private readonly UIManager _uiManager;
        private readonly GamePlayTimer _gamePlayTimer;

        public GameRestartState(UIManager uiManager, GamePlayTimer gamePlayTimer)
        {
	        _uiManager = uiManager;
	        _gamePlayTimer = gamePlayTimer;
	        _disposable = new CompositeDisposable();
        }
        
        public override void Start()
        {
	        _uiManager.HideAllPopups();

	        var gameRestartPopupData = new GameRestartPopup.GameRestartPopupData()
	        {
		        gameLiveTimer = _gamePlayTimer.GameTimer
	        };
	        
	        _uiManager.DrawPopup<GameRestartPopup>(gameRestartPopupData, setMainPriority: true);
        }

        public override Task Dispose()
        {
	        _uiManager.HidePopup<GameRestartPopup>();
            _disposable.Dispose();
            
            return Task.CompletedTask;
        }

        public class Factory : PlaceholderFactory<GameRestartState>
		{
		}
	}

}