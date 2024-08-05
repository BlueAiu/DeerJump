using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float shiftYPos = 2f;
    float shiftZpos;

    private void Start()
    {
        shiftZpos = transform.position.z;
    }

    void Update()
    {
        transform.position = new Vector3(0, player.position.y + shiftYPos, shiftZpos);
    }
}
