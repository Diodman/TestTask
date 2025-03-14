using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class DogController : MonoBehaviour
{
    private const string DogBreedsApiUrl = "https://dogapi.dog/api/v2/breeds";
    private const string DogBreedDetailsApiUrl = "https://dogapi.dog/api/v2/breeds/";

    private bool isRequestInProgress = false;
    private Coroutine dogBreedsCoroutine;
    private Coroutine dogBreedDetailsCoroutine;

    [Inject]
    private DogFactsView dogFactsView;


    public void StartDogBreedsUpdates()
    {
        StopAllCoroutines();
        dogFactsView.gameObject.SetActive(true);
        dogBreedsCoroutine = StartCoroutine(UpdateDogBreeds());
    }

    public void StopAllCoroutines()
    {
        if (dogBreedsCoroutine != null)
        {
            StopCoroutine(dogBreedsCoroutine);
            dogBreedsCoroutine = null;
        }
        if (dogBreedDetailsCoroutine != null)
        {
            StopCoroutine(dogBreedDetailsCoroutine);
            dogBreedDetailsCoroutine = null;
        }
    }

   

    private IEnumerator UpdateDogBreeds()
    {
        UnityWebRequest request = UnityWebRequest.Get(DogBreedsApiUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Ошибка загрузки данных: " + request.error);
            yield break;
        }

        string jsonResponse = request.downloadHandler.text;
        Debug.Log("Dog Breeds JSON Response: " + jsonResponse);

        // Парсим JSON
        DogBreedResponse breeds = JsonUtility.FromJson<DogBreedResponse>(jsonResponse);

        if (breeds == null || breeds.data == null)
        {
            Debug.LogError("Ошибка парсинга JSON! Объект пуст.");
            yield break;
        }

        List<DogBreedData> dogBreeds = new List<DogBreedData>();
        foreach (var breed in breeds.data)
        {
            dogBreeds.Add(new DogBreedData
            {
                id = breed.id,
                type = breed.type,
                attributes = new DogBreedAttributes
                {
                    name = breed.attributes.name,
                    description = breed.attributes.description,
                    life = breed.attributes.life
                }
            });
        }

        // Проверка на null
        if (dogFactsView == null)
        {
            Debug.LogError("dogFactsView is not assigned.");
            yield break;
        }

        if (dogFactsView.dogBreedItemPrefab == null)
        {
            Debug.LogError("dogBreedItemPrefab is not assigned.");
            yield break;
        }

        if (dogFactsView.dogBreedListContainer == null)
        {
            Debug.LogError("dogBreedListContainer is not assigned.");
            yield break;
        }

        dogFactsView.UpdateDogBreeds(dogBreeds);

        foreach (var breed in breeds.data)
        {
            Debug.Log($"Порода: {breed.attributes.name}, Описание: {breed.attributes.description}");
        }
    }



    public void RequestDogBreedDetails(string breedId)
    {
        if (dogBreedDetailsCoroutine != null)
        {
            StopCoroutine(dogBreedDetailsCoroutine);
        }
        dogBreedDetailsCoroutine = StartCoroutine(GetDogBreedDetails(breedId));
    }

    private IEnumerator GetDogBreedDetails(string breedId)
    {
        isRequestInProgress = true;
        dogFactsView.ShowLoader(true);
        UnityWebRequest request = UnityWebRequest.Get(DogBreedDetailsApiUrl + breedId);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Dog Breed Details JSON Response: {request.downloadHandler.text}");
            try
            {
                DogBreedData dogBreedData = JsonUtility.FromJson<DogBreedData>(request.downloadHandler.text);
                if (dogBreedData != null)
                {
                    Debug.Log($"Dog Breed: {dogBreedData.attributes.name}");
                    dogFactsView.ShowDogBreedDetails(dogBreedData);
                }
                else
                {
                    Debug.LogError("DogBreedData is null");
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

        dogFactsView.ShowLoader(false);
        isRequestInProgress = false;
    }
}
