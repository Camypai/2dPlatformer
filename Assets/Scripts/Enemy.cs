using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Helpers;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _hp = 100;
    private bool _death = false;
    private bool _lookRight = false;
    private Rigidbody2D _rigidbody2D;
    private bool _inAttack = false;
    
    public Transform Player;
    public bool isLook = false;
    public bool isAggro;

    [SerializeField] private Animator animator;
    [SerializeField] private float attackRadius;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float distance;
    [SerializeField] private int damage = 5;

    private void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_death)
        {
            if (isLook)
            {
                if (WhereIsPlayer() < 0 && _lookRight)
                {
                    transform.Flip(ref _lookRight);
                }
    
                if (WhereIsPlayer() > 0 && !_lookRight)
                {
                    transform.Flip(ref _lookRight);
                }
            }

            if (isAggro)
            {
                Aggro();
            }
            else
            {
                CancelInvoke("Attack");
            }
        }
        animator.SetFloat("Speed", Mathf.Abs(_rigidbody2D.velocity.x));
    }

    private float WhereIsPlayer()
    {
        return Player.position.x - transform.position.x;
    }

    public void GetDamage(int damage)
    {
        if (_hp > 0)
        {
            _hp -= damage;
            animator.SetTrigger("getDamage");
        }
        else
        {
            Die();
        }
        
    }

    private void Aggro()
    {
        if (CheckDistanceForAttack() && !_inAttack)
        {
            _inAttack = !_inAttack;
            InvokeRepeating("Attack", 0, 1.5f);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(maxSpeed * WhereIsPlayer()+distance, _rigidbody2D.velocity.y);
        }
    }

    private bool CheckDistanceForAttack()
    {
        return Vector3.Distance(transform.position, Player.transform.position) <= distance;
    }

    private void Attack()
    {
        var character = Player.GetComponent<CharacterMovement>();
        animator.SetTrigger("Attack");
        character.GetDamage(damage);
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        _death = true;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        CancelInvoke("Attack");
    }
}