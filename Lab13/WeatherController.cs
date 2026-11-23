using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class WeatherController : MonoBehaviour
{

    public string[] cities = { "Orlando,us", "Tokyo,jp", "London,gb", "Chicago,us", "Sydney,au" };
    public int selectedCityIndex = 0;

    public Light sunLight;
    public Material daySkybox;
    public Material nightSkybox;
    public Material rainySkybox;
    public Material snowySkybox;

    private const string apiKey = "OBFUSCATED_API_KEY";
    private WeatherManager weatherManager;
    private int previousCityIndex = -1;

    void Start()
    {
        weatherManager = new WeatherManager(apiKey);
        LoadSelectedCity();
    }

    void Update()
    {
        if (selectedCityIndex != previousCityIndex)
        {
            previousCityIndex = selectedCityIndex;
            LoadSelectedCity();
        }
    }

    public void LoadSelectedCity()
    {
        string city = cities[Mathf.Clamp(selectedCityIndex, 0, cities.Length - 1)];
        previousCityIndex = selectedCityIndex;
        StartCoroutine(weatherManager.GetWeatherXML(city, OnXMLDataLoaded));
    }

    public void OnXMLDataLoaded(string data)
    {
        Debug.Log(data);

        XDocument doc = XDocument.Parse(data);
        XElement root = doc.Root;

        string weatherDesc = root?.Element("weather")?.Attribute("value")?.Value?.ToLower() ?? "";
        string tempRaw = root?.Element("temperature")?.Attribute("value")?.Value ?? "293.15";
        string tzRaw = root?.Element("city")?.Element("timezone")?.Value ?? "0";

        float.TryParse(tempRaw, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float tempKelvin);
        int.TryParse(tzRaw, out int cityOffsetSeconds);

        //compare times
        TimeSpan localOffsetFromUtc = DateTimeOffset.Now.Offset;
        TimeSpan cityOffsetFromUtc = TimeSpan.FromSeconds(cityOffsetSeconds);
        TimeSpan timeDifference = cityOffsetFromUtc - localOffsetFromUtc;
        DateTime cityLocalTime = DateTime.Now + timeDifference;

        bool isDaytime = cityLocalTime.Hour >= 6 && cityLocalTime.Hour < 19;
        float tempCelsius = tempKelvin - 273.15f;

        Debug.Log($"City Time: {cityLocalTime:HH:mm} (diff: {timeDifference.TotalHours:+0.#;-0.#;0}h) | Day: {isDaytime} | Temp: {tempCelsius:0.0} C | Weather: {weatherDesc}");

        ApplyWeatherAndTime(weatherDesc, isDaytime);
    }

    private void ApplyWeatherAndTime(string weatherDesc, bool isDaytime)
    {
        bool isRainy = weatherDesc.Contains("rain") || weatherDesc.Contains("drizzle") || weatherDesc.Contains("thunderstorm");
        bool isSnowy = weatherDesc.Contains("snow") || weatherDesc.Contains("sleet");

        //change skybox
        if (isSnowy && snowySkybox != null)
        {
            RenderSettings.skybox = snowySkybox;
        }
        else if (isRainy && rainySkybox != null)
        {
            RenderSettings.skybox = rainySkybox;
        }
        else if (!isDaytime && nightSkybox != null)
        {
            RenderSettings.skybox = nightSkybox;
        }
        else if (daySkybox != null)
        {
            RenderSettings.skybox = daySkybox;
        }
        DynamicGI.UpdateEnvironment();

        //change light
        if (sunLight != null)
        {
            if (!isDaytime)
            {
                sunLight.intensity = 0.25f;
                sunLight.color = new Color(0.45f, 0.6f, 1.0f);
            }
            else if (isRainy || isSnowy)
            {
                sunLight.intensity = 0.6f;
                sunLight.color = new Color(0.75f, 0.82f, 0.9f);
            }
            else
            {
                sunLight.intensity = 1.2f;
                sunLight.color = new Color(1.0f, 0.95f, 0.84f);
            }
        }
    }

}