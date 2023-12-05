﻿using System;
using Zenject;

namespace TestWork.GameStates
{
    public abstract class GameStateEntity : IInitializable, ITickable, IDisposable
    {

        public virtual void Initialize()
        {
            
        }

        public virtual void Start()
        {

        }

        public virtual void Tick()
        {
            
        }

        public virtual void Dispose()
        {
            
        }
    }
}
