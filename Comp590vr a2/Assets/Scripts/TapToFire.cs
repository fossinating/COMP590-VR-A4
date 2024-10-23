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
    const float shotCooldown = 0.2f;

    const short maxBullets = 6;
    short bullets = 6;

    float timeSinceReload = 0f;
    const float reloadSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        triggerActionReference.action.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeSinceLastShot += Time.fixedDeltaTime;
        timeSinceReload += Time.fixedDeltaTime;
        while (bullets < maxBullets && timeSinceReload > reloadSpeed)
        {
            bullets++;
            timeSinceReload -= reloadSpeed; // do this to allow for multiple reloads at once if the game is lagging
        }
        if (bullets > 0 && timeSinceLastShot > shotCooldown && triggerActionReference.action.IsPressed())
        {
            SpawnProjectile();
            bullets--;
            timeSinceLastShot = 0;
            timeSinceReload = (timeSinceReload > reloadSpeed) ? 0 : timeSinceReload;
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
