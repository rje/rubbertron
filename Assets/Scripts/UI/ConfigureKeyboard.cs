using UnityEngine;
using System.Collections;

public class ConfigureKeyboard : MonoBehaviour {

	void BackPressed() {
		Application.LoadLevel("main_menu");
	}
}
