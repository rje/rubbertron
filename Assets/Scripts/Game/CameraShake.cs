using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
	
	public Vector2 m_min;
	public Vector2 m_max;
	public float m_duration;
	
	float m_runtime;
	bool m_shaking = false;
	
	public void StartShake(float time) {
		m_duration = time;
		m_runtime = 0;
		m_shaking = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!m_shaking) {
			return;
		}
		Shake ();
		if(m_runtime > m_duration) {
			FinishShake();
		}
	}
	
	void Shake() {
		m_runtime += Time.deltaTime;
		transform.position = new Vector3(
			Random.Range (m_min.x, m_max.x),
			Random.Range (m_min.y, m_max.y),
			-10.0f
			);
	}
	
	
	void FinishShake() {
		transform.position = new Vector3(0, 0, -10);
		m_shaking = false;
	}
}
