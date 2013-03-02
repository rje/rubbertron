using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class InputMeasure {
	public string m_name;
	public float m_min;
	public float m_max;
	
	public InputMeasure(string name) {
		m_name = name;
		m_min = 0;
		m_max = 0;
	}
	
	public void AddMeasurement(float val) {
		m_min = Mathf.Min (m_min, val);
		m_max = Mathf.Max (m_max, val);
	}
	
	public float GetTotal() {
		return m_max - m_min;
	}
}

public class ConfigureGamepad : MonoBehaviour {
	
	enum ConfigureState {
		Waiting,
		Ready,
		MoveHorizAxis,
		MoveVertAxis,
		FireHorizAxis,
		FireVertAxis,
		Button,
		Finished
	}
	
	public TextMesh m_found;
	public TextMesh m_instructions;
	
	public string m_instructionsWaiting;
	public string m_instructionsPressAnyButton;
	public string m_instructionsMoveHorizAxis;
	public string m_instructionsMoveVertAxis;
	public string m_instructionsFireHorizAxis;
	public string m_instructionsFireVertAxis;
	public string m_instructionsButton;
	public string m_instructionsComplete;
	
	public float m_threshold;
	
	ConfigureState m_state;
	string m_controllerName;
	string m_button;
	string m_horizontalAxis;
	string m_verticalAxis;
	string m_horizFireAxis;
	string m_vertFireAxis;
	
	Dictionary<string, InputMeasure> m_measurements;
	
	void Start() {
		m_measurements = new Dictionary<string, InputMeasure>();
		SetState(ConfigureState.Waiting);
		FindController();
	}
	
	void FindController() {
		var names = Input.GetJoystickNames();
		if(names.Length == 0) {
			return;
		}
		else {
			m_controllerName = names[0];
			SetState(ConfigureState.Ready);
		}
	}
	
	string LookForButton() {
		var format = "joystick {0} button {1}";
		for(var joy = 1; joy < 2; joy++) {
			for(var key = 0; key < 20; key++) {
				var identifier = string.Format (format, joy, key);
				if(Input.GetKey(identifier)) {
					return identifier;
				}
			}
		}
		return null;
	}
	
	void Update() {
		InputMeasure axis = null;
		string button = null;
		switch(m_state) {
		case ConfigureState.Waiting:
			FindController ();
			break;
		case ConfigureState.Ready:
			button = LookForButton();
			if(button != null) {
				SetState (ConfigureState.MoveHorizAxis);
			}
			break;
		case ConfigureState.MoveHorizAxis:
			LookForAxis();
			axis = GetResolvedAxis();
			if(axis != null) {
				m_horizontalAxis = axis.m_name;
				SetState (ConfigureState.MoveVertAxis);
			}
			break;
		case ConfigureState.MoveVertAxis:
			LookForAxis();
			axis = GetResolvedAxis();
			if(axis != null) {
				m_verticalAxis = axis.m_name;
				SetState (ConfigureState.FireHorizAxis);
			}
			break;
		case ConfigureState.FireHorizAxis:
			LookForAxis();
			axis = GetResolvedAxis();
			if(axis != null) {
				m_horizFireAxis = axis.m_name;
				SetState (ConfigureState.FireVertAxis);
			}
			break;
		case ConfigureState.FireVertAxis:
			LookForAxis();
			axis = GetResolvedAxis();
			if(axis != null) {
				m_vertFireAxis = axis.m_name;
				SetState (ConfigureState.Button);
			}
			break;
		case ConfigureState.Button:
			button = LookForButton();
			if(button != null) {
				m_button = button;
				SaveResults();
				SetState (ConfigureState.Finished);
			}
			break;
		case ConfigureState.Finished:
			if(CustomInput.GetButtonDown("Button")) {
				ReturnToMainMenu();
			}
			break;
		}
	}
	
	void SaveResults() {
		CustomInput.SaveSetting(CustomInput.MoveHorizAxis, m_horizontalAxis);
		CustomInput.SaveSetting(CustomInput.MoveVertAxis, m_verticalAxis);
		CustomInput.SaveSetting(CustomInput.FireHorizAxis, m_horizFireAxis);
		CustomInput.SaveSetting(CustomInput.FireVertAxis, m_vertFireAxis);
		CustomInput.SaveSetting(CustomInput.ControllerInteractionButton, m_button);
		CustomInput.Save();
	}
	
	void LookForAxis() {
		var format = "Axis {0}";
		for(var i = 1; i <= 10; i++) {
			var name = string.Format (format, i);
			var val = Input.GetAxisRaw (name);
			if(!m_measurements.ContainsKey(name)) {
				m_measurements[name] = new InputMeasure(name);
			}
			m_measurements[name].AddMeasurement(val);
		}
	}
	
	InputMeasure GetResolvedAxis() {
		var format = "Axis {0}";
		for(var i = 1; i <= 10; i++) {
			var name = string.Format (format, i);
			if(m_measurements.ContainsKey(name) && m_measurements[name].GetTotal() > m_threshold) {
				return m_measurements[name];
			}
		}
		return null;
	}
	
	void SetState(ConfigureState newState) {
		m_state = newState;
		UpdateLabels();
		switch(m_state) {
		case ConfigureState.MoveHorizAxis:
		case ConfigureState.MoveVertAxis:
		case ConfigureState.FireHorizAxis:
		case ConfigureState.FireVertAxis:
			ResetAxisInput();
			break;
		}
	}
	
	void ResetAxisInput() {
		m_measurements.Clear();
		Input.ResetInputAxes();
	}
	
	void UpdateLabels() {
		switch(m_state) {
		case ConfigureState.Waiting:
			m_found.text = "Looking for controller...";
			m_instructions.text = "";
			break;
		case ConfigureState.Ready:
			m_found.text = string.Format ("Found controller:\n{0}", m_controllerName);
			m_instructions.text = m_instructionsPressAnyButton;
			break;
		case ConfigureState.MoveHorizAxis:
			m_instructions.text = m_instructionsMoveHorizAxis;
			break;
		case ConfigureState.MoveVertAxis:
			m_instructions.text = m_instructionsMoveVertAxis;
			break;
		case ConfigureState.FireHorizAxis:
			m_instructions.text = m_instructionsFireHorizAxis;
			break;
		case ConfigureState.FireVertAxis:
			m_instructions.text = m_instructionsFireVertAxis;
			break;
		case ConfigureState.Button:
			m_instructions.text = m_instructionsButton;
			break;
		case ConfigureState.Finished:
			m_instructions.text = m_instructionsComplete;
			break;
		}
	}
	
	void ReturnToMainMenu() {
		Application.LoadLevel("main_menu");
	}
}
