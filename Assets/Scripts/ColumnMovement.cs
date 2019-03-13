using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ColumnMovement : MonoBehaviour
{

	public bool move = false;
	[SerializeField] private float speed = 3f;
	[SerializeField] private float direction = -1f;
	private Rigidbody2D _rigidbody2D;

	private void Start()
	{
		_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		if (move)
		{
			_rigidbody2D.velocity = new Vector2(0, speed * direction);
		}
		else
		{
			_rigidbody2D.velocity = Vector2.zero;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		
		if (move && other.CompareTag("Stop"))
		{
			direction = -direction;
			move = !move;
		}
	}
}
