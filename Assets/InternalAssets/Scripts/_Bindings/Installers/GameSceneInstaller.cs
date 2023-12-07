using System;
using TestWork.Game.Player;
using UnityEngine;
using Zenject;

namespace TestWork.Bindings.ScriptableInstallers
{
    [CreateAssetMenu(fileName = "GameSceneInstaller", menuName = "Installers/GameSceneInstaller")]
    public class GameSceneInstaller : ScriptableObjectInstaller<GameSceneInstaller>
    {
        [SerializeField] private PlayerSettings _playerSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_playerSettings.playerMoveHandler).IfNotBound();
        }
    }
    
    [Serializable]
    public class PlayerSettings
    {
        public PlayerMoveHandler.Settings playerMoveHandler;
    }
}

