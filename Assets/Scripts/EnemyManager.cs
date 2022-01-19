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

    void Start()
    {
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

    // ����̔����L���ɂ����薳���ɂ����肷��
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
        // �_���[�W��^������̂ɓ���������
        DanageManager danageManager = other.GetComponent<DanageManager>();
        if(danageManager != null)
        {
            // �Ԃ������Ώۂ�DamageManager�������Ă�����
            Debug.Log("�G�̓_���[�W���󂯂�");
            animator.SetTrigger("Hurt");
        }
    }
}
