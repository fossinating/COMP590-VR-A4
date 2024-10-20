using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitResponsive : MonoBehaviour
{
    [SerializeField] public int pointValue = 0;

    abstract public void Hit();
}
