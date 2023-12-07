using System;
using TestWork.Bindings.SceneBinding;
using TestWork.Game.Enemies.EnemyStates;
using TestWork.Game.Player;
using UnityEngine;
using Zenject;

namespace TestWork.Bindings.ScriptableInstallers
{
    [CreateAssetMenu(fileName = "GameSceneInstaller", menuName = "Installers/GameSceneInstaller")]
    public class GameSceneInstaller : ScriptableObjectInstaller<GameSceneInstaller>
    {
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private GameSceneBindings.Settings _gameInstaller;

        public override void InstallBindings()
        {
            Container.BindInstance(_playerSettings.playerMoveHandler).IfNotBound();
            Container.BindInstance(_gameInstaller).IfNotBound();
        }
    }
    
    [Serializable]
    public class PlayerSettings
    {
        public PlayerMoveHandler.Settings playerMoveHandler;
    }
}

