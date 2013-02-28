using UnityEngine;
using System.Collections;

public class HSVParticles : MonoBehaviour {
	
	public ParticleSystem m_particles;
	public float m_hueShiftSpeed;
	public float m_alpha;
	public float m_saturation;
	public float m_value;
	
	public float m_rotSpeed;
	
	float m_hue = 0.0f;
	float m_angle = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		m_hue += Time.deltaTime * m_hueShiftSpeed;
		m_hue = Mathf.Repeat(m_hue, 360.0f);
		
		var color = HSVColor.HSVToRGB(m_hue, m_saturation, m_value);
		color.a = m_alpha;
		m_particles.startColor = color;
	}
	
	void FixedUpdate() {
		m_angle += m_rotSpeed * Time.deltaTime;
		m_angle = Mathf.Repeat(m_angle, 360.0f);
		transform.localRotation = Quaternion.Euler(0, 0, m_angle);
	}
}
