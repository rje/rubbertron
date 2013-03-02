using UnityEngine;
using System.Collections;

public class KeyConfigRow : MonoBehaviour {
	
	public static KeyConfigRow sm_rowActive;
	
	public string m_logicalName;
	public TextMesh m_value;
	
	void Start() {
		m_value.text = CustomInput.GetInputName(m_logicalName);
	}

	void OnMouseDown() {
		if(KeyConfigRow.sm_rowActive != this) {
			if(KeyConfigRow.sm_rowActive != null) {
				KeyConfigRow.sm_rowActive.ResetState();
			}
		}
		sm_rowActive = this;
		Highlight();
	}
	
	void ResetState() {
		Unhighlight();
	}
	
	void Highlight() {
		m_value.renderer.material.color = Color.yellow;
	}
	
	void Unhighlight() {
		m_value.renderer.material.color = Color.white;
	}
	
	void OnGUI() {
		if(KeyConfigRow.sm_rowActive != this) {
			return;
		}
		if(Event.current.type != EventType.KeyDown || !Event.current.isKey) {
			return;
		}
		var key = Event.current.keyCode.ToString();
		m_value.text = key;
		CustomInput.SaveSetting(CustomInput.ResolveLogicalName(m_logicalName), key);
		CustomInput.Save ();
		KeyConfigRow.sm_rowActive = null;
		Unhighlight();
	}
}
