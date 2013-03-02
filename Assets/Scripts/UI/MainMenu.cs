using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void PlayGame() {
		Application.LoadLevel("default");
	}
	
	void ConfigureController() {
		Application.LoadLevel("configure_controller");
	}
	
	void ConfigureKeyboard() {
		Application.LoadLevel("configure_keyboard");
	}
}
