using UnityEngine;
using System.Collections;

public class Shootable : MonoBehaviour {
	
	public GameObject m_owner;
	public string m_message;
	
	public void WasShot() {
		m_owner.SendMessage(m_message, SendMessageOptions.RequireReceiver);
	}
}
