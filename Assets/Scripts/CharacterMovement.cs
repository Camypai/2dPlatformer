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

    [SerializeField] private float _maxSpeed = 3;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private GameObject _crank;

    // Use this for initialization
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_onGround && Input.GetButtonDown("Jump"))
        {
            _rigidbody2D.AddForce(new Vector2(0, _jumpForce));
        }
    }

    private void FixedUpdate()
    {
        var _direction = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = new Vector2(_maxSpeed * _direction, _rigidbody2D.velocity.y);

        if (_direction > 0)
        {
            _lookRight = true;
        }
        else if (_direction < 0)
        {
            _lookRight = false;
        }

        Flip();

        _onGround = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, _ground);
    }

    private void Flip()
    {
        if (transform.localScale.x > 0 && !_lookRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _lookRight = !_lookRight;
        }
        else if (transform.localScale.x < 0 && _lookRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _lookRight = !_lookRight;
        }
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