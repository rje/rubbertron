using UnityEngine;
using System.Collections;

public class Tank : Mob {
	
	void Update () {
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
