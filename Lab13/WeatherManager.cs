using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherManager
{

    private const string xmlApiBase = "http://api.openweathermap.org/data/2.5/weather?q={0}&mode=xml&appid={1}";
    private readonly string apiKey;

    public WeatherManager(string apiKey)
    {
        this.apiKey = apiKey;
    }

    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError($"network problem: {request.error}");
            }
            else if (request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"response error: {request.responseCode}");
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetWeatherXML(string cityQuery, Action<string> callback)
    {
        string url = string.Format(xmlApiBase, cityQuery, apiKey);
        return CallAPI(url, callback);
    }

}
