using TestWork.Game.Enemies.EnemyStates;
using Zenject;

namespace TestWork.Game.Enemies
{

	public class EnemyInstaller : Installer<EnemyInstaller>
	{

		public override void InstallBindings()
		{
			Container.Bind<EnemyModel>().AsSingle();
			
			Container.BindInterfacesAndSelfTo<EnemyStateManager>().AsSingle();

			Container.Bind<EnemyStateIdle>().AsSingle();
			Container.Bind<EnemyStateFollow>().AsSingle();
		}

	}

}