using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour {

	public float m_speed;
	Vector3 m_direction;
	
	public GameObject m_spawnEffect;
	public GameObject m_destroyEffect;
	public GameObject m_savedEffect;
	
	public AudioClip m_collectedSound;
	public AudioClip m_destroyedSound;
	
	void Start() {
		DoSpawnEffect();
	}
	
	void DoSpawnEffect() {
		var go = (GameObject)GameObject.Instantiate(m_spawnEffect);
		go.transform.position = transform.position;
		Destroy (go, 2.0f);
	}
	
	void FixedUpdate() {
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
		var local = transform.localPosition;
		// So, occasionally these guys are escaping the board - don't have time to dig in so just warp back for now.
		if(Mathf.Abs (local.x) >= 12.9f || Mathf.Abs (local.y) >= 9.9f) {
			WarpBackToBoard();
		}
	}
	
	void WarpBackToBoard() {
		transform.localPosition = new Vector3(
			Random.Range(-10.0f, 10.0f),
			Random.Range (-8.0f, 8.0f),
			0);
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
	
	public void PlayDestroyEffect() {
		var go = (GameObject)GameObject.Instantiate(m_destroyEffect);
		go.transform.position = transform.position;
		AudioSource.PlayClipAtPoint(m_destroyedSound, transform.position);
		Destroy (go, 2.5f);
	}
	
	public void PlaySavedEffect() {
		var go = (GameObject)GameObject.Instantiate(m_savedEffect);
		go.transform.position = transform.position;
		AudioSource.PlayClipAtPoint(m_collectedSound, transform.position);
		Destroy (go, 2.5f);
	}
}
