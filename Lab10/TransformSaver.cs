using UnityEngine;

public class TransformSaver : MonoBehaviour, ISaveable
{
    [SerializeField] private string _saveID;

    public string SaveID
    {
        get
        {
            if (string.IsNullOrEmpty(_saveID))
            {
                _saveID = System.Guid.NewGuid().ToString();
            }
            return _saveID;
        }
    }


    public string SavedDataJson
    {
        get
        {
            Vector3 position = transform.position;
            return JsonUtility.ToJson(position);
        }
    }

}