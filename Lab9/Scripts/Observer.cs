using UnityEngine;

public class Observer
{
    private int totalScore = 0;
    public void UpdateScore(int score)
    {
        totalScore += score;

    }
}
