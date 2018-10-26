using UnityEngine;
using System;

public class limb2 : MonoBehaviour {
    const int numlimbs = 4;
    const float groundlevel = 1.0f;
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
    public Vector3 stoppedMovement = new Vector3(0.0f, 0.0f, 0);
    Vector3 swapVector;

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

    public bool inputAvailable = true;

    float origAngle = 0.0f;

    double startTime1 = -1;
    double startTime2 = -1;
    double startTime3 = -1;
    double startTime4 = -1;


    void Awake() {

        DrawLimb();

    }

    // Use this for initialization
    void Start() {
        if (child != null) {
            child.GetComponent<limb2>().MoveByOffset(jointOffset);
        }
        dir = 1;
    }

    // Update is called once per frame
    void Update() {


        lastAngle = angle;

        if (!angled) {
            if (dir == 0) {
                if (child != null) {
                    child.GetComponent<limb2>().RotateAroundPoint(jointLocation, initAngle, lastAngle);
                    Debug.Log("haschild");
                    angle = initAngle;
                    lastAngle = angle;
                }
            } else {
                if (child != null) {
                    child.GetComponent<limb2>().RotateAroundPoint(jointLocation, -initAngle, lastAngle);
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

        if (inputAvailable) {
            Walk();
        }

        Jump();



        if (Input.GetKeyDown("j") && inputAvailable) {
            dir = 0;
        } else if (Input.GetKeyDown("l") && inputAvailable) {
            dir = 1;
        }

        if (startTime3 > Time.time) {
            if (limbNum != 2) {
                float sign = 1.0f;
                if (dir == 1) {
                    sign = -1.0f;
                }
                angle += sign * Time.deltaTime;
            }


        } else if (startTime4 > Time.time) {
            float sign = 1.0f;
            if (dir == 1) {
                sign = -1.0f;
            }
            if (angle < origAngle) {
                angle -= sign * Time.deltaTime;
            }
        } else if (Input.GetKey("m") && inputAvailable) {
            inputAvailable = false;
            origAngle = angle;
            startTime3 = Time.time + 2.5f;
            startTime4 = Time.time + 5.0f;
        } else {
            inputAvailable = true;
        }


        if (child != null) {
            child.GetComponent<limb2>().RotateAroundPoint(jointLocation, angle, lastAngle);
        }

        mesh.RecalculateBounds();

    }

    public void Jump() {
        if (startTime1 > Time.time) {
            transform.position += new Vector3(0, 0.6f * Time.deltaTime);
        } else if (startTime2 > Time.time) {
            transform.position += new Vector3(0, -0.6f * Time.deltaTime);
        } else {
            if (Input.GetKeyDown("i") && inputAvailable) {
                startTime1 = Time.time + 1;
                startTime2 = Time.time + 2;
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
                child.GetComponent<limb2>().changeDir = true;
            }
            changeDir = false;
        } else if (changeDir && dir == 1) {
            angled = false;
            dir = 0;
            if (child != null) {
                child.GetComponent<limb2>().changeDir = true;
            }
            changeDir = false;
        }
    }



    private void HeadNod() {
        if (dir == 0) {
            if (targetAngle < 0 && angle < targetAngle) {
                LorR = true;
            } else if (targetAngle > 0 && angle > targetAngle) {
                LorR = false;
            }

            if (LorR) {
                targetAngle = 0.5f;
                angle += 0.1f;
                if (child != null) {
                    child.GetComponent<limb2>().RotateAroundPoint(jointLocation, angle, lastAngle);
                }
            } else {
                targetAngle = -0.5f;
                angle -= 0.1f;
                if (child != null) {
                    child.GetComponent<limb2>().RotateAroundPoint(jointLocation, angle, lastAngle);
                }
            }

        } else {
            if (targetAngle > 0 && angle > targetAngle) {
                LorR = true;
            } else if (targetAngle < 0 && angle < targetAngle) {
                LorR = false;
            }

            if (LorR) {
                targetAngle = -0.5f;
                angle -= 0.1f;
                if (child != null) {
                    child.GetComponent<limb2>().RotateAroundPoint(jointLocation, angle, lastAngle);
                }
            } else {
                targetAngle = 0.5f;
                angle += 0.1f;
                if (child != null) {
                    child.GetComponent<limb2>().RotateAroundPoint(jointLocation, angle, lastAngle);
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

        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        mesh.RecalculateBounds();

    }

    public void MoveByOffset(Vector3 offset) {

        Matrix3x3 T = Translate3x3(offset);
        Vector3[] verts = mesh.vertices;
        for (int i = 0; i < verts.Length; i++) {
            verts[i] = T.MultiplyPoint(verts[i]);
        }
        mesh.vertices = verts;

        jointLocation = T.MultiplyPoint(jointLocation);

        if (child != null) {
            child.GetComponent<limb2>().MoveByOffset(offset);
        }

    }

    public void RotateAroundPoint(Vector3 point, float angle, float lastAngle) {

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

        if (child != null) {
            child.GetComponent<limb2>().RotateAroundPoint(point, angle, lastAngle);
        }
    }

    /////////////////////////////

    public static Matrix3x3 Rotate3x3(float angle) {

        Matrix3x3 matrix = new Matrix3x3();

        matrix.SetRow(0, new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0.0f));
        matrix.SetRow(1, new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f));
        matrix.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        return matrix;

    }


    public static Matrix3x3 Translate3x3(Vector3 offset) {

        Matrix3x3 matrix = new Matrix3x3();

        matrix.SetRow(0, new Vector3(1.0f, 0.0f, offset.x));
        matrix.SetRow(1, new Vector3(0.0f, 1.0f, offset.y));
        matrix.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        return matrix;

    }
}
