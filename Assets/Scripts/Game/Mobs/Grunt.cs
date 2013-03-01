using UnityEngine;
using System.Collections;

public class Grunt : Mob {
	// Update is called once per frame
	void FixedUpdate () {
		UpdatePause();
		if(IsPaused()) {
			return;
		}
		DoCommonUpdate();
	}
	
	void ShotByPlayer() {
		var gm = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
		gm.RemoveMob(gameObject);
		AudioSource.PlayClipAtPoint(m_explosionSound, transform.position);
		MakeExplosionParticles();
		Destroy (gameObject);
	}
}
