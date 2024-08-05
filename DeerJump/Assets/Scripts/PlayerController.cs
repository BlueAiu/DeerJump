using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    Vector2 velocity_;

    [Header("垂直方向の移動")]
    [Tooltip("終端速度")]
    [SerializeField] float terminalVelocity = 10f;
    [Tooltip("最大までタメた時のジャンプ力")]
    [SerializeField] float jumpPower = 3f;

    float chargeTime = 0f;
    [Tooltip("タメが最大になるまでの時間(s)")]
    [SerializeField] float chargeLimit = 1f;
    float chargeLate = 0f;


    [Header("水平方向の移動")]
    [Tooltip("左右入力の加速度")]
    [SerializeField] float acceleration = 1f;
    [Tooltip("左右速度の制限")]
    [SerializeField] float limitSpeed = 5f;

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
            velocity_.y = jumpPower * chargeLate;
            chargeTime = 0; chargeLate = 0;
        }

        velocity_.y = Mathf.Max(velocity_.y, -terminalVelocity);
    }

    void HorizontalVelocity()
    {
        velocity_.x += InputManeger.HorizontalAxis() * acceleration * Time.deltaTime;
        velocity_.x = Mathf.Clamp(velocity_.x, -limitSpeed, limitSpeed);
    }
}
