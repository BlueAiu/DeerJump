using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

enum PlayerState
{
    Normal, DoubleJump, FastFalling
}

public partial class PlayerController : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    Vector2 velocity_;
    public bool isground { get; private set; } = true;
    PlayerState state = PlayerState.Normal;
    float stateTimer = 0f;

    [Header("垂直方向の移動")]
    [Tooltip("終端速度")]
    [SerializeField] float terminalVelocity = 10f;
    [Tooltip("最大までタメた時のジャンプ力")]
    [SerializeField] float jumpPower = 3f;
    float currentJumpPower;

    float chargeTime = 0f;
    [Tooltip("タメが最大になるまでの時間(s)")]
    [SerializeField] float chargeLimit = 1f;
    [Tooltip("タメの最小値(1未満)")]
    [SerializeField] float chargeMin = 0.1f;
    float chargeLate = 0f;

    int jumpableTime = 0;
    int maxJumpableTime = 1;


    [Header("水平方向の移動")]
    [Tooltip("左右入力の加速度")]
    [SerializeField] float acceleration = 1f;
    [Tooltip("タメ中の左右速度の倍率")]
    [SerializeField] float chargingAccelerateLate = 0.5f;
    [Tooltip("左右速度の制限")]
    [SerializeField] float limitSpeed = 5f;
    [Tooltip("左右ワープの座標")]
    [SerializeField] float warpPosition = 10f;


    [Header("アイテム関係")]
    [SerializeField] float highJumpPower = 10f;
    [SerializeField] float otherStateDuration = 10f;
    [SerializeField] float fastGravityScale = 2f;
    [SerializeField] float fastJumpPower = 10f;
    [SerializeField] float fastHighJumpPower = 20f;
    [SerializeField] float fastChargeLate = 2f;
    [SerializeField] float fastXMoveLate = 1.5f;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        currentJumpPower = jumpPower;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity_ = rigidbody.velocity;

        VerticalVelocity();
        HorizontalVelocity();

        rigidbody.velocity = velocity_;

        StateChanger();

        Shrinking();

        FullChargeColor();
    }

    void VerticalVelocity() {

        if (InputManeger.IsCharging())
        {
            chargeTime += Time.deltaTime * (state == PlayerState.FastFalling ? fastChargeLate : 1);

            chargeTime = Mathf.Min(chargeTime, chargeLimit);
            chargeLate = chargeTime / chargeLimit;
        }
        else if (chargeTime > 0f)
        {
            chargeLate = chargeMin + chargeLate * (1 - chargeMin);

            if (jumpableTime > 0)
            {
                //Jump
                float thisJumpPower = currentJumpPower * Mathf.Sqrt(chargeLate);
                velocity_.y = Mathf.Min(Mathf.Max(thisJumpPower, velocity_.y + thisJumpPower), currentJumpPower);

                PlaySE(AudioType.Jump);

                if(!isground)
                {
                    jumpableTime--;
                }
            }
            chargeTime = 0; chargeLate = 0;
        }

        velocity_.y = Mathf.Max(velocity_.y, -terminalVelocity * (state == PlayerState.FastFalling ? fastGravityScale : 1));
    }

    void HorizontalVelocity()
    {
        if (!isground)
        {
            float fastItemLate = state == PlayerState.FastFalling ? fastXMoveLate : 1;
            float chargingLate = InputManeger.IsCharging() ? chargingAccelerateLate : 1;

            velocity_.x += InputManeger.HorizontalAxis() * acceleration * chargingLate * fastItemLate * Time.deltaTime;

            velocity_.x = Mathf.Clamp(velocity_.x, -limitSpeed, limitSpeed);
        }

        if(transform.position.x > warpPosition)
        {
            transform.position += Vector3.left * 2 * warpPosition;
        }
        else if(transform.position.x < -warpPosition)
        {
            transform.position += Vector3.right * 2 * warpPosition;
        }
    }

    void StateChanger()
    {
        if (state != PlayerState.Normal)
        {
            stateTimer += Time.deltaTime;
        }

        if (stateTimer > otherStateDuration)
        {
            state = PlayerState.Normal;

            maxJumpableTime = 1;
            if(!isground)
                jumpableTime--;

            currentJumpPower = jumpPower;
            rigidbody.gravityScale = 1f;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            if (rigidbody.velocity.y <= 0)
            {
                isground = true;
                jumpableTime = maxJumpableTime;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            isground = false;
            jumpableTime--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            PlaySE(AudioType.ItemGet);

            var itemName = collision.gameObject.name;

            if (itemName.Contains("HighJump"))
            {
                if (state != PlayerState.FastFalling)
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, highJumpPower);
                else
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, fastHighJumpPower);

                PlaySE(AudioType.HighJump);
            }
            else if (itemName.Contains("DoubleJump"))
            {
                state = PlayerState.DoubleJump;
                stateTimer = 0f;

                maxJumpableTime = 2;
                jumpableTime++;

                currentJumpPower = jumpPower;
                rigidbody.gravityScale = 1f;
            }
            else if (itemName.Contains("FastFalling"))
            {
                state = PlayerState.FastFalling;
                stateTimer = 0f;

                maxJumpableTime = 1;
                jumpableTime--;

                currentJumpPower = fastJumpPower;
                rigidbody.gravityScale = fastGravityScale;
            }

            Destroy(collision.gameObject);
        }
    }
}
