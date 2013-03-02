using UnityEngine;
using System.Collections;

public class FocusManager : MonoBehaviour {

	public FocusItem[] m_items;
	
	public int m_default;
	public int m_current;
	public float m_moveThreshold;
	
	float m_sinceLastMove;
	
	void Start() {
		m_current = m_default;
		foreach(var item in m_items) {
			item.Unhighlight();
		}
		m_items[m_current].Highlight();
	}
	
	void FixedUpdate() {
		if(m_sinceLastMove < m_moveThreshold) {
			m_sinceLastMove += Time.fixedDeltaTime;
			return;
		}
		var vert = CustomInput.GetAxisRaw("Vertical");
		if(vert > 0.8f) {
			HighlightNext();
			m_sinceLastMove = 0;
		}
		else if(vert < -0.8f) {
			HighlightPrev();
			m_sinceLastMove = 0;
		}
		
		if(CustomInput.GetButton("Button")) {
			SelectCurrent();
		}
	}
	
	public void HighlightItem(FocusItem item) {
		m_items[m_current].Unhighlight();
		m_current = System.Array.IndexOf(m_items, item);
		m_items[m_current].Highlight();
	}
	
	public void HighlightNext() {
		m_items[m_current].Unhighlight();
		m_current++;
		if(m_current >= m_items.Length) {
			m_current -= m_items.Length;
		}
		m_items[m_current].Highlight();
	}
	
	public void HighlightPrev() {
		m_items[m_current].Unhighlight();
		m_current--;
		if(m_current < 0) {
			m_current += m_items.Length;
		}
		m_items[m_current].Highlight();
	}
	
	public void SelectCurrent() {
		m_items[m_current].Select();
	}
	
	
}
