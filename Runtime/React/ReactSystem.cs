using Scellecs.Morpeh;

namespace Slimebones.ECSCore.React
{
    /// <summary>
    /// Adds listeners to buttons.
    /// </summary>
    public class ReactSystem: ISystem
    {
        private Filter f;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            f = World.Filter.With<React>().Build();

            foreach (var e in f)
            {
                ref var c = ref e.GetComponent<React>();

                if (c.listeners == null)
                {
                    continue;
                }

                foreach (var listener in c.listeners)
                {
                    if (listener == null)
                    {
                        continue;
                    }
                    listener.Subscribe(e, World);
                }
            }
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void Dispose()
        {
        }
    }
}
