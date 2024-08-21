using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlatformType
{
    Normal,
    Ice,
    Move,
    Belt
}

public class PlatformScript : MonoBehaviour
{
    [SerializeField] public static Sprite[] sprites;

    [SerializeField] public PlatformType type;

    [Tooltip("Ž©•ª‚ª“®‚­‘¬‚³")]
    [SerializeField] public float moveSpeed = 0f;
    [SerializeField] public float moveRange = 0f;
    float moveDirection = 1f;
    [Tooltip("æ‚Á‚Ä‚éƒvƒŒƒCƒ„[‚ð“®‚©‚·‘¬‚³")]
    [SerializeField] public float movePlayerVelocity = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if(moveSpeed != 0)
        {
            movePlayerVelocity = moveSpeed;
        }

        if(type == PlatformType.Belt && Random.value > 0.5f)
        {
            moveDirection = -1f;
            GetComponent<SpriteRenderer>().flipX = true;
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
        if(collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().isground)
            {
                collision.transform.position += new Vector3(movePlayerVelocity * moveDirection * Time.deltaTime, 0, 0);
            }
        }
    }

    public void Copy(PlatformScript platform, float range)
    {
        this.type = platform.type;
        this.moveSpeed = platform.moveSpeed;
        this.movePlayerVelocity = platform.movePlayerVelocity;
        this.moveRange = range;
    }
}
