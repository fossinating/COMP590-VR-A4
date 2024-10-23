using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null || other.gameObject == null || other.gameObject.transform == null)
        {
            return;
        }
        if (other.gameObject.GetComponent<SphericEnemy>() != null && other.gameObject.GetComponent<SphericEnemy>().timeAlive > 1)
        {
            GameObject.Find("Enemy Spawner").GetComponent<GameManager>().DamagePlayer();
            Destroy(other.gameObject);
        }
    }
}
