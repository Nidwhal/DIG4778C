using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LocationSaveManager : MonoBehaviour
{
    private string jsonFileName = "locations.json";

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject player;

    [System.Serializable]
    public class ObjectSaveRecord
    {
        public string saveID;
        public string transformJsonData;
        public string unitInfo;
    }

    [System.Serializable]
    public class SaveFileWrapper
    {
        public ObjectSaveRecord[] records;
    }

    public void SaveLocations()
    {
        var saveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<TransformSaver>();

        var recordList = saveableObjects.Select(obj => new ObjectSaveRecord
        {
            saveID = obj.SaveID,
            transformJsonData = obj.SavedDataJson,
            unitInfo = obj.tag
        }).ToArray();

        SaveFileWrapper wrapper = new SaveFileWrapper { records = recordList };
        string jsonOutput = JsonUtility.ToJson(wrapper, true);

        string path = Path.Combine(Application.persistentDataPath, jsonFileName);
        File.WriteAllText(path, jsonOutput);
        Debug.Log($"Locations saved to JSON at: {path}");
    }

    public void LoadLocations()
    {
        string path = Path.Combine(Application.persistentDataPath, jsonFileName);
        if (!File.Exists(path))
        {
            Debug.LogWarning("No location JSON file found.");
            return;
        }

        //destroy old ducks before load
        var existingSaveables = Object.FindObjectsOfType<TransformSaver>();
        foreach (var obj in existingSaveables)
        {
            if(obj.tag != "Player")
            {
                Destroy(obj.gameObject);
            }
        }

        string jsonInput = File.ReadAllText(path);
        SaveFileWrapper wrapper = JsonUtility.FromJson<SaveFileWrapper>(jsonInput);

        foreach (var record in wrapper.records)
        {
            Vector3 position = JsonUtility.FromJson<Vector3>(record.transformJsonData);
            if (record.unitInfo == "Player")
            {
                player.transform.position = position;
            }
            else
            {
                enemySpawner.LoadDuck(record.unitInfo, position);
            }
        }
        Debug.Log("Locations loaded successfully from JSON.");
    }
}