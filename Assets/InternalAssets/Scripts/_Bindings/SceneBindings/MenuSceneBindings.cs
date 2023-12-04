using System;
using OrCor.UI;
using Zenject;

namespace OrCor.SceneBinding
{
    public class MenuSceneBindings : MonoInstaller
    {
        public override void InstallBindings()
        {
            InitUiViews();
        }

        private void InitUiViews()
        {
            Container.Bind(typeof(IInitializable), typeof(IDisposable), typeof(IUIElement)).To<MenuPage>().AsSingle();
        }
    }
}