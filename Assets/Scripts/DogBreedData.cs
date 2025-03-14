[System.Serializable]
public class DogBreedData
{
    public string id;
    public string type;
    public DogBreedAttributes attributes;
}

[System.Serializable]
public class DogBreedAttributes
{
    public string name;
    public string description;
    public LifeSpan life;
    public Weight male_weight;
    public Weight female_weight;
    public bool hypoallergenic;
}

[System.Serializable]
public class LifeSpan
{
    public int max;
    public int min;
}

[System.Serializable]
public class Weight
{
    public int max;
    public int min;
}
