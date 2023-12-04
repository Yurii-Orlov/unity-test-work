﻿using System;
using UnityEngine;

namespace OrCor
{
    public interface IUIPopup
    {
        event Action OnPopupWasInited;

        GameObject SelfPage { get; }

        void Show();
        void Show(object data);
        void Hide();
        void Dispose();
        void SetMainPriority();
    }
}