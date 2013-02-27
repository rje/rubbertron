using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	
	public Player m_player;
	void Update() {
		HandleMovementInput();
		HandleShooting();
	}
	
	void HandleMovementInput() {
		var horiz = Input.GetAxisRaw("Horizontal");
		var vert = Input.GetAxisRaw("Vertical");
		var direction = new Vector3(horiz, vert, 0);
		direction.Normalize();
		m_player.UpdatePosition(direction);
	}
	
	void HandleShooting() {
		var isMouseDown = Input.GetButton("Fire1");
		var gamepadAim = GetGamepadAim();
		if(isMouseDown || gamepadAim.sqrMagnitude > 0) {
			if(isMouseDown) {
				m_player.m_shooter.SetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			}
			else {
				m_player.m_shooter.SetTarget(transform.position + gamepadAim);
			}
			m_player.m_shooter.StartFiring();
		}
		else {
			m_player.m_shooter.StopFiring();
		}
	}
	
	Vector3 GetGamepadAim() {
		var horiz = Input.GetAxisRaw("FireHoriz");
		var vert = Input.GetAxisRaw("FireVert");
		var dir = new Vector3(horiz, vert, 0);
		return dir;
	}
}
