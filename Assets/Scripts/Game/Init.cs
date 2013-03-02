using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {
	
	void Awake() {
		if(!CustomInput.HaveSettings()) {
			CustomInput.SetDefaults();
		}
	}
}
