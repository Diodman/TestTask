using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

    [System.Serializable]
    public class WetherClass
    {
        public int number;
        public string name;
        public string startTime;
        public string endTime;
        public bool isDaytime;
        public int temperature;
        public string temperatureUnit;
        public string temperatureTrend;
        public string windSpeed;
        public string windDirection;
        public string icon;
        public string shortForecast;
        public string detailedForecast;
        //"number": 1,
        //"name": "Overnight",
        //"startTime": "2025-03-12T04:00:00-05:00",
        //"endTime": "2025-03-12T06:00:00-05:00",
        //"isDaytime": false,
        //"temperature": 37,
        //"temperatureUnit": "F",
        //"temperatureTrend": "",
        //"windSpeed": "5 mph",
        //"windDirection": "NE",
        //"icon": "https://api.weather.gov/icons/land/night/bkn?size=medium",
        //"shortForecast": "Mostly Cloudy",
        //"detailedForecast": "Mostly cloudy, with a low around 37. Northeast wind around 5 mph."
    }

    [System.Serializable]
    public class WeatherList
    {
        public List<WetherClass> data;
    }

[System.Serializable]
public class WeatherData
{
    public WetherClass data;
}

