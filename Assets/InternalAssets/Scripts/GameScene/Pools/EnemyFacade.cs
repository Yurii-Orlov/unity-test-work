using Zenject;

namespace OrCor.Pool
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