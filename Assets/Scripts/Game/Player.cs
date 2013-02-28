using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float m_speed;
	public CharacterController m_controller;
	public Shooter m_shooter;
	
	public GameObject m_deathParticles;
	
	bool m_dead = false;
	
	public void UpdatePosition(Vector3 moveVector) {
		m_controller.Move(moveVector * m_speed * Time.deltaTime);
	}
	
	void Update() {
		LookForHumans();
	}
	
	void LookForHumans() {
		var colliders = Physics.OverlapSphere(transform.position, m_controller.radius + 0.05f);
		foreach(var collider in colliders) {
			if(collider.CompareTag("human")) {
				CollectHuman(collider.GetComponent<Human>());
			}
		}
	}
	
	void CollectHuman(Human toCollect) {
		var gm = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
		gm.CollectHuman(toCollect);
		Destroy (toCollect.gameObject);
	}
	
	void Shot() {
		Die();
	}
	
	public void Die() {
		if(m_dead) {
			return;
		}
		var explosion = (GameObject)GameObject.Instantiate(m_deathParticles);
		explosion.transform.position = transform.position;
		Destroy (explosion, 2.5f);
		m_dead = true;
		Destroy (gameObject);
		var gm = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
		gm.HandlePlayerDeath();
	}
}
