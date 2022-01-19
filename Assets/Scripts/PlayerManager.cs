using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;

    float x;
    float z;
    public float moveSpeed = 3;
    public Collider weaponCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        HideClliderWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        // 方向キーで移動させる
        // キーボード入力で移動させたい
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        // 攻撃入力:Spaceボタンを押したら
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.position + new Vector3(x, 0f, z) * moveSpeed;
        transform.LookAt(direction);
        // 速度設定
        rb.velocity = new Vector3(x, 0f, z) * moveSpeed;
        animator.SetFloat("MoveSpeed", rb.velocity.magnitude);
    }

    // 武器の判定を有効にしたり無効にしたりする
    public void ShowClliderWeapon()
    {
        weaponCollider.enabled = true;
    }
    public void HideClliderWeapon()
    {
        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ダメージを与えるものに当たったら
        DanageManager danageManager = other.GetComponent<DanageManager>();
        if (danageManager != null)
        {
            // ぶつかった対象がDamageManagerを持っていたら
            Debug.Log("プレイヤーはダメージを受ける");
            animator.SetTrigger("Hurt");
        }
    }
}
