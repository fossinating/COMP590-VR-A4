using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GlowingProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destructible destructable = collision.gameObject.GetComponent<Destructible>();
        if (destructable != null)
        {
            destructable.Hit();
        }
        Destroy(gameObject);
    }
}
