using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

enum PlayerState
{
    Normal, DoubleJump, FastFalling,
    LongHorn, WideSight, ConstSpeed
}

public partial class PlayerController : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    Vector2 velocity_;
    public bool Isground { get; private set; } = true;
    PlayerState state = PlayerState.Normal;
    float stateTimer = 0f;
    Vector3 initPos;

    [SerializeField] GameRuleManegenent gameRuleManeger;

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

    [Header("look only")]
    [SerializeField] int jumpableTime = 0;
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
    [SerializeField] CameraView cameraView;

    [SerializeField] GameObject itemParticle;

    public float PositionLimit { get { return warpPosition; } }
    bool isHited = false;



    public void Init()
    {
        chargeTime = 0f;
        chargeLate = 0f;

        transform.position = initPos;
        transform.rotation = Quaternion.identity;

        face.SetActive(true);
        whiteDeer.SetActive(false);
        currentJumpPower = jumpPower;

        StateReset();

        jumpableTime = maxJumpableTime;
        cameraView.SetNormalFast();

        rigidbody.bodyType = RigidbodyType2D.Dynamic;

        isHited = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();

        initPos = transform.position;

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameRuleManegenent.isGameDoing) return;
        if (Time.timeScale == 0) return;

        PlatformMoving();

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

                transform.parent = null;
                PlaySE(AudioType.Jump);

                if(!Isground)
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
        if (!Isground)
        {
            if (state != PlayerState.ConstSpeed)
            {
                float fastItemLate = state == PlayerState.FastFalling ? fastXMoveLate : 1;
                float chargingLate = InputManeger.IsCharging() ? chargingAccelerateLate : 1;

                velocity_.x += InputManeger.HorizontalAxis() * acceleration * chargingLate * fastItemLate * Time.deltaTime;

                velocity_.x = Mathf.Clamp(velocity_.x, -limitSpeed, limitSpeed);
            }
            else
            {
                velocity_.x = InputManeger.HorizontalAxis() * limitSpeed;
            }
        }

        if(transform.position.x > warpPosition)
        {
            transform.position += Vector3.left * (2 * warpPosition);
        }
        else if(transform.position.x < -warpPosition)
        {
            transform.position += Vector3.right * (2 * warpPosition);
        }
    }

    void StateReset()
    {
        state = PlayerState.Normal;

        stateTimer = 0;

        maxJumpableTime = 1;
        if (!Isground)
            jumpableTime--;

        currentJumpPower = jumpPower;
        rigidbody.gravityScale = 1f;

        horn.SetActive(true);
        longHorn.SetActive(false);

        cameraView.IsSizeWide = false;

        itemParticle.SetActive(false);
    }

    void StateChanger()
    {
        if (state != PlayerState.Normal)
        {
            stateTimer += Time.deltaTime;
        }

        if (stateTimer > otherStateDuration)
        {
            StateReset();
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            if (rigidbody.velocity.y <= 0)
            {
                Isground = true;
                jumpableTime = maxJumpableTime;

                var platformType = collision.gameObject.GetComponent<PlatformScript>().type;
                if (platformType != PlatformType.Ice)
                {
                    rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
                }
                if(platformType == PlatformType.Move)
                {
                    transform.parent = collision.transform;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform") && Isground)
        {
            Isground = false;
            jumpableTime--;

            transform.parent = null;

            if(collision.gameObject.GetComponent<PlatformScript>().type != PlatformType.Ice)
            {
                rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            PlaySE(AudioType.ItemGet);

            var itemName = collision.gameObject.name;

            if (itemName.Contains(ItemType.HighJump.ToString()))
            {
                if (state != PlayerState.FastFalling)
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, highJumpPower);
                else
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, fastHighJumpPower);

                PlaySE(AudioType.HighJump);
            }
            else if (itemName.Contains(ItemType.DoubleJump.ToString()))
            {
                StateReset();
                state = PlayerState.DoubleJump;

                maxJumpableTime = 2;
                jumpableTime++;

                itemParticle.SetActive(true);
            }
            else if (itemName.Contains(ItemType.FastFall.ToString()))
            {
                StateReset();
                state = PlayerState.FastFalling;

                currentJumpPower = fastJumpPower;
                rigidbody.gravityScale = fastGravityScale;

                itemParticle.SetActive(true);
            }
            else if (itemName.Contains(ItemType.LongHorn.ToString()))
            {
                StateReset();
                state = PlayerState.LongHorn;

                horn.SetActive(false);
                longHorn.SetActive(true);

                itemParticle.SetActive(true);
            }
            else if (itemName.Contains(ItemType.WideSight.ToString()))
            {
                StateReset();
                state = PlayerState.WideSight;
                cameraView.IsSizeWide = true;
            }
            else if (itemName.Contains(ItemType.ConstSpeed.ToString()))
            {
                StateReset();
                state = PlayerState.ConstSpeed;
                itemParticle.SetActive(true);
            }

            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Water"))
        {
            if (isHited) return;
            isHited = true;

            transform.parent = null;
            face.SetActive(false);
            horn.SetActive(false);
            longHorn.SetActive(false);
            whiteDeer.SetActive(true);

            transform.localEulerAngles = new Vector3(0, 0, -90);
            rigidbody.bodyType = RigidbodyType2D.Static;
            PlaySE(AudioType.Miss);
            gameRuleManeger.SendMiss();
        }
        else if (collision.gameObject.name == "Goal")
        {
            if (isHited) return;
            isHited = true;

            BodyColor = Color.white;
            rigidbody.bodyType = RigidbodyType2D.Static;
            PlaySE(AudioType.Clear);
            gameRuleManeger.SendClear();
        }
    }
    
    void PlatformMoving()
    {
        if(transform.parent != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -warpPosition, warpPosition),
                transform.position.y, transform.position.z);
        }
    }
}
