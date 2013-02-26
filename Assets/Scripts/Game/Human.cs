using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour {

	public float m_speed;
	Vector3 m_direction;
	
	void Update() {
		UpdateDirection();
		UpdatePosition();
	}
	
	void UpdateDirection() {
		// FIXME: Random now, update later to suck less
		if(Random.Range (0.0f, 1.0f) < 0.01f) {
			m_direction = new Vector3(Random.Range (-1.0f, 1.0f), Random.Range (-1.0f, 1.0f), 0.0f);
			m_direction.Normalize();
		}
	}
	
	void UpdatePosition() {
		transform.localPosition += m_direction * m_speed * Time.deltaTime;
	}
	
	void OnCollisionEnter(Collision collision) {
		var collider = collision.collider;
		if(collider.CompareTag("wall")) {
			var ray = new Ray(transform.position, m_direction);
			RaycastHit rh;
			collider.Raycast(ray, out rh, float.MaxValue);
			m_direction = -2 * Vector3.Dot(m_direction, rh.normal) * rh.normal - m_direction;
			m_direction.Normalize();
		}
	}
}
