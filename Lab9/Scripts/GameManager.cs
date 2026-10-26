using UnityEngine;

public class GameManager : MonoBehaviour
{
    //create cooldowns and timers
    private float easySpawnCooldown = 1.2f;
    private float lastEasySpawn = 0f;
    private float mediumSpawnCooldown = 1f;
    private float lastMediumSpawn = 0f;
    private float hardSpawnCooldown = .5f;
    private float lastHardSpawn = 0f;

    [SerializeField] private EnemySpawner spawner;
    private void Update()
    {
        //handle cooldown timers
        lastEasySpawn += Time.deltaTime;
        lastMediumSpawn += Time.deltaTime;
        lastHardSpawn += Time.deltaTime;

        //spawn easy ducks
        if(lastEasySpawn > easySpawnCooldown)
        {
            spawner.SpawnDuck(1);
            lastEasySpawn = 0;
        }
        //spawn medium ducks
        if (lastMediumSpawn > mediumSpawnCooldown)
        {
            spawner.SpawnDuck(2);
            lastMediumSpawn = 0;
        }
        //spawn hard ducks
        if (lastHardSpawn > hardSpawnCooldown)
        {
            spawner.SpawnDuck(3);
            lastHardSpawn = 0;
        }
    }
}
