using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	public TextMesh m_livesMesh;
	public TextMesh m_waveMesh;
	public TextMesh m_scoreMesh;
	
	public GameManager m_manager;
	
	void FixedUpdate() {
		m_livesMesh.text = string.Format ("Lives: {0}x", m_manager.m_lives);
		m_waveMesh.text = string.Format ("Wave: {0}", m_manager.m_wave);
		m_scoreMesh.text = string.Format ("Score: {0}", m_manager.m_score);
	}
}
