using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class WeatherView : MonoBehaviour
{
    public Text text;
    public Image image;

    public void DisplayText(WetherClass wether)
    {
        text.text = $"Сейчас - {wether.temperature}{wether.temperatureUnit}";
        StartCoroutine(LoadIcon(wether.icon));
    }

    private IEnumerator LoadIcon(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;

            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            Debug.Log("error");
        }    
    }
}
