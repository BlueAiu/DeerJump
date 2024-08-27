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

public struct PlatformInfo
{
    public PlatformType type;
    public float moveSpeed;
    public float beltSpeed;

    public PlatformInfo(int n = 0)
    {
        type = PlatformType.Normal;
        moveSpeed = 0;
        beltSpeed = 0;
    }
}

public class PlatformScript : MonoBehaviour
{
    public static Sprite[] sprites;

    [SerializeField] public PlatformType type;
    [SerializeField] PhysicsMaterial2D noneSlip;
    [SerializeField] PhysicsMaterial2D fullSlip;

    [Tooltip("自分が動く速さ")]
    [SerializeField] public float moveSpeed = 0f;
    [SerializeField] public float moveRange = 0f;
    float moveDirection = 1f;
    [Tooltip("乗ってるプレイヤーを動かす速さ")]
    [SerializeField] public float movePlayerVelocity = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if(moveSpeed != 0)
        {
            movePlayerVelocity = moveSpeed;
        }

        if (type == PlatformType.Ice)
        {
            GetComponent<BoxCollider2D>().sharedMaterial = fullSlip;
        }
        else if (type == PlatformType.Belt && Random.value > 0.5f)
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
            var playerCon = collision.gameObject.GetComponent<PlayerController>();
            if (playerCon.isground)
            {
                var player = collision.transform;
                player.position += new Vector3(movePlayerVelocity * moveDirection * Time.deltaTime, 0, 0);

                float posLimit = playerCon.PositionLimit;
                player.position = new Vector3
                    (Mathf.Clamp(player.position.x, -posLimit, posLimit), player.position.y, player.position.z);
            }
        }
    }

    public void Copy(PlatformInfo platform, float range)
    {
        this.type = platform.type;
        this.moveSpeed = platform.moveSpeed;
        this.movePlayerVelocity = platform.beltSpeed;
        this.moveRange = range;
    }
}
