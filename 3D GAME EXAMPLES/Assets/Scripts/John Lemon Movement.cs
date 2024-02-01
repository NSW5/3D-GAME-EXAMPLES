using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20;
    private Vector3 _Movement;
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

    }

    //void OnAnimatorMove()
    //{
    //m_Rigidbody.MovePosition(_Rigidbody.position + _Movement * //m_Animator.deltaPosition.magnitude);
    //m_Rigidbody.MoveRotation(m_Rotation);
    //}
}
