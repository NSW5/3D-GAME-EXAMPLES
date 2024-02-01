using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWJohn : MonoBehaviour
{
    public float turnSpeed = 20;
    public float moveSpeed = 1;
    public float JumpForce = 10f;
    private Vector3 _Movement;
    public float GravityModifier = 1f;
    public bool IsOnGround = true;

    //private Animator m_Animator;
    private Rigidbody _Rigidbody;
    private Quaternion _Rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        //_Animator = GetComponent<Animator>();
        _Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _Movement.Set(horizontal, 0f, vertical);
        _Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        //m_Animator.SetBool("IsWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, _Movement, turnSpeed * Time.deltaTime, 0f);
        _Rotation = Quaternion.LookRotation(desiredForward);

        _Rigidbody.MovePosition(_Rigidbody.position + _Movement * moveSpeed * Time.deltaTime);
        _Rigidbody.MoveRotation(_Rotation);
    }

    //void OnAnimatorMove()
    //{
    //m_Rigidbody.MovePosition(_Rigidbody.position + _Movement * //m_Animator.deltaPosition.magnitude);
    //m_Rigidbody.MoveRotation(m_Rotation);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            _Rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }
    }
}