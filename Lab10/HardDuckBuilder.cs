using UnityEngine;

public class HardDuckBuilder : EnemyBuilder
{
    public HardDuckBuilder(GameObject prefab, Vector3 spawnPos)
    {
        GameObject obj = Object.Instantiate(prefab, spawnPos, Quaternion.identity);
        enemyInstance = obj.GetComponent<Enemy>();
        if (enemyInstance == null) enemyInstance = obj.AddComponent<Enemy>();
    }

    public override void BuildName() { enemyInstance.targetName = "Hard Duck"; }
    public override void BuildSpeed() { enemyInstance.speed = 5f; }
    public override void BuildPoints() { enemyInstance.pointValue = 3; }
    public override void BuildDirection() { enemyInstance.direction = 1; }
}
