using UnityEngine;
using System.Collections;

public class Spawner : Mob {
	
	public MobType m_toSpawn;
	public float m_spawnTime;
	public int m_hp;
	
	float m_sinceLastSpawn = 0;
	
	void Update() {
		if(IsPaused()) {
			return;
		}
		UpdateSpawn();
	}
	
	void FixedUpdate() {
		UpdatePause();
		if(IsPaused()) {
			return;
		}
		DoCommonUpdate();
	}
	
	void UpdateSpawn() {
		m_sinceLastSpawn += Time.deltaTime;
		if(m_sinceLastSpawn >= m_spawnTime) {
			DoSpawn();
			m_sinceLastSpawn -= m_spawnTime;
		}
	}
	
	void DoSpawn() {
		var gm = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
		gm.SpawnMobAtPosition(m_toSpawn, transform.position);
	}
	
	void ShotByPlayer() {
		m_hp--;
		if(m_hp <= 0) {
			var gm = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
			MakeExplosionParticles();
			AudioSource.PlayClipAtPoint(m_explosionSound, transform.position);
			gm.RemoveMob(gameObject);
			Destroy (gameObject);
		}
	}
}
