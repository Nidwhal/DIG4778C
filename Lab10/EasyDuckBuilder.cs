using UnityEngine;

public class EasyDuckBuilder : EnemyBuilder
{
    public EasyDuckBuilder(GameObject prefab, Vector3 spawnPos)
    {
        GameObject obj = Object.Instantiate(prefab, spawnPos, Quaternion.identity);
        enemyInstance = obj.GetComponent<Enemy>();
        if (enemyInstance == null) enemyInstance = obj.AddComponent<Enemy>();
    }

    public override void BuildName() { enemyInstance.targetName = "Easy Duck"; }
    public override void BuildSpeed() { enemyInstance.speed = 3f; }
    public override void BuildPoints() { enemyInstance.pointValue = 1; }
    public override void BuildDirection() { enemyInstance.direction = 1; }
}