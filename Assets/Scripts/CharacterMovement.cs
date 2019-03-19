using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Helpers;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private bool _lookRight = true;
    private bool _onGround = false;
    private float _groundCheckerRadius = 0.2f;
    private float _jumpForce = 200f;
    private bool _die = false;
    private RaycastHit2D _raycastRight;
    private RaycastHit2D _raycastLeft;
    private Weapon _weapon;
    private Enemy _enemy;
    
    public int Heals = 100;

    [SerializeField] private float maxSpeed = 3;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject weapon;
    [SerializeField] private float raycastLength = 2.5f;
    [SerializeField] private LayerMask enemies;
    [SerializeField] private int damage = 20;
//    [SerializeField] private GameObject test;

    public void GetDamage(int damage)
    {
        if (Heals > 0)
        {
            Heals -= damage;
        }
        else
        {
            if (!_die)
            {
                animator.SetTrigger("Die");
                _die = true;
            }
        }
    }

    private void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _weapon = weapon.GetComponent<Weapon>();
//        test.transform.position = weapon.transform.position;
    }

    private void Update()
    {
        var position = weapon.transform.position;
        _raycastRight = Physics2D.Raycast(position, Vector2.right, raycastLength, enemies);
        _raycastLeft = Physics2D.Raycast(position, Vector2.left, raycastLength, enemies);

//        var q = Physics2D.OverlapCircle(weapon.transform.position, raycastLength, enemies);
        
        if (_onGround && Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Ground", false);

            _rigidbody2D.AddForce(new Vector2(0, _jumpForce));
        }

        var attack = Input.GetButtonDown("Fire1");

        if(_raycastLeft.collider != null)
        {
//            print(_raycastLeft.collider.gameObject.name);
            _enemy = _raycastLeft.collider.gameObject.GetComponent<Enemy>();
            Attack(attack, _enemy);
        }
        if(_raycastRight.collider != null)
        {
//            print(_raycastRight.collider.gameObject.name);
            _enemy = _raycastRight.collider.gameObject.GetComponent<Enemy>();
           Attack(attack, _enemy); 
        }
        
        Debug.DrawRay(_raycastRight.point, Vector3.right, Color.red);
        Debug.DrawRay(_raycastLeft.point, Vector3.left, Color.yellow);
    }

    private void FixedUpdate()
    {
        var direction = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(direction));
        _rigidbody2D.velocity = new Vector2(maxSpeed * direction, _rigidbody2D.velocity.y);

        if (direction > 0 && !_lookRight)
        {
            transform.Flip(ref _lookRight);
        }
        else if (direction < 0 && _lookRight)
        {
            transform.Flip(ref _lookRight);
        }

        _onGround = Physics2D.OverlapCircle(groundChecker.position, _groundCheckerRadius, ground);

        animator.SetBool("Ground", _onGround);
        animator.SetFloat("vSpeed", _rigidbody2D.velocity.y);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Crank") && Input.GetButtonDown("Use"))
        {
            var crankScript = other.GetComponent<ToggleCrank>();
            crankScript.toggle = true;
        }
    }

    private void Attack(bool attack, Enemy enemy)
    {
        if (attack)
        {
            animator.SetTrigger("Attack");
            if(_enemy != null)
            _weapon.SetDamage(damage, enemy); 
//            weapon.SetActive(true);
        }
    }
}