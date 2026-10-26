using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour, ISubject
{
    public static ScoreManager Instance;

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
}
