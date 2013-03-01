using UnityEngine;
using System.Collections;

public class Robot : Mob {
	
	public Shooter m_shooter;
	
	// Update is called once per frame
	void FixedUpdate () {
		UpdatePause();
		if(IsPaused()) {
			return;
		}
		DoCommonUpdate();
	}
	
	void Update() {
		UpdateFiring ();
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
	
	void ShotByPlayer() {
		var gm = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
		gm.RemoveMob(gameObject);
		MakeExplosionParticles();
		AudioSource.PlayClipAtPoint(m_explosionSound, transform.position);
		Destroy (gameObject);
	}
}
