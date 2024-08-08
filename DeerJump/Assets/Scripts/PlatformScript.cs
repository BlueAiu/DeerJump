using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [Tooltip("é©ï™Ç™ìÆÇ≠ë¨Ç≥")]
    [SerializeField] float moveSpeed = 0f;
    [SerializeField] float moveRange = 0f;
    float moveDirection = 1f;
    [Tooltip("èÊÇ¡ÇƒÇÈÉvÉåÉCÉÑÅ[ÇìÆÇ©Ç∑ë¨Ç≥")]
    [SerializeField] float movePlayerVelocity = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if(moveSpeed != 0)
        {
            movePlayerVelocity = moveSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(moveSpeed * moveDirection * Time.deltaTime, 0, 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-moveRange,moveRange), transform.position.y, 0);
        if(Mathf.Abs(transform.position.x) == moveRange && moveRange != 0)
        {
            moveDirection = -moveDirection;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerController>().isground)
            {
                collision.transform.position += new Vector3(movePlayerVelocity * moveDirection * Time.deltaTime, 0, 0);
            }
        }
    }
}
