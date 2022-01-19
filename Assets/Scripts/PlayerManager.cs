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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // �����L�[�ňړ�������
        // �L�[�{�[�h���͂ňړ���������
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        // �U������:Space�{�^������������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.position + new Vector3(x, 0f, z) * moveSpeed;
        transform.LookAt(direction);
        // ���x�ݒ�
        rb.velocity = new Vector3(x, 0f, z) * moveSpeed;
        animator.SetFloat("MoveSpeed", rb.velocity.magnitude);
    }
}
