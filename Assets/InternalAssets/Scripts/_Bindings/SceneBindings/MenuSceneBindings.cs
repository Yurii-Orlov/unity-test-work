using System;
using TestWork.UI.Interfaces;
using TestWork.UI.MenuPage;
using Zenject;

namespace TestWork.Bindings.SceneBinding
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