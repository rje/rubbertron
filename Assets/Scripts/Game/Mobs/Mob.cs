using UnityEngine;
using System.Collections;

public enum MobType {
	None = -1,
	Grunt,
	Robot,
	Tank,
	Spawner,
	Exploder
};

public class Mob : MonoBehaviour {

	public MobType m_type;
	public CharacterController m_controller;
	public float m_speed;
	public GameObject m_target;
	public float m_searchRange;
	public float m_currentSpeed;
	public float m_accel;
	
	public GameObject m_spawnEffect;
	public GameObject m_deathExplosionPrefab;
	
	public AudioSource m_audioSource;
	public AudioClip m_spawnSound;
	public AudioClip m_explosionSound;
	
	float m_pauseAmount;
	bool m_isPaused;
	
	void Start() {
		DoSpawnEffect();
	}
	
	protected void DoSpawnEffect() {
		var go = (GameObject)GameObject.Instantiate(m_spawnEffect);
		go.transform.position = transform.position;
		Destroy (go, 2.0f);
	}
	
	public void PlaySpawnSound() {
		m_audioSource.PlayOneShot(m_spawnSound);
	}
	
	public void PauseFor(float amount) {
		m_pauseAmount = amount;
		m_isPaused = true;
	}
	
	protected void UpdatePause() {
		if(!m_isPaused) {
			return;
		}
		m_pauseAmount -= Time.deltaTime;
		if(m_pauseAmount <= 0) {
			m_isPaused = false;
		}
	}
	
	protected bool IsPaused() {
		return m_isPaused;
	}
	
	protected void DoCommonUpdate() {
		UpdateSpeed();
		UpdateTarget();
		MoveTowardsTarget();
		LookForHumans();
		LookForPlayer();
	}
	
	protected void LookForHumans() {
		var colliders = Physics.OverlapSphere(transform.position, m_controller.radius + 0.05f);
		foreach(var collider in colliders) {
			if(collider.CompareTag("human")) {
				CollectHuman(collider.GetComponent<Human>());
			}
		}
	}
	
	protected void LookForPlayer() {
		var colliders = Physics.OverlapSphere(transform.position, m_controller.radius + 0.05f);
		foreach(var collider in colliders) {
			if(collider.CompareTag("Player")) {
				var player = collider.GetComponent<Player>();
				player.SendMessage("Die");
			}
		}
	}
	
	protected void UpdateTarget(bool ignoreHumans = false) {
		if(!ignoreHumans) {
			var colliders = Physics.OverlapSphere(transform.position, m_searchRange);
			foreach(var collider in colliders) {
				if(collider.CompareTag("human")) {
					m_target = collider.gameObject;
					return;
				}
			}
		}
		m_target = GameObject.FindGameObjectWithTag("Player");
	}
	
	protected void UpdateSpeed() {
		m_currentSpeed += m_accel * Time.deltaTime;
		m_currentSpeed = Mathf.Clamp (m_currentSpeed, 0.0f, m_speed);
	}
	
	protected void MoveTowardsTarget() {
		if(m_target == null) {
			return;
		}
		var dir = m_target.transform.position - transform.position;
		dir.Normalize();
		m_controller.Move (dir * m_currentSpeed * Time.deltaTime);
	}
	
	protected void CollectHuman(Human toCollect) {
		var gm = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
		gm.DestroyHuman(toCollect);
		Destroy (toCollect.gameObject);
	}
	
	public void MakeExplosionParticles(float delay = 1.5f) {
		var go = (GameObject)GameObject.Instantiate(m_deathExplosionPrefab);
		go.transform.position = transform.position;
		Destroy(go, delay);
	}
}
