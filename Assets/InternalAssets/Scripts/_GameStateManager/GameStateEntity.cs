using System.Threading.Tasks;
using Zenject;

namespace TestWork.GameStates
{
    public abstract class GameStateEntity : IInitializable, ITickable
    {

        public virtual void Initialize()
        {
            
        }

        public virtual async void Start()
        {

        }

        public virtual void Tick()
        {
            
        }

        public abstract Task Dispose();

    }
}
