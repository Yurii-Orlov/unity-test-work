using System;
using UnityEngine;
using Zenject;

namespace OrCor.DataInstallers
{
    [CreateAssetMenu(fileName = "GameSceneInstaller", menuName = "Installers/GameSceneInstaller")]
    public class GameSceneInstaller : ScriptableObjectInstaller<GameSceneInstaller>
    {
        [SerializeField] private TestSettings _testSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_testSettings);
        }
    }

    [Serializable]
    public class TestSettings
    {
        public int testNum;
        public int EnemiesCreateCount;
    }
}

