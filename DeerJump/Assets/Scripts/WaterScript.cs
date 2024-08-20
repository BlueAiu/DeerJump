using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    [SerializeField] float surfaceTime = 13.5f;
    [SerializeField] float goalTime = 40f;
    [SerializeField] Transform goal;

    float velocity;

    // Start is called before the first frame update
    void Start()
    {
        float distance = goal.position.y;
        float surfaceToGoalTime = goalTime - surfaceTime;
        velocity = distance / surfaceToGoalTime;

        transform.position = velocity * surfaceTime * Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up *(velocity * Time.deltaTime));
    }
}
