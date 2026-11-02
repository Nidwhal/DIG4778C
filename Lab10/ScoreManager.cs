using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ScoreManager : MonoBehaviour, ISubject
{
    public static ScoreManager Instance;
    private string scoreFile = "score.txt";

    private List<IObserver> observers = new List<IObserver>();
    private int currentScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void RemoveObserver(IObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public void NotifyObservers(int points)
    {
        foreach (var observer in observers)
        {
            observer.UpdateScore(points);
        }
    }

    //call when player gets a kill
    public void AddScore(int pointsToAdd)
    {
        currentScore += pointsToAdd;
        NotifyObservers(currentScore);
    }

    public void SaveScore()
    {
        string path = Path.Combine(Application.persistentDataPath, scoreFile);

        BinaryFormatter formatter = new BinaryFormatter();
        using(FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream, currentScore);
        }

        Debug.Log($"Saved score to: {path}");
    }

    public void LoadScore()
    {
        string path = Path.Combine(Application.persistentDataPath, scoreFile);

        //check if file exists, load if it does
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                currentScore = (int)formatter.Deserialize(stream);
                NotifyObservers(currentScore);
            }
            Debug.Log($"Score loaded successfully: {currentScore}");
        }
        else
        {
            Debug.LogWarning("No score file found.");
        }
    }
}
