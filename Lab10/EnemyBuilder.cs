using UnityEngine;

public abstract class EnemyBuilder
{
    protected Enemy enemyInstance;

    public Enemy enemyDuck
    {
        get { return enemyInstance; }
    }

    public abstract void BuildName();
    public abstract void BuildSpeed();
    public abstract void BuildPoints();
    public abstract void BuildDirection();
}