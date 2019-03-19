using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

//	private void OnCollisionEnter2D(Collision2D other)
//	{
//		if (other.gameObject.CompareTag("Enemy"))
//		{
//			other.gameObject.GetComponent<Enemy>().GetDamage(20);
//		}
//	}

//	public Enemy Enemy;

	public void SetDamage(int damage, Enemy enemy)
	{
		enemy.GetDamage(damage);
	}
}
