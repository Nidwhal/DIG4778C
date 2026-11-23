using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BillboardManager : MonoBehaviour
{

    private const string webImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/15/Cat_August_2010-4.jpg/2560px-Cat_August_2010-4.jpg";

    public Renderer[] billboards;
    public string[] imageUrls;

    private Dictionary<string, Texture2D> imageCache = new Dictionary<string, Texture2D>();

    void Start()
    {
        for (int i = 0; i < billboards.Length; i++)
        {
            if (billboards[i] == null) continue;

            string url = (imageUrls != null && i < imageUrls.Length && !string.IsNullOrEmpty(imageUrls[i]))
               ? imageUrls[i]
               : webImage;

            int index = i;
            StartCoroutine(DownloadImage(url, tex => {
                if (billboards[index] != null && tex != null)
                {
                    billboards[index].material.mainTexture = tex;
                }
            }));
        }
    }

    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {
        return DownloadImage(webImage, callback);
    }

    public IEnumerator DownloadImage(string url, Action<Texture2D> callback)
    {
        if (imageCache.ContainsKey(url) && imageCache[url] != null)
        {
            Debug.Log("Using cached image: " + url);
            callback(imageCache[url]);
            yield break;
        }

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
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
            Texture2D tex = DownloadHandlerTexture.GetContent(request);
            imageCache[url] = tex;
            callback(tex);
        }

        request.Dispose();
    }

}
