using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float shiftYPos = 2f;
    [SerializeField] float trackSpeed = 1f;
    float shiftZpos;

    private void Start()
    {
        shiftZpos = transform.position.z;
    }

    void Update()
    {
        float difference = player.position.y + shiftYPos - transform.position.y;
        if (difference > 0)
        {
            transform.Translate(Vector3.up * (difference * trackSpeed * Time.deltaTime));
        }
        else
        {
            transform.position = new Vector3(0, player.position.y + shiftYPos, shiftZpos);
        }
    }
}
