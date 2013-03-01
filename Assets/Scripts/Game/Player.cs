using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float m_speed;
	public CharacterController m_controller;
	public Shooter m_shooter;
	
	public GameObject m_spawnEffect;
	public GameObject m_deathParticles;
	
	public AudioSource m_audioSource;
	public AudioClip m_spawnClip;
	public AudioClip m_explosionClip;
	
	bool m_dead = false;
	
	void Start() {
		DoSpawnEffect();
	}
	
	void DoSpawnEffect() {
		var go = (GameObject)GameObject.Instantiate(m_spawnEffect);
		go.transform.position = transform.position;
		m_audioSource.PlayOneShot(m_spawnClip);
		Destroy (go, 2.0f);
	}
	
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
		AudioSource.PlayClipAtPoint(m_explosionClip, transform.position);
		Camera.main.GetComponent<CameraShake>().StartShake(1.0f);
	}
}
