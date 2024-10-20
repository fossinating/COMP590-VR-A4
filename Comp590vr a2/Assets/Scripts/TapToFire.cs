using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem;

public class Fire : MonoBehaviour
{
    [SerializeField] private GlowingProjectile projectilePrefab;
    [SerializeField] private float maxProjectileSpeed = 1000f;
    [SerializeField] private float minProjectileSpeed = 750f;

    // Start is called before the first frame update
    void Start()
    {
        EnhancedTouchSupport.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!transform.parent.parent.GetComponentInChildren<GameManager>().playing) {
            return;
        }
        // Source: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.10/api/UnityEngine.InputSystem.EnhancedTouch.Touch.html
        foreach (var touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
        {
            if (touch.began)
            {
                SpawnProjectile();
            }
        }
    }

    private void SpawnProjectile()
    {
        GlowingProjectile projectile = Instantiate(projectilePrefab);
        projectile.transform.localPosition = transform.position;
        projectile.transform.rotation = Quaternion.AngleAxis(90, transform.right) * transform.rotation;
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.AddForce(Camera.main.transform.forward * Random.Range(minProjectileSpeed, maxProjectileSpeed));
    }
}
