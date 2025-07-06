using UnityEngine;

using VContainer;

namespace Orion.Main.DI
{
    public abstract class ServiceInstaller : MonoBehaviour
    {
        public abstract void Install(IContainerBuilder builder);
    }
}