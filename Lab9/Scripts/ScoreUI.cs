using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour, IObserver
{
    [SerializeField] private TextMeshProUGUI scoreTextField;

    private void Start()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.RegisterObserver(this);
        }
    }

    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.RemoveObserver(this);
        }
    }

    public void UpdateScore(int totalScore)
    {
        if (scoreTextField != null)
        {
            scoreTextField.text = "Score: " + totalScore;
        }
    }
}
