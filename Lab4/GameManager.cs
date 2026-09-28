using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //create gamemanager
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject bigMeteorPrefab;
    [SerializeField] private GameObject MeteorPrefab;
    [SerializeField] private int scoreToSpawnBigMeteor = 5;

    private int currentScore = 0;

    private void Awake()
    {
        //make sure there is only one instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        //begin spawning meteors
        InvokeRepeating("SpawnMeteor",4f,4f);
    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        //listen for meteor destruction
        MeteorBase.OnMeteorDestroyed += HandleMeteorDestroyed;
    }

    private void OnDisable()
    {
        //stop listening for meteors being destroyed
        MeteorBase.OnMeteorDestroyed -= HandleMeteorDestroyed;
    }

    private void SpawnMeteor()
    {
        Instantiate(MeteorPrefab);
    }

    private void HandleMeteorDestroyed(MeteorBase meteor)
    {
        currentScore++;
        Debug.Log($"Meteor Destroyed! Current Score: {currentScore}");

        if (currentScore > 0 && currentScore % scoreToSpawnBigMeteor == 0 && bigMeteorPrefab != null)
        {
            SpawnBigMeteor();
        }
    }

    private void SpawnBigMeteor()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 6f, 0f);
        Instantiate(bigMeteorPrefab, spawnPos, Quaternion.identity);
    }

    public void SetGameOver()
    {
        Debug.Log("Game Over!");
        
    }
}
