using System;
using System.Collections.Generic;
using System.Linq;
using OrCor.States;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;
using ObservableExtensions = UniRx.ObservableExtensions;

namespace OrCor.Manager
{
    public class UIManager : IInitializable, IDisposable
    {
        private List<IUIElement> Pages { get; }
        private List<IUIPopup> Popups { get; }
        private IUIElement CurrentPage { get; set; }
        private CanvasScaler CanvasScaler { get; set; }
        private GameObject Canvas { get; set; }
        public GameObject CanvasParent { get; set; }
        
        private readonly GameStateManager _gameStateManager;

        public UIManager(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
            Pages = new List<IUIElement>();
            Popups = new List<IUIPopup>();

            var uiView = Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/UICanvasView"));
            Canvas = uiView.transform.Find("Canvas").gameObject;
            CanvasParent = Canvas.transform.Find("SafeArea").gameObject;
        }

        void IDisposable.Dispose()
        {
        }

        public void Initialize()
        {
            Canvas.transform.parent.SetParent(_gameStateManager.transform.parent);
            CanvasScaler = Canvas.GetComponent<CanvasScaler>();

            Observable.OnceApplicationQuit().Subscribe(x =>
            {
                Dispose();
            });
        }

        public void AddPage(IUIElement page)
        {
            Pages.Add(page);
        }

        public void AddPopup(IUIPopup popup)
        {
            Popups.Add(popup);
        }

        public void RemovePage(IUIElement page)
        {
            Pages.Remove(page);
        }

        public void RemovePopup(IUIPopup popup)
        {
            Popups.Remove(popup);
        }

        public void HideAllPages()
        {
            foreach (var item in Pages) item.Hide();
        }

        public void HideAllPopups()
        {
            foreach (var item in Popups) item.Hide();
        }

        public void SetPage<T>(bool hideAll = false) where T : IUIElement
        {
            if (hideAll)
                HideAllPages();
            else
                CurrentPage?.Hide();

            foreach (var item in Pages.OfType<T>())
            {
                CurrentPage = item;
                break;
            }

            CurrentPage?.Show();
        }

        public void DrawPopup<T>(object message = null, bool setMainPriority = false) where T : IUIPopup
        {
            IUIPopup popup = null;
            foreach (var item in Popups.OfType<T>())
            {
                popup = item;
                break;
            }

            if (popup == null) return;

            if (setMainPriority)
                popup.SetMainPriority();

            if (message == null)
                popup.Show();
            else
                popup.Show(message);
        }

        public void HidePopup<T>() where T : IUIPopup
        {
            foreach (var popup in Popups.OfType<T>())
            {
                popup.Hide();
                break;
            }
        }

        public IUIPopup GetPopup<T>() where T : IUIPopup
        {
            IUIPopup popup = null;
            foreach (var item in Popups.OfType<T>())
            {
                popup = item;
                break;
            }

            return popup;
        }

        public IUIElement GetPage<T>() where T : IUIElement
        {
            IUIElement page = null;
            foreach (var item in Pages.OfType<T>())
            {
                page = item;
                break;
            }

            return page;
        }


        private void Dispose()
        {
            if (!ReferenceEquals(Pages, null))
            {
                foreach (var page in Pages.ToList())
                {
                    page.Dispose();
                }
            }

            if (!ReferenceEquals(Popups, null))
            {
                foreach (var popup in Popups.ToList())
                {
                    popup.Dispose();
                }
            }
        }
    }
}