using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			other.gameObject.GetComponent<Enemy>().HP -= 20;
		}
			gameObject.SetActive(false);
	}
}
