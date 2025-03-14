using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WeatherServer : MonoBehaviour
{
    private WeatherController weatherController;
    private DogController dogController;

    [Inject]
    private DogFactsView dogFactsView;

    [Inject]
    public void Construct(WeatherController weatherController)
    {
        this.weatherController = weatherController;
    }

    void Start()
    {
        weatherController.StartWeatherUpdates();
    }

    public void OnWeatherTabSelected()
    {
        weatherController.StartWeatherUpdates();
        dogFactsView.gameObject.SetActive(false);
        weatherController.gameObject.SetActive(true);
    }

    public void OnDogFactsTabSelected()
    {
        dogController.StartDogBreedsUpdates();
        weatherController.gameObject.SetActive(false);
        dogFactsView.gameObject.SetActive(true);
    }
}
