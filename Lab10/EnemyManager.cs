public class EnemyManager
{
    public void Construct(EnemyBuilder builder)
    {
        //build an enemy
        builder.BuildName();
        builder.BuildSpeed();
        builder.BuildDirection();
        builder.BuildPoints();
    }
}
