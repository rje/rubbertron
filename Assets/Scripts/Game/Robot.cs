using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour {
	
	public CharacterController m_controller;
	public Shooter m_shooter;
	public float m_speed;
	
	public GameObject m_target;
	
	// Use this for initialization
	void Start () {
		m_shooter.StartFiring();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateTarget();
		UpdateFiring ();
		MoveTowardsTarget();
		LookForHumans();
	}
	
	void UpdateTarget() {
		var colliders = Physics.OverlapSphere(transform.position, 2.0f);
		foreach(var collider in colliders) {
			if(collider.CompareTag("human")) {
				m_target = collider.gameObject;
				return;
			}
		}
		m_target = GameObject.FindGameObjectWithTag("Player");
	}
	
	void UpdateFiring() {
		if(m_target != null && m_target.CompareTag("Player")) {
			m_shooter.SetTarget(m_target.transform.position);
			m_shooter.StartFiring();
		}
		else {
			m_shooter.StopFiring();
		}
	}
	
	void MoveTowardsTarget() {
		if(m_target == null) {
			return;
		}
		var dir = m_target.transform.position - transform.position;
		dir.Normalize();
		m_controller.Move (dir * m_speed * Time.deltaTime);
	}
	
	void ShotByPlayer() {
		Destroy (gameObject);
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
}
