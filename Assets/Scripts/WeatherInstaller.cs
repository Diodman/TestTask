using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class WeatherInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<WeatherView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<WeatherController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<WeatherServer>().FromComponentInHierarchy().AsSingle();
    }
}
