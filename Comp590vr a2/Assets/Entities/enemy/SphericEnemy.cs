using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericEnemy : MonoBehaviour
{
    private const float radius = 5;
    private const float speed = 1.5f;
    private bool shouldMove = true;
    public float timeAlive = 0f;

    void Start()
    {
        float randAngle = Random.Range(0, 2 * Mathf.PI);
        transform.position = new Vector3(
            radius * Mathf.Sin(randAngle),
            0,
            radius * Mathf.Cos(randAngle)) * radius;
    }

    void FixedUpdate()
    {
        timeAlive += Time.fixedDeltaTime;
        if (!shouldMove) return;


        transform.LookAt(Vector3.zero, Vector3.up);

        GetComponent<Rigidbody>().MovePosition(transform.position + speed * Time.fixedDeltaTime * transform.forward);
    }

    public void Stop()
    {
        shouldMove = false;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
