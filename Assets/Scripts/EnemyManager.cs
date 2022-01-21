using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Enemy�̃A�j���[�V������ݒ�
 * �EIdle:����:7�ȏ�:Speed��0
 * �ERun:���߂�:7�ȉ�:Speed��2
 * �EAttack:�߂�:2�ȉ�:Speed��0
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
        // Player��ǐՂ���R�[�h�̎���
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

    // ����̔����L���ɂ����薳���ɂ����肷��
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
        // �_���[�W��^������̂ɓ���������
        DanageManager danageManager = other.GetComponent<DanageManager>();
        if(danageManager != null)
        {
            // �Ԃ������Ώۂ�DamageManager�������Ă�����
            Debug.Log("�G�̓_���[�W���󂯂�");
            animator.SetTrigger("Hurt");
            Damage(danageManager.damage);
        }
    }
}
