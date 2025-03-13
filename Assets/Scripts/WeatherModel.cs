using System;
using System.Collections.Generic;

[System.Serializable]
public class WeatherPeriod
{
    public int number;
    public string name;
    public string startTime;
    public string endTime;
    public bool isDaytime;
    public int temperature;
    public string temperatureUnit;
    public string windSpeed;
    public string windDirection;
    public string icon;
    public string shortForecast;
    public string detailedForecast;
}

[System.Serializable]
public class WeatherProperties
{
    public List<WeatherPeriod> periods;
}

[System.Serializable]
public class WeatherData
{
    public WeatherProperties properties;
}
