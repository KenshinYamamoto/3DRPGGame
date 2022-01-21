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
    [SerializeField] PlayerUIManager playerUIManager;
    [SerializeField] Transform target;
    public int maxHp = 100;
    int hp;
    public int maxStanima = 100;
    int stamina;
    bool isDie;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        stamina = maxStanima;
        playerUIManager.Init(this);
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        HideClliderWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie)
        {
            return;
        }

        // 方向キーで移動させる
        // キーボード入力で移動させたい
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        // 攻撃入力:Spaceボタンを押したら
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        IncreaseStamina();
    }

    private void FixedUpdate()
    {
        if (isDie)
        {
            return;
        }

        Vector3 direction = transform.position + new Vector3(x, 0f, z) * moveSpeed;
        transform.LookAt(direction);
        // 速度設定
        rb.velocity = new Vector3(x, 0f, z) * moveSpeed;
        animator.SetFloat("MoveSpeed", rb.velocity.magnitude);
    }

    void IncreaseStamina()
    {
        stamina++;
        if (stamina >= maxStanima)
        {
            stamina = maxStanima;
        }
        playerUIManager.UpdateStamina(stamina);
    }

    void Attack()
    {
        if(stamina >= 20)
        {
            stamina -= 60;
            playerUIManager.UpdateStamina(stamina);
            LookAtTarget();
            animator.SetTrigger("Attack");
        }

    }

    void LookAtTarget()
    {
        if(target == null)
        {
            return;
        }
        float distance = Vector3.Distance(transform.position, target.position);
        if(distance <= 2f)
        {
            transform.LookAt(target);
        }
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

    void Damage(int damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            GameOver();
        }
        playerUIManager.UpdateHP(hp);
        Debug.Log("Playerhp:" + hp);
    }

    void GameOver()
    {
        hp = 0;
        GameObserver.gameObserver.ShowGameOverText();
        animator.SetTrigger("Die");
        isDie = true;
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDie)
        {
            return;
        }
        // ダメージを与えるものに当たったら
        DanageManager danageManager = other.GetComponent<DanageManager>();
        if (danageManager != null)
        {
            // ぶつかった対象がDamageManagerを持っていたら
            Debug.Log("プレイヤーはダメージを受ける");
            animator.SetTrigger("Hurt");
            Damage(danageManager.damage);
        }
    }
}
