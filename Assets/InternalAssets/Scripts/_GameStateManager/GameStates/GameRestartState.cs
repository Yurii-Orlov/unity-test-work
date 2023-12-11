using TestWork.Managers;
using TestWork.ProjectSettings;
using TestWork.UI.LoadingPopup;
using UniRx;
using Zenject;

namespace TestWork.GameStates.States
{

	public class GameRestartState : GameStateEntity
	{
		private readonly SceneLoaderManager _sceneLoaderManager;
		private readonly CompositeDisposable _disposable;
        private readonly UIManager _uiManager;

        public GameRestartState(UIManager uiManager, SceneLoaderManager sceneLoaderManager)
        {
	        _sceneLoaderManager = sceneLoaderManager;
	        _uiManager = uiManager;
            _disposable = new CompositeDisposable();
        }
        
        public override void Start()
        {
	        _uiManager.HideAllPopups();
	        _uiManager.DrawPopup<GameRestartPopup>(setMainPriority: true);
        }

        public override void Dispose()
        {
	        _uiManager.HidePopup<GameRestartPopup>();
            _disposable.Dispose();
        }

        public class Factory : PlaceholderFactory<GameRestartState>
		{
		}
	}

}