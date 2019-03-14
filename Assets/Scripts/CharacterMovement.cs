using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private bool _lookRight = true;
    private bool _onGround = false;
    private float _groundCheckerRadius = 0.2f;
    private float _jumpForce = 200f;

    [SerializeField] private float maxSpeed = 3;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject weapon;

    // Use this for initialization
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_onGround && Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Ground", false);
            
            _rigidbody2D.AddForce(new Vector2(0, _jumpForce));
        }

        var attack = Input.GetButtonDown("Fire1");

        if (attack)
        {
            animator.SetTrigger("Attack");
            weapon.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        var direction = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(direction));
        _rigidbody2D.velocity = new Vector2(maxSpeed * direction, _rigidbody2D.velocity.y);

        if (direction > 0 && !_lookRight)
        {
            Flip();
        }
        else if (direction < 0 && _lookRight)
        {
            Flip();
        }

        _onGround = Physics2D.OverlapCircle(groundChecker.position, _groundCheckerRadius, ground);
        
        animator.SetBool("Ground", _onGround);
        animator.SetFloat("vSpeed", _rigidbody2D.velocity.y);
    }

    private void Flip()
    {
        _lookRight = !_lookRight;

        var localScale = transform.localScale;

        localScale.x *= -1;

        transform.localScale = localScale;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Crank") && Input.GetButtonDown("Use"))
        {
            var crankScript = other.GetComponent<ToggleCrank>();
            crankScript.toggle = true;
        }
    }
}