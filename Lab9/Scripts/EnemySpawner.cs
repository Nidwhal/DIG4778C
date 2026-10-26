using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject easyDuck;
    [SerializeField] private GameObject mediumDuck;
    [SerializeField] private GameObject hardDuck;
    public void SpawnDuck(int difficulty)
    {
        EnemyManager spawner = new EnemyManager();

        switch (difficulty)
        {
            case 1:
                //build an easy duck
                EnemyBuilder easyDuckBuilder = new EasyDuckBuilder(easyDuck, new Vector3(-10f, 2f, 0f));
                spawner.Construct(easyDuckBuilder);
                break;
            case 2:
                //build a medium duck
                EnemyBuilder mediumDuckBuilder = new MediumDuckBuilder(mediumDuck, new Vector3(10f, 3f, 0f));
                spawner.Construct(mediumDuckBuilder);
                break;
            case 3:
                //build a hard duck
                EnemyBuilder hardDuckBuilder = new HardDuckBuilder(hardDuck, new Vector3(-10f, 4f, 0f));
                spawner.Construct(hardDuckBuilder);
                break;
        }
    }
}
