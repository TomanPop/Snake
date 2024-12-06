using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IJsonService>().To<JsonService>().AsSingle();
        Container.Bind<IAppSettingsService>().To<AppSettingsService>().AsSingle();
    }
}