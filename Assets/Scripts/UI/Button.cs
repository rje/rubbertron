using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public GameObject m_toNotify;
	public string m_message;
	
	void OnMouseDown() {
		m_toNotify.SendMessage(m_message);
	}
}
