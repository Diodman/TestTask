using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DogBreed
{
    public string id;
    public string type;
    public Attributes attributes;
}

[Serializable]
public class Attributes
{
    public string name;
    public string description;
    public LifeSpan life;
}

[Serializable]
public class DogBreedResponse
{
    public List<DogBreed> data;
}
