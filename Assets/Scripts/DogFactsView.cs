using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogFactsView : MonoBehaviour
{
    public GameObject loader;
    public GameObject dogBreedItemPrefab;
    public Transform dogBreedListContainer;
    public GameObject dogBreedDetailsPopup;
    public Text dogBreedDetailsText;

    private List<GameObject> dogBreedItems = new List<GameObject>();

    public void ShowLoader(bool show)
    {
        loader.SetActive(show);
    }

    public void UpdateDogBreeds(List<DogBreedData> dogBreeds)
    {
        ClearDogBreedItems();

        foreach (var breed in dogBreeds)
        {
            GameObject item = Instantiate(dogBreedItemPrefab, dogBreedListContainer);
            if (item == null)
            {
                Debug.LogError("Failed to instantiate dogBreedItemPrefab.");
                continue;
            }

            Text breedText = item.GetComponentInChildren<Text>();
            if (breedText == null)
            {
                Debug.LogError("Text component not found in dogBreedItemPrefab.");
                continue;
            }

            breedText.text = breed.attributes.name;

            Button breedButton = item.GetComponent<Button>();
            if (breedButton == null)
            {
                Debug.LogError("Button component not found in dogBreedItemPrefab.");
                continue;
            }

            breedButton.onClick.AddListener(() => OnDogBreedItemClick(breed.id));
            dogBreedItems.Add(item);
        }
    }


    private void ClearDogBreedItems()
    {
        foreach (var item in dogBreedItems)
        {
            Destroy(item);
        }
        dogBreedItems.Clear();
    }

    private void OnDogBreedItemClick(string breedId)
    {
        FindObjectOfType<DogController>().RequestDogBreedDetails(breedId);
    }

    public void ShowDogBreedDetails(DogBreedData dogBreed)
    {
        dogBreedDetailsText.text = $"{dogBreed.attributes.name}\n\n{dogBreed.attributes.description}";
        dogBreedDetailsPopup.SetActive(true);
    }
}
