using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Limb : MonoBehaviour {
	
	public GameObject child;
	// public GameObject control;
	
	public Vector3 jointLocation;
	public Vector3 jointOffset;
	public Vector4 colour;
	
	public int limbNum;
	
	public float angle;
	public float lastAngle;
	public float targetAngle;
	public float initAngle;
	public float[] jumpAngle = new float[2];
	
	public Vector3[] limbVertexLocations;
	public Vector3 walkMovement = new Vector3(0.1f, 0, 0);
	public Vector3 jumpMovement = new Vector3(0, 0.1f, 0);
	
	public Mesh mesh;
	public Material material;
	
	public int dir = 0;
	public bool head = false;
	public bool LorR = false;
	public bool angled = false;
	public bool changeDir = false;
	public bool doJump = false;
	public bool jumpUp = false;
	public bool jumpDown = false;
	public bool jumpRest = true;
	
	void Awake () {
		
		DrawLimb();
		
	}
	
	// Use this for initialization
	void Start () {
		
		if (child != null) {
			child.GetComponent<Limb>().MoveByOffset(jointOffset);
		}
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		lastAngle = angle;
		
		if (!angled) {
			if (dir == 0) {
				if (child != null) {
					child.GetComponent<Limb>().RotateAroundPoint(jointLocation, initAngle, lastAngle);
					Debug.Log("haschild");
					angle = initAngle;
					lastAngle = angle;
				}
			} else {
				if (child != null) {
					child.GetComponent<Limb>().RotateAroundPoint(jointLocation, -initAngle, lastAngle);
					Debug.Log("haschild");
					angle = -initAngle;
					lastAngle = angle;
				}
			}
			angled = true;
		}
		
		
	
		CheckLR();
		
		if (limbNum == 2) {
			HeadNod();
		}
		
		Walk();
		Jump();
		
		if (child != null) {
			child.GetComponent<Limb>().RotateAroundPoint(jointLocation, angle, lastAngle);
		}
		
		mesh.RecalculateBounds();

	}
	
	
	
	public void Jump() {
		if (doJump) {
			if (dir == 0) {
				// if (jumpRest) {
					// if (jumpAngle[0] > 0) {
						// if (angle < jumpAngle[0]){
							// angle += 0.01f;
							// if (child != null) {
								// child.GetComponent<Limb>().Jump();
							// }
						// }  else {
							// jumpDown = true;
							// jumpRest = false;
						// }
					// } else {
						// if (angle > jumpAngle[0]){
							// angle -= 0.01f;
							// if (child != null) {
								// child.GetComponent<Limb>().Jump();
							// }
						// }  else {
							// jumpDown = true;
							// jumpRest = false;
						// }
					// }
				// }
				// if (jumpDown) {
					// if (jumpAngle[1] > 0) {
						// if (angle > jumpAngle[1]){
							// angle -= 0.01f;
							// if (child != null) {
								// child.GetComponent<Limb>().Jump();
							// }
						// }  else {
							// jumpDown = true;
							// jumpRest = false;
						// }
					// } else {
						// if (angle < jumpAngle[1]){
							// angle += 0.01f;
							// if (child != null) {
								// child.GetComponent<Limb>().Jump();
							// }
						// }  else {
							// jumpUp = true;
							// jumpDown = false;
						// }
					// }
					// if (this.transform.position.y < 3) {
						// this.transform.position += jumpMovement;
					// }
				// }
				// if (jumpUp) {
					// if (initAngle > 0) {
						// if (angle < initAngle){
							// angle += 0.01f;
							// if (child != null) {
								// child.GetComponent<Limb>().Jump();
							// }
						// }  else {
							// jumpRest = true;
							// jumpUp = false;
							// doJump = false;
						// }
					// } else {
						// if (angle > initAngle){
							// angle -= 0.01f;
							// if (child != null) {
								// child.GetComponent<Limb>().Jump();
							// }
						// }  else {
							// jumpRest = true;
							// jumpUp = false;
							// doJump = false;
						// }
					// }
					
				// }
				
				if (this.transform.position.y < 1) {
					jumpMovement.x = -0.1f;
					this.transform.position += jumpMovement;
				} else {
					jumpMovement.x = 0;
					doJump = false;
				}
			} else {
				if (this.transform.position.y < 1) {
					jumpMovement.x = 0.1f;
					this.transform.position += jumpMovement;
				} else {
					jumpMovement.x = 0;
					doJump = false;
				}
				
			}
			
		} else {
			if (this.transform.position.y > 0) {
				this.transform.position -= jumpMovement;
			}
		}
		
			
	}
		
		
		
	public void Walk() {
		if (dir == 0) {
			this.transform.position -= walkMovement;
		} else {
			this.transform.position += walkMovement;
		}
	}
	
	
	
	public void CheckLR() {
		if (limbNum == 0) {
			if (this.transform.position.x < -6 && dir != 1) {
				changeDir = true;
			} else if (this.transform.position.x > 6 && dir != 0) {
				changeDir = true;
			}
		}
		if (changeDir && dir == 0) {
			angled = false;
			dir = 1;
			if (child != null) {
				child.GetComponent<Limb>().changeDir = true;
			}
			changeDir = false;
		} else if (changeDir && dir == 1) {
			angled = false;
			dir = 0;
			if (child != null) {
				child.GetComponent<Limb>().changeDir = true;
			}
			changeDir = false;
		}
	}
	
	
	
	private void HeadNod () {
		if (dir == 0) {
			if (targetAngle < 0 &&  angle < targetAngle) {
				LorR = true;
			} else if (targetAngle > 0 &&  angle > targetAngle){
				LorR = false;
			}
			
			if (LorR) {
				targetAngle = 0.5f;
				angle += 0.05f;
				if (child != null) {
					child.GetComponent<Limb>().RotateAroundPoint(jointLocation, angle, lastAngle);
				}
			} else {
				targetAngle = -0.5f;
				angle -= 0.05f;
				if (child != null) {
					child.GetComponent<Limb>().RotateAroundPoint(jointLocation, angle, lastAngle);
				}
			}

		} else {
			if (targetAngle > 0 &&  angle > targetAngle) {
				LorR = true;
			} else if (targetAngle < 0 &&  angle < targetAngle){
				LorR = false;
			}
			
			if (LorR) {
				targetAngle = -0.5f;
				angle -= 0.05f;
				if (child != null) {
					child.GetComponent<Limb>().RotateAroundPoint(jointLocation, angle, lastAngle);
				}
			} else {
				targetAngle = 0.5f;
				angle += 0.05f;
				if (child != null) {
					child.GetComponent<Limb>().RotateAroundPoint(jointLocation, angle, lastAngle);
				}
			}
		}
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
			child.GetComponent<Limb>().MoveByOffset(offset);
		}
				
	}
	
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
			child.GetComponent<Limb>().RotateAroundPoint(point, angle, lastAngle);
		}
	}
	
	/////////////////////////////
	
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
