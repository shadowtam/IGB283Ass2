//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;
//using System.Text;

//namespace Assets {
//    static class IGB283Transform {
//        public static void Rotate(float angle, GameObject target) {
//            Mesh mesh = target.GetComponent<MeshFilter>().mesh;
//            Vector3[] verticies = mesh.vertices;

//            Matrix3x3 M = Matrix3x3.RotatedMatrix(angle);
//            verticies = M.MultiplyAllPoints(verticies);

//            mesh.vertices = verticies;
//            mesh.RecalculateBounds();
//        }

//        public static void rotateAroundCenter(float angle, GameObject target) {
//            Mesh mesh = target.GetComponent<MeshFilter>().mesh;
//            Vector3[] verticies = mesh.vertices;
//            Vector3 pos = new Vector3();
//            pos.x = mesh.bounds.center.x;
//            pos.y = mesh.bounds.center.y;

//            Matrix3x3 T1 = Matrix3x3.TranslatedMatrix(pos.x, pos.y);
//            Matrix3x3 R1 = Matrix3x3.RotatedMatrix(angle);
//            Matrix3x3 T2 = Matrix3x3.TranslatedMatrix(-pos.x, -pos.y);
//            Matrix3x3 M = T1 * R1 * T2;

//            verticies = M.MultiplyAllPoints(verticies);

//            mesh.vertices = verticies;
//            mesh.RecalculateBounds();

//            //Vector3 offset = new Vector3();


//            Debug.Log(pos);
//            //offset.x = mesh.bounds.size.x / 2;
//            //offset.y = mesh.bounds.size.y / 2;
//        }

//        public static void ArbitraryRotation(float xPos, float yPos, float angle, GameObject target) {
//            Mesh mesh = target.GetComponent<MeshFilter>().mesh;
//            Vector3[] verticies = mesh.vertices;

//            Matrix3x3 T1 = Matrix3x3.TranslatedMatrix(xPos, yPos);
//            Matrix3x3 R1 = Matrix3x3.RotatedMatrix(angle);
//            Matrix3x3 T2 = Matrix3x3.TranslatedMatrix(-xPos, -yPos);
//            Matrix3x3 M = T1 * R1 * T2;
//            //Matrix3x3 M = Matrix3x3.RotatePoint(xPos, yPos, angle);

//            verticies = M.MultiplyAllPoints(verticies);

//            mesh.vertices = verticies;
//            mesh.RecalculateBounds();
//        }

//        public static void Scale(float xScale, float yScale, GameObject target) {
//            Mesh mesh = target.GetComponent<MeshFilter>().mesh;
//            Vector3[] verticies = mesh.vertices;

//            Matrix3x3 S = Matrix3x3.ScaleMatrix(xScale, yScale);
//            verticies = S.MultiplyAllPoints(verticies);

//            mesh.vertices = verticies;
//            mesh.RecalculateBounds();
//        }

//        public static void Translate(float xPos, float yPos, GameObject target) {
//            Mesh mesh = target.GetComponent<MeshFilter>().mesh;
//            Vector3[] verticies = mesh.vertices;

//            Matrix3x3 T = Matrix3x3.TranslatedMatrix(xPos, yPos);
//            verticies = T.MultiplyAllPoints(verticies);

//            mesh.vertices = verticies;
//            mesh.RecalculateBounds();
//        }


//    }
//}
