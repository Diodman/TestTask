using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class WeatherController : MonoBehaviour
{
    private const string WeatherApiUrl = "https://api.weather.gov/gridpoints/MPX/107,69/forecast";
    private bool isRequestInProgress = false;
    private Coroutine weatherCoroutine;

    [Inject]
    private WeatherView weatherView;

    public void StartWeatherUpdates()
    {
        weatherCoroutine = StartCoroutine(UpdateWeather());
    }

    public void StopWeatherUpdates()
    {
        if (weatherCoroutine != null)
        {
            StopCoroutine(weatherCoroutine);
            weatherCoroutine = null;
        }
    }

    private IEnumerator UpdateWeather()
    {
        while (true)
        {
            if (isRequestInProgress)
            {
                yield return new WaitForSeconds(5);
                continue;
            }

            isRequestInProgress = true;
            UnityWebRequest request = UnityWebRequest.Get(WeatherApiUrl);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    WeatherData weatherData = JsonUtility.FromJson<WeatherData>(request.downloadHandler.text);
                    if (weatherData != null && weatherData.properties != null && weatherData.properties.periods != null && weatherData.properties.periods.Count > 0)
                    {
                        WeatherPeriod currentWeather = weatherData.properties.periods[0];
                        weatherView.UpdateWeather(currentWeather);
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"JSON parse error: {ex.Message}");
                }
            }
            else
            {
                Debug.LogError($"Request error: {request.error}");
            }

            isRequestInProgress = false;
            yield return new WaitForSeconds(5);
        }
    }
}
