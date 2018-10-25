using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour {
    public Material mat;
	// Use this for initialization
	void Start () {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        Mesh groundmesh = gameObject.GetComponent<MeshFilter>().mesh;
        gameObject.GetComponent<MeshRenderer>().material = mat;
        groundmesh.vertices = new Vector3[] {
            new Vector3(-1000, 1.0f),
            new Vector3(-1000, 0.9f),
            new Vector3(1000, 1.0f),
            new Vector3(1000, 0.9f),
        };
        groundmesh.triangles = new int[] { 0, 1, 2, 2, 3, 0 };
        groundmesh.colors = new Color[] {
            new Color(1, 0, 0, 1),
            new Color(1, 0, 0, 1),
            new Color(1, 0, 0, 1),
            new Color(1, 0, 0, 1),
        };
        groundmesh.RecalculateBounds();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
