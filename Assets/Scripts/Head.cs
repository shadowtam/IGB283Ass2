using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {
	
	public GameObject child;
	
	public Vector3 jointLocation;
	public Vector3 jointOffset;
	
	public float angle;
	public float targetAngle;
	public float lastAngle;
	
	public Vector3[] limbVertexLocations;
	public Vector4 colour;
	
	public Mesh mesh;
	public Material material;
	
	private bool LorR = true;

	void Awake () {
		
		DrawLimb();
		
	}
	
	// Use this for initialization
	void Start () {
		
		if (child != null) {
			child.GetComponent<Head>().MoveByOffset(jointOffset);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		lastAngle = angle;
		if (targetAngle < 0 &&  angle < targetAngle) {
			LorR = true;
		} else if (targetAngle > 0 &&  angle > targetAngle){
			LorR = false;
		}
		
		if (LorR) {
			targetAngle = 0.1f;
			angle += 0.01f;
			if (child != null) {
				child.GetComponent<Head>().RotateAroundPoint(jointLocation, angle, lastAngle);
			}
		} else {
			targetAngle = -0.1f;
			angle -= 0.01f;
			if (child != null) {
				child.GetComponent<Head>().RotateAroundPoint(jointLocation, angle, lastAngle);
			}
		}

		mesh.RecalculateBounds();
		
	}
	
	private void DrawLimb() {
		
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
	
		mesh = GetComponent<MeshFilter>().mesh;
		
		GetComponent<MeshRenderer>().material = material;
		
		mesh.Clear();
		
		mesh.vertices = new Vector3[] {
			limbVertexLocations[0],
			limbVertexLocations[1],
			limbVertexLocations[2],
			limbVertexLocations[3]
		};
		
		mesh.colors = new Color[] {
			new Color(colour.x, colour.y, colour.z, colour.w),
			new Color(colour.x, colour.y, colour.z, colour.w),
			new Color(colour.x, colour.y, colour.z, colour.w),
			new Color(colour.x, colour.y, colour.z, colour.w)
		};
		
		mesh.triangles = new int[]{0, 1, 2, 0, 2, 3};
		
	}
	
	public void MoveByOffset (Vector3 offset) {
		
		Matrix3x3 T = Translate3x3(offset);
		Vector3[] verts = mesh.vertices;
		for (int i = 0; i < verts.Length; i++) {
			verts[i] = T.MultiplyPoint(verts[i]);
		}
		mesh.vertices = verts;
		
		jointLocation = T.MultiplyPoint(jointLocation);
		
		if (child != null) {
			child.GetComponent<Head>().MoveByOffset(offset);
		}
				
	}
	
	////////////////////////////////////////////
	
	public void RotateAroundPoint (Vector3 point, float angle, float lastAngle) {
		
		Matrix3x3 T1 = Translate3x3(-point);
		
		Matrix3x3 R1 = Rotate3x3(-lastAngle);
		
		Matrix3x3 T2 = Translate3x3(point);
		
		Matrix3x3 R2 = Rotate3x3(angle);
		
		Matrix3x3 M = T2 * R2 * R1 * T1;
		
		Vector3[] verts = mesh.vertices;
		for (int i = 0; i < verts.Length; i++) {
			verts[i] = M.MultiplyPoint(verts[i]);
		}
		mesh.vertices = verts;
		
		jointLocation = M.MultiplyPoint(jointLocation);
		
		if (child != null){
			child.GetComponent<Head>().RotateAroundPoint(point, angle, lastAngle);
		}
		Debug.Log("rotated");
		Debug.Log(angle);
	}
	
	public static Matrix3x3 Rotate3x3 (float angle) {
		
		Matrix3x3 matrix = new Matrix3x3();
		
		matrix.SetRow(0, new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0.0f));
		matrix.SetRow(1, new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f));
		matrix.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));
		
		return matrix;
		
	}
	
	
	public static Matrix3x3 Translate3x3 (Vector3 offset) {
		
		Matrix3x3 matrix = new Matrix3x3();
		
		matrix.SetRow(0, new Vector3(1.0f, 0.0f, offset.x));
		matrix.SetRow(1, new Vector3(0.0f, 1.0f, offset.y));
		matrix.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));
		
		return matrix;
		
	}
}
