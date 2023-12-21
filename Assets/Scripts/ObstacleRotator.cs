using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotator : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] Vector3 rotateDirection;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateDirection * Time.deltaTime * rotateSpeed, Space.Self);
    }

}
