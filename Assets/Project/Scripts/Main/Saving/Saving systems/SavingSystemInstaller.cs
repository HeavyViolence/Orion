using Orion.Main.DI;

using UnityEngine;

using VContainer;

namespace Orion.Main.Saving
{
    public sealed class SavingSystemInstaller : ServiceInstaller
    {
        [SerializeField]
        private SavePath _savePath;

        public override void Install(IContainerBuilder builder)
        {
            switch (_savePath)
            {
                case SavePath.ToFile:
                    {
                        builder.RegisterInstance(new ToFileSavingSystem()).AsImplementedInterfaces().AsSelf();
                        break;
                    }
                case SavePath.ToPlayerPrefs:
                    {
                        builder.RegisterInstance(new ToPlayerPrefsSavingSystem()).AsImplementedInterfaces().AsSelf();
                        break;
                    }
                default:
                    {
                        goto case SavePath.ToFile;
                    }
            }
        }
    }
}