using UnityEngine;

public class MediumDuckBuilder : EnemyBuilder
{
    public MediumDuckBuilder(GameObject prefab, Vector3 spawnPos)
    {
        GameObject obj = Object.Instantiate(prefab, spawnPos, Quaternion.identity);
        enemyInstance = obj.GetComponent<Enemy>();
        if (enemyInstance == null) enemyInstance = obj.AddComponent<Enemy>();
    }

    public override void BuildName() { enemyInstance.targetName = "Medium Duck"; }
    public override void BuildSpeed() { enemyInstance.speed = 4f; }
    public override void BuildPoints() { enemyInstance.pointValue = 2; }
    public override void BuildDirection() { enemyInstance.direction = -1; }
}
