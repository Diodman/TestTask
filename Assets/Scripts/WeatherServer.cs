using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WeatherServer : MonoBehaviour
{
    private WeatherController weatherController;

    [Inject]
    public void Construct(WeatherController weatherController)
    {
        this.weatherController = weatherController;
    }

    private bool isOnFirstScreen = true;

    void Start()
    {
        weatherController.StartWeatherUpdates();
    }
}
