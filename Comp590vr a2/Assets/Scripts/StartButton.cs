using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{

    [SerializeField] GameManager gameManager;
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GlowingProjectile>() != null)
        {
            gameManager.StartGame();
        }
    }
}
