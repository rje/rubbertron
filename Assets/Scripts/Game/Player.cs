using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float m_speed;
	public CharacterController m_controller;
	public Shooter m_shooter;
	
	public void UpdatePosition(Vector3 moveVector) {
		m_controller.Move(moveVector * m_speed * Time.deltaTime);
	}
	
	void Update() {
		m_shooter.SetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
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
		Destroy (toCollect.gameObject);
	}
	
	void Shot() {
		Die();
	}
	
	void Die() {
		Destroy (gameObject);
		var gm = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
		gm.HandlePlayerDeath();
	}
}
