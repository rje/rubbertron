using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomInput {
	
	public const string MoveHorizAxis = "preferences.input.axis.horiz_axis";
	public const string MoveVertAxis = "preferences.input.axis.vert_axis";
	public const string FireHorizAxis = "preferences.input.axis.fire_horiz_axis";
	public const string FireVertAxis = "preferences.input.axis.fire_vert_axis";
	public const string ControllerInteractionButton = "preferences.input.joystick_button.interaction_button";
	
	public const string LeftButton = "preferences.input.key.left";
	public const string RightButton = "preferences.input.key.right";
	public const string UpButton = "preferences.input.key.up";
	public const string DownButton = "preferences.input.key.down";
	
	public const string HaveSettingsKey = "preferences.input.have_set";
	
	public static void SetDefaults() {
		CustomInput.SaveSetting (MoveHorizAxis, "Axis 1");
		CustomInput.SaveSetting (MoveVertAxis, "Axis 2");
		CustomInput.SaveSetting (FireHorizAxis, "Axis 4");
		CustomInput.SaveSetting (FireVertAxis, "Axis 5");
		CustomInput.SaveSetting (ControllerInteractionButton, "joystick 1 button 0");
		CustomInput.SaveSetting (LeftButton, KeyCode.A.ToString());
		CustomInput.SaveSetting (RightButton, KeyCode.D.ToString());
		CustomInput.SaveSetting (UpButton, KeyCode.W.ToString());
		CustomInput.SaveSetting (DownButton, KeyCode.S.ToString());
		CustomInput.SaveSetting (HaveSettingsKey, "true");
		CustomInput.Save ();
	}
	
	public static bool HaveSettings() {
		return PlayerPrefs.HasKey(HaveSettingsKey);
	}
	
	public static Dictionary<string, string> m_logicalNames = new Dictionary<string, string>() {
		{"Horizontal", MoveHorizAxis},
		{"Vertical", MoveVertAxis},
		{"FireHoriz", FireHorizAxis},
		{"FireVert", FireVertAxis},
		{"Button", ControllerInteractionButton},
		{"Left", LeftButton},
		{"Right", RightButton},
		{"Up", UpButton},
		{"Down", DownButton},
	};
	
	public static void SaveSetting(string setting, string val) {
		PlayerPrefs.SetString(setting, val);
	}
	
	public static string GetSetting(string setting) {
		if(PlayerPrefs.HasKey(setting)) {
			return PlayerPrefs.GetString (setting);
		}
		return null;
	}
	
	public static void Save() {
		PlayerPrefs.Save();
	}
	
	public static float GetAxisRaw(string logicalName) {
		return Input.GetAxisRaw (GetInputName(logicalName));
	}
	
	
	// Emulate an axis until I have repeat mappings
	public static float GetAxisRaw(string negativeKey, string positiveKey) {
		var toReturn = 0.0f;
		var neg = (KeyCode)System.Enum.Parse(typeof(KeyCode), GetInputName (negativeKey));
		var pos = (KeyCode)System.Enum.Parse (typeof(KeyCode), GetInputName (positiveKey));
		toReturn += Input.GetKey(neg) ? -1 : 0;
		toReturn += Input.GetKey(pos) ? 1 : 0;
		return toReturn;
	}
	
	public static bool GetButton(string logicalName) {
		return Input.GetKey (GetInputName (logicalName));
	}
	
	public static bool GetButtonDown(string logicalName) {
		return Input.GetKeyDown(GetInputName (logicalName));
	}
	
	public static string GetInputName(string logicalName) {
		var setting = GetSetting (m_logicalNames[logicalName]);
		if(setting != null) {
			return setting;
		}
		else {
			return logicalName;
		}
	}
	
	public static string ResolveLogicalName(string logicalName) {
		if(m_logicalNames.ContainsKey(logicalName)) {
			return m_logicalNames[logicalName];
		}
		else {
			return logicalName;
		}
	}
}
