using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class BigMeteor : MeteorBase
{
    // Start is called before the first frame update
    protected override void Start()
    {
        //add the health of the meteor and run meteorbase start
        maxHealth = 5;
        base.Start();
    }
}
