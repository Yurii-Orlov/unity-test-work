using System;
using UnityEngine;
using Zenject;

namespace TestWork.Game.Player
{

	public class PlayerInstaller : MonoInstaller
	{
		[SerializeField] private Settings _settings;
		
		public override void InstallBindings()
		{
			Container.Bind<PlayerModel>().AsSingle().WithArguments(_settings.Rigidbody);
			
			Container.BindInterfacesTo<PlayerInputHandler>().AsSingle();
			Container.Bind<PlayerInputState>().AsSingle();

			Container.BindInterfacesTo<PlayerMoveHandler>().AsSingle();
		}

		[Serializable]
		public class Settings
		{
			public Rigidbody Rigidbody;
		}
	}

}

