using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    Vector2 velocity_;
    public bool isground { get; private set; } = true;

    [Header("垂直方向の移動")]
    [Tooltip("終端速度")]
    [SerializeField] float terminalVelocity = 10f;
    [Tooltip("最大までタメた時のジャンプ力")]
    [SerializeField] float jumpPower = 3f;

    float chargeTime = 0f;
    [Tooltip("タメが最大になるまでの時間(s)")]
    [SerializeField] float chargeLimit = 1f;
    [Tooltip("タメの最小値(1未満)")]
    [SerializeField] float chargeMin = 0.1f;
    float chargeLate = 0f;


    [Header("水平方向の移動")]
    [Tooltip("左右入力の加速度")]
    [SerializeField] float acceleration = 1f;
    [Tooltip("左右速度の制限")]
    [SerializeField] float limitSpeed = 5f;
    [Tooltip("左右ワープの座標")]
    [SerializeField] float warpPosition = 10f;

    [SerializeField] TMP_Text TMP_Text;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity_ = rigidbody.velocity;

        VerticalVelocity();
        HorizontalVelocity();

        rigidbody.velocity = velocity_;

        TMP_Text.text = chargeLate.ToString();
    }

    void VerticalVelocity() {

        if (InputManeger.IsCharging())
        {
            chargeTime = Mathf.Min(chargeTime + Time.deltaTime, chargeLimit);
            chargeLate = chargeTime / chargeLimit;
        }
        else if (chargeTime > 0f)
        {
            chargeLate = chargeMin + chargeLate * (1 - chargeMin);

            if (isground)
            {
                velocity_.y = jumpPower * chargeLate;
                isground = false;
            }
            chargeTime = 0; chargeLate = 0;
        }

        velocity_.y = Mathf.Max(velocity_.y, -terminalVelocity);
    }

    void HorizontalVelocity()
    {
        if (!isground)
        {
            velocity_.x += InputManeger.HorizontalAxis() * acceleration * Time.deltaTime;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            if (rigidbody.velocity.y <= 0)
            {
                isground = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            isground = false;
        }
    }
}
