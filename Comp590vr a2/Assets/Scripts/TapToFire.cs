using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fire : MonoBehaviour
{
    [SerializeField] private GlowingProjectile projectilePrefab;
    [SerializeField] private float maxProjectileSpeed = .1f;
    [SerializeField] private float minProjectileSpeed = .1f;

    [SerializeField] private InputActionReference triggerActionReference;

    float timeSinceLastShot = 1.0f;
    const float shotCooldown = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        triggerActionReference.action.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeSinceLastShot += Time.fixedDeltaTime;
        if (timeSinceLastShot > shotCooldown && triggerActionReference.action.IsPressed())
        {
            SpawnProjectile();
        }

    }

    private void SpawnProjectile()
    {
        GlowingProjectile projectile = Instantiate(projectilePrefab);
        projectile.transform.localPosition = transform.position;
        projectile.transform.rotation = Quaternion.AngleAxis(90, transform.right) * transform.rotation;
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.AddForce(transform.forward * UnityEngine.Random.Range(minProjectileSpeed, maxProjectileSpeed));
    }
}
