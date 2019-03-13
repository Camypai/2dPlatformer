using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z),
            new Vector3(_target.position.x, _target.position.y, transform.position.z), _speed * Time.deltaTime);
    }
}