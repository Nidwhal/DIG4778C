using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabController : MonoBehaviour
{
    [SerializeField] private string titleId = "YOUR_TITLE_ID";

    void Start()
    {
        if (string.IsNullOrWhiteSpace(titleId) || titleId == "YOUR_TITLE_ID")
        {
            Debug.LogWarning("Set your PlayFab Title ID in the Inspector before running.");
            return;
        }

        PlayFabSettings.staticSettings.TitleId = titleId;

        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log($"PlayFab login succeeded! Player ID: {result.PlayFabId}");
        SaveSampleData("0");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError($"PlayFab login failed: {error.GenerateErrorReport()}");
    }

    public void SaveSampleData(string cityIndexValue)
    {
        PlayFabClientAPI.UpdateUserData(
            new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { "LastCityIndex", cityIndexValue }
                }
            },
            result =>
            {
                Debug.Log("Saved LastCityIndex to PlayFab.");
                LoadSampleData();
            },
            error => Debug.LogError($"Save failed: {error.GenerateErrorReport()}")
        );
    }

    public void LoadSampleData()
    {
        PlayFabClientAPI.GetUserData(
            new GetUserDataRequest(),
            result =>
            {
                if (result.Data != null && result.Data.TryGetValue("LastCityIndex", out var record))
                {
                    Debug.Log($"Loaded LastCityIndex from PlayFab: {record.Value}");
                }
                else
                {
                    Debug.Log("No LastCityIndex found yet.");
                }
            },
            error => Debug.LogError($"Load failed: {error.GenerateErrorReport()}")
        );
    }
}
