using Zenject;

namespace TestWork.Game.Pool
{

    public class EnemyFacade : ObjectFacade
    {

        public override void OnSpawned(IMemoryPool pool)
        {
            base.OnSpawned(pool);
        }

        public class Factory : PlaceholderFactory<EnemyFacade>
        {
        }
    }
}