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
        if (other == null)
        {
            return;
        }
        if (other.gameObject.GetComponentInParent<SphericEnemy>())
        {
            if (other.gameObject == null || other.gameObject.transform == null || other.gameObject.transform.parent == null || other.gameObject.transform.parent.gameObject == null)
            {
                return;
            }
            transform.parent.parent.GetComponentInChildren<GameManager>().DamagePlayer();
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
