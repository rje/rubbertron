using UnityEngine;
using System.Collections;

public class Tank : Mob {
	
	void FixedUpdate () {
		UpdatePause();
		if(IsPaused()) {
			return;
		}
		DoCommonUpdate();
	}
	
	void ShotByPlayer() {
		m_currentSpeed /= 4.0f;
	}
}
