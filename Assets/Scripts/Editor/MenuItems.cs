using UnityEngine;
using UnityEditor;
using System.Collections;

public class MenuItems : MonoBehaviour {

	[MenuItem("February/Create Plane Asset...")]
	public static void CreateAndSavePlane() {
		var path = EditorUtility.SaveFilePanelInProject("Save mesh", "Plane", "asset", 
			"Please pick where you would like to save the asset");
		if(string.IsNullOrEmpty(path)) {
			return;
		}
		
		var mesh = ProceduralGeometry.CreateUnitSquare();
		AssetDatabase.CreateAsset(mesh, path);
		AssetDatabase.SaveAssets();
	}
}
