using UnityEngine;
using System.Collections;

public class FocusItem : MonoBehaviour {
	
	public FocusManager m_manager;
	
	public string m_onHighlight;
	public string m_onUnhighlight;
	public string m_onSelect;
	
	public void Highlight() {
		gameObject.SendMessage(m_onHighlight);
	}
	
	public void Unhighlight() {
		gameObject.SendMessage(m_onUnhighlight);
	}
	
	public void Select() {
		gameObject.SendMessage(m_onSelect);
	}
	
	void OnMouseEnter() {
		m_manager.HighlightItem(this);
	}
	
	void OnMouseDown() {
		m_manager.HighlightItem(this);
		m_manager.SelectCurrent();
	}
}
