using UnityEngine;
using System.Collections;

public class HSVObjects : MonoBehaviour {
	
	public Renderer[] m_renderers;
	public float m_hueShiftSpeed;
	public float m_alpha;
	public float m_saturation;
	public float m_value;
	
	float m_hue = 0.0f;
	
	// Update is called once per frame
	void Update () {
		m_hue += Time.deltaTime * m_hueShiftSpeed;
		m_hue = Mathf.Repeat(m_hue, 360.0f);
		
		var color = HSVColor.HSVToRGB(m_hue, m_saturation, m_value);
		color.a = m_alpha;
		foreach(var renderer in m_renderers) {
			var material = renderer.sharedMaterial;
			material.color = color;
		}
	}
}
