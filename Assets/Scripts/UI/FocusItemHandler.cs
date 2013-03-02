using UnityEngine;
using System.Collections;

public class FocusItemHandler : MonoBehaviour {
	
	public Color m_default;
	public Color m_highlight;
	public GameObject m_toNotify;
	public string m_message;

	void OnHighlight() {
		renderer.material.color = m_highlight;
	}
	
	void OnUnhighlight() {
		renderer.material.color = m_default;
	}
	
	void OnSelect() {
		m_toNotify.SendMessage(m_message);
	}
}
