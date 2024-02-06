using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWJohn : MonoBehaviour
{
    public float turnSpeed = 20;
    public float moveSpeed = 1;
    public float JumpForce = 10f;
    public float outOfBounds = -10f;
    public GameObject checkPointAreaObject;
    private Vector3 _Movement;
    public float GravityModifier = 1f;
    public bool IsOnGround = true;
    public bool isAtCheckpoint = false;

    //private Animator m_Animator;
    private Rigidbody _Rigidbody;
    private Quaternion _Rotation = Quaternion.identity;
    private Vector3 _defaultGravity = new Vector3(0f, -9.81f, 0f);
    private Vector3 _startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        //_Animator = GetComponent<Animator>();
        _Rigidbody = GetComponent<Rigidbody>();
        Physics.gravity = _defaultGravity;
        Physics.gravity *= GravityModifier;
        _startingPosition = transform.position;
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

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == checkPointAreaObject)
        {
            isAtCheckpoint = true;
            //Debug.Log(_startingPosition);
            _startingPosition = checkPointAreaObject.transform.position;
            //Debug.Log(_startingPosition);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            _Rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }

        if(transform.position.y < outOfBounds)
        {
            transform.position = _startingPosition;
        }
    }
}