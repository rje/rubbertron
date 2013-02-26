using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	

	public enum Gunner {
		Player,
		Enemy
	}
	
	public Gunner m_firedBy;
	public float m_speed;
	public Vector3 m_direction;
	public float m_lifetime;
	
	float m_lived;
	
	void Update() {
		m_lived += Time.deltaTime;
		if(m_lived > m_lifetime) {
			Destroy (gameObject);
		}
		
		UpdatePosition();
	}
	
	void UpdatePosition() {
		transform.localPosition += m_direction * m_speed * Time.deltaTime;
	}
	
	void OnTriggerEnter(Collider c) {
		if(c.CompareTag("wall")) {
			Destroy (gameObject);
		}
		var target = c.GetComponent<Shootable>();
		if(target != null && IsNotShooter(target)) {
			target.WasShot();
			Destroy (gameObject);
		}
	}
	
	bool IsNotShooter(Shootable toCheck) {
		var firedByPlayer = m_firedBy == Gunner.Player;
		var targetIsPlayer = toCheck.CompareTag("Player");
		
		return firedByPlayer != targetIsPlayer;
	}
}
