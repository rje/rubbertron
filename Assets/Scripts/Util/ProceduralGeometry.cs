using UnityEngine;
using System.Collections;

public class ProceduralGeometry : MonoBehaviour {

	public static Mesh CreateUnitSquare() {
		var mesh = new Mesh();
		var verts = new Vector3[] {
			new Vector3(-0.5f,  0.5f, 0.0f),
			new Vector3( 0.5f,  0.5f, 0.0f),
			new Vector3(-0.5f, -0.5f, 0.0f),
			new Vector3( 0.5f, -0.5f, 0.0f)
		};
		var uvs = new Vector2[] {
			new Vector2(0.0f, 1.0f),
			new Vector2(1.0f, 1.0f),
			new Vector2(0.0f, 0.0f),
			new Vector2(1.0f, 0.0f)
		};
		var normals = new Vector3[] {
			new Vector3(0, 1, 0),
			new Vector3(0, 1, 0),
			new Vector3(0, 1, 0),
			new Vector3(0, 1, 0)
		};
		var idx = new int[] { 0, 1, 2, 3, 2, 1 };
		mesh.vertices = verts;
		mesh.uv = uvs;
		mesh.normals = normals;
		mesh.triangles = idx;
		mesh.RecalculateBounds();
		mesh.Optimize();
		
		return mesh;
	}
}
