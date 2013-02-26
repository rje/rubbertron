using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	
	public Player m_player;

	void Update() {
		var horiz = Input.GetAxis("Horizontal");
		var vert = Input.GetAxis ("Vertical");
		var direction = new Vector3(horiz, vert, 0);
		direction.Normalize();
		m_player.UpdatePosition(direction);
		
		if(Input.GetButton("Fire1")) {
			m_player.m_shooter.StartFiring();
		}
		else {
			m_player.m_shooter.StopFiring();
		}
	}
}
