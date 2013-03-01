using UnityEngine;
using System.Collections;

public class Exploder : Mob {
	
	public GameObject m_explosionEffect;
	public float m_explosionRadius;
	public float m_timeToExplosion;
	public int m_hp;
	
	float m_timeSinceStart;
	
	void Update() {
		m_timeSinceStart += Time.deltaTime;
		if(m_timeSinceStart >= m_timeToExplosion) {
			Explode();
		}
	}
	
	void FixedUpdate() {
		UpdatePause();
		if(IsPaused()) {
			return;
		}
		
		UpdateSpeed();
		UpdateTarget(true);
		MoveTowardsTarget();
	}
	
	void Explode() {
		var gm = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
		var colliders = Physics.OverlapSphere(transform.position, m_explosionRadius);
		foreach(var collider in colliders) {
			var mob = collider.GetComponent<Mob>();
			if(mob != null && mob.m_type != MobType.Tank) {
				mob.MakeExplosionParticles();
				gm.DestroyMob(collider.gameObject);
				Destroy (collider.gameObject);
				continue;
			}
			
			var human = collider.GetComponent<Human>();
			if(human != null) {
				gm.DestroyHuman (human);
				Destroy (collider.gameObject);
				continue;
			}
			
			var player = collider.GetComponent<Player>();
			if(player != null) {
				player.Die ();
				continue;
			}
		}
		var go = (GameObject)GameObject.Instantiate(m_explosionEffect);
		go.transform.position = transform.position;
		AudioSource.PlayClipAtPoint(m_explosionSound, transform.position);
		Destroy (go, 2.5f);
		Destroy (gameObject);
	}
	
	void ShotByPlayer() {
		m_hp--;
		if(m_hp <= 0) {
			var gm = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
			//MakeExplosionParticles();
			gm.RemoveMob(gameObject);
			Explode ();	
		}
	}
}
