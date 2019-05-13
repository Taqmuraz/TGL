using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Slicer : MonoBehaviour {

	[SerializeField] Transform sliceTrans;
	[SerializeField] MeshFilter meshFilter;

	void Start ()
	{
		Mesh[] meshes = Slice (meshFilter.mesh, sliceTrans);
		foreach (var m in meshes) {
			GameObject n = new GameObject ("Sliced");
			n.AddComponent<MeshFilter> ().mesh = m;
			n.AddComponent<MeshRenderer> ().materials = meshFilter.GetComponent<MeshRenderer> ().materials;
			Destroy (meshFilter.gameObject);
		}
	}

	public static Mesh[] Slice (Mesh mesh, Transform cutPlane)
	{
		Vector3[] verts = mesh.vertices;
		IEnumerable<int> left;
		IEnumerable<int> right;

		left = mesh.triangles.Where ((int i) => !(cutPlane.InverseTransformDirection(mesh.vertices[i]).x > 0));
		right = mesh.triangles.Where ((int i) => !(cutPlane.InverseTransformDirection(mesh.vertices[i]).x < 0));

		Mesh leftMesh = Mesh.Instantiate<Mesh> (mesh);
		Mesh rightMesh = Mesh.Instantiate<Mesh> (mesh);

		leftMesh.triangles = left.ToArray ();
		rightMesh.triangles = right.ToArray ();

		leftMesh.RecalculateBounds ();
		leftMesh.RecalculateNormals ();
		leftMesh.RecalculateTangents ();

		rightMesh.RecalculateBounds ();
		rightMesh.RecalculateNormals ();
		rightMesh.RecalculateTangents ();

		return new Mesh[2] { leftMesh, rightMesh };
	}
}
