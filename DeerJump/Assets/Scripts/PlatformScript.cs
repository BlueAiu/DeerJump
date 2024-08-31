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

    [Tooltip("é©ï™Ç™ìÆÇ≠ë¨Ç≥")]
    [SerializeField] public float moveSpeed = 0f;
    [SerializeField] public float moveRange = 0f;
    float moveDirection = 1f;
    [Tooltip("èÊÇ¡ÇƒÇÈÉvÉåÉCÉÑÅ[ÇìÆÇ©Ç∑ë¨Ç≥")]
    [SerializeField] public float movePlayerVelocity = 0f;

    float actionTimer = 0;
    [SerializeField] Sprite[] beltSprites;
    [SerializeField] float beltSpritePeriod = 0.3f;

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
        else if(type == PlatformType.Normal)
        {
            GetComponent<BoxCollider2D>().sharedMaterial = noneSlip;
        }
        if (type == PlatformType.Belt && Random.value > 0.5f)
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

        if(type == PlatformType.Belt)
        {
            Belting();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && movePlayerVelocity > 0)
        {
            var playerCon = collision.gameObject.GetComponent<PlayerController>();
            if (playerCon.Isground)
            {
                var playerTrans = playerCon.transform;
                var playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

                //var movement = Vector2.right * (movePlayerVelocity * moveDirection * Time.deltaTime);
                playerRb.AddForce(Vector2.right * (movePlayerVelocity * moveDirection - playerRb.velocity.x), ForceMode2D.Impulse);
                //playerRb.velocity = new Vector2(movePlayerVelocity * moveDirection, playerRb.velocity.y);
                //playerTrans.position += Vector3.right * (movePlayerVelocity * moveDirection * Time.deltaTime);

                float posLimit = playerCon.PositionLimit;
                playerTrans.position = new Vector3
                    (Mathf.Clamp(playerTrans.position.x, -posLimit, posLimit), playerTrans.position.y, playerTrans.position.z);
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

    void Belting()
    {
        if (!GameRuleManegenent.isGameDoing) return;

        actionTimer += Time.deltaTime;
        var beltLate = actionTimer / (movePlayerVelocity * beltSpritePeriod) % beltSprites.Length;
        GetComponent<SpriteRenderer>().sprite = beltSprites[Mathf.FloorToInt(beltLate)];
    }
}
