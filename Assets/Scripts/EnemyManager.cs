using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Enemyのアニメーションを設定
 * ・Idle:遠い:7以上:Speedを0
 * ・Run:やや近い:7以下:Speedを2
 * ・Attack:近い:2以下:Speedを0
 */

public class EnemyManager : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public Collider weaponCollider;
    public Transform target;
    [SerializeField] EnemyUIManager enemyUIManager;
    public int maxHp = 100;
    int hp;

    void Start()
    {
        hp = maxHp;
        enemyUIManager.Init(this);
        // Playerを追跡するコードの実装
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.destination = target.position;
        HideClliderWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
        animator.SetFloat("Distance", agent.remainingDistance);
    }

    public void LookAtTarget()
    {
        transform.LookAt(target);
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

        if (hp <= 0)
        {
            GameClear();
        }
        enemyUIManager.UpdateUI(hp);
        Debug.Log("Enemyhp:" + hp);
    }

    void GameClear()
    {
        hp = 0;
        animator.SetTrigger("Die");
        Destroy(gameObject, 2f);
        GameObserver.gameObserver.ShowGameClearText();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ダメージを与えるものに当たったら
        DanageManager danageManager = other.GetComponent<DanageManager>();
        if(danageManager != null)
        {
            // ぶつかった対象がDamageManagerを持っていたら
            Debug.Log("敵はダメージを受ける");
            animator.SetTrigger("Hurt");
            Damage(danageManager.damage);
        }
    }
}
