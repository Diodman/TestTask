using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeatherView : MonoBehaviour
{
    public Text weatherText;
    public Image weatherIcon;

    public void UpdateWeather(WeatherPeriod weather)
    {
        weatherText.text = $"Сегодня - {weather.temperature}{weather.temperatureUnit}";
        StartCoroutine(LoadIcon(weather.icon));
    }

    private IEnumerator LoadIcon(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            weatherIcon.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            Color color = weatherIcon.color;
            color.a = 1f;
            weatherIcon.color = color;
        }
        else
        {
            Debug.LogError($"Icon load error: {request.error}");
        }
    }
}
