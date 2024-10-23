using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchYRotation : MonoBehaviour
{
    [SerializeField] GameObject matchObject;


    // Update is called once per frame
    void Update()
    {
        transform.SetLocalPositionAndRotation(transform.localPosition, Quaternion.AngleAxis(matchObject.transform.rotation.y, Vector3.up));
    }
}
