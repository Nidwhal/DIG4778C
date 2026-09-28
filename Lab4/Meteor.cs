using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MeteorBase
{

    // Start is called before the first frame update
    protected override void Start()
    {
        //add the health of the meteor and run meteorbase start
        maxHealth = 1;
        base.Start();
    }
}
