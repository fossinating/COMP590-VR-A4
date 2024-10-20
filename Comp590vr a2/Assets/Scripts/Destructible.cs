using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : HitResponsive
{

    override public void Hit()
    {
        Destroy(gameObject);
        GameObject.Find("Enemy Spawner").GetComponent<GameManager>().AddPoints(pointValue);
        pointValue = 0;
    }
}
