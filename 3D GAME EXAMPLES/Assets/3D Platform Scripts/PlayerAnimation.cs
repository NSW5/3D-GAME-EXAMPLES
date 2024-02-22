using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private NoahsPlayerMovement _characterMovement;
    private Animator _characterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _characterAnimator = GetComponent<Animator>();
        _characterMovement = GameObject.Find("Player").GetComponent<NoahsPlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerWalking();
        PlayerJumping();
    }

    private void PlayerWalking()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            _characterAnimator.SetBool("IsWalking", true);
        }
        else
        {
            _characterAnimator.SetBool("IsWalking", false);
        }
    }

    public void PlayerJumping()
    {
        if (_characterMovement.IsPlayerOnGround())
        {
            _characterAnimator.SetBool("IsOnGround", true);
        }
        else
        {
            _characterAnimator.SetBool("IsOnGround", false);
        }
    }
}