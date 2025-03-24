using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quests
{
    public static class DelaunayTriangulation
    {
        public static Triangle[] Triangulate(Vector2[] points) {
            Triangle superTriangle = CalcSuperTriangle(points);
            List<Triangle> triangles = new List<Triangle> { superTriangle };

            foreach (Vector2 point in points) {
                triangles = AddPoint(point, triangles);
            }

            triangles = triangles.Where(
                triangle => !(triangle.A == superTriangle.A || triangle.A == superTriangle.B ||
                              triangle.A == superTriangle.C ||
                              triangle.B == superTriangle.A || triangle.B == superTriangle.B ||
                              triangle.B == superTriangle.C ||
                              triangle.C == superTriangle.A || triangle.C == superTriangle.B ||
                              triangle.C == superTriangle.C)).ToList();

            return triangles.ToArray();
        }
        
        private static List<Triangle> AddPoint(Vector2 point, List<Triangle> triangles) {
            List<Edge> edges = new List<Edge>();

            List<Triangle> tris = new List<Triangle>();
            foreach (Triangle triangle in triangles) {
                if (triangle.CircumscribedCircle.IsInside(point)) {
                    edges.Add(new Edge(triangle.A, triangle.B));
                    edges.Add(new Edge(triangle.B, triangle.C));
                    edges.Add(new Edge(triangle.C, triangle.A));
                    continue;
                }
                tris.Add(triangle);
            }

            edges = UniqueEdges(edges);

            foreach (Edge edge in edges) {
                tris.Add(new Triangle(edge.A, edge.B, point));
            }

            return tris;
        }
        private static List<Edge> UniqueEdges(List<Edge> edges) {
            List<Edge> uniqueEdges = new List<Edge>();
            for (int i = 0; i < edges.Count; i++) {
                bool isUnique = true;
                for (int j = 0; j < edges.Count; j++) {
                    if (i != j && edges[i] == edges[j]) {
                        isUnique = false;
                        break;
                    }
                }
                if(isUnique) uniqueEdges.Add(edges[i]);
            }

            return uniqueEdges;
        }


        private static Triangle CalcSuperTriangle(Vector2[] points) {
            float minx = points[0].x;
            float maxx = points[0].x;
            float miny = points[0].y;
            float maxy = points[0].y;

            foreach (Vector2 point in points) {
                minx = Mathf.Min(minx, point.x);
                miny = Mathf.Min(miny, point.y);
                maxx = Mathf.Max(maxx, point.x);
                maxy = Mathf.Max(maxy, point.y);
            }

            float dx = (maxx - minx) * 10f;
            float dy = (maxy - miny) * 10f;

            Vector2 a = new Vector2(minx - dx, miny - dy * 3);
            Vector2 b = new Vector2(minx - dx, maxy + dy);
            Vector2 c = new Vector2(maxx + dx * 3, maxy + dy);

            return new Triangle(a, b, c);
        }
    }

    public class Edge
    {
        public Vector2 A;
        public Vector2 B;
        
        public Edge(Vector2 a, Vector2 b) {
            A = a;
            B = b;
        }

        public static bool operator ==(Edge left, Edge right) {
            return (left.A == right.A && left.B == right.B) || (left.A == right.B && left.B == right.A);
        }
        public static bool operator !=(Edge left, Edge right) {
            return !(left == right);
        }

        public bool Equals(Edge other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return A.Equals(other.A) && B.Equals(other.B);
        }
        public override bool Equals(object obj) {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Edge)obj);
        }
        public override int GetHashCode() {
            return A.GetHashCode() ^ B.GetHashCode();
        }
    }

    public class Triangle
    {
        public Vector2 A;
        public Vector2 B;
        public Vector2 C;

        public Circle CircumscribedCircle { get; private set; }

        public Triangle(Vector2 a, Vector2 b, Vector2 c) {
            A = a;
            B = b;
            C = c;

            CircumscribedCircle = CalcCircumscribedCircle();
        }
        
        private Circle CalcCircumscribedCircle() {
            float delta = new Matrix3X3(A.x, A.y, 1, B.x, B.y, 1, C.x, C.y, 1).Determinant;
            float x = new Matrix3X3(A.sqrMagnitude, A.y, 1, B.sqrMagnitude, B.y, 1, C.sqrMagnitude, C.y, 1).Determinant / (2 * delta);
            float y = - new Matrix3X3(A.sqrMagnitude, A.x, 1, B.sqrMagnitude, B.x, 1, C.sqrMagnitude, C.x, 1).Determinant / (2 * delta);
            Vector2 center = new Vector2(x, y);
            float am = (A-B).magnitude;
            float bm = (B-C).magnitude;
            float cm = (C-A).magnitude;
            float p = (am + bm + cm) / 2;
            float r = (am * bm * cm) / (4 * Mathf.Sqrt(p * (p - am) * (p - bm) * (p - cm)));
            return new Circle(center, r);
        }
    }

    public class Circle
    {
        public Vector2 c;
        public float r;
        
        public Circle(Vector2 c, float r) {
            this.c = c;
            this.r = r;
        }

        public float Circumference => 2 * Mathf.PI * r;
        public float Area => Mathf.PI * r * r;

        public bool IsInside(Vector2 vertex) {
            return (vertex - c).sqrMagnitude <= r * r;
        }
    }

    public class Matrix3X3
    {
        public float a;
        public float b;
        public float c;
        public float d;
        public float e;
        public float f;
        public float g;
        public float h;
        public float i;
        
        public Matrix3X3(float a, float b, float c, float d, float e, float f, float g, float h, float i) {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.f = f;
            this.g = g;
            this.h = h;
            this.i = i;
        }
        
        public Matrix3X3(Vector3 col1, Vector3 col2, Vector3 col3) {
            a = col1.x;
            b = col2.x;
            c = col3.x;
            d = col1.y;
            e = col2.y;
            f = col3.y;
            g = col1.z;
            h = col2.z;
            i = col3.z;
        }

        public float Determinant => a * e * i + g * b * f + d * h * c - a * h * f - d * b * i - g * e * c;

        public Vector3 GetColumn(int index) {
            return index switch
            {
                0 => new Vector3(a, d, g),
                1 => new Vector3(b, e, h),
                2 => new Vector3(c, f, i),
                _ => throw new IndexOutOfRangeException("Invalid Column Index !")
            };
        }

        public static Matrix3X3 operator *(Matrix3X3 left, Matrix3X3 right) {
            Matrix3X3 result = new Matrix3X3(0,0,0,0,0,0,0,0,0)
            {
                a = left.a * right.a + left.b * right.d + left.c * right.g,
                b = left.a * right.b + left.b * right.e + left.c * right.h,
                c = left.a * right.c + left.b * right.f + left.c * right.i,
                
                d = left.d * right.a + left.e * right.d + left.f * right.g,
                e = left.d * right.b + left.e * right.e + left.f * right.h,
                f = left.d * right.c + left.e * right.f + left.f * right.i,
                
                g = left.g * right.a + left.h * right.d + left.i * right.g,
                h = left.g * right.b + left.h * right.e + left.i * right.h,
                i = left.g * right.c + left.h * right.f + left.i * right.i
            };
            return result;
        }

        public static Matrix3X3 operator *(Matrix3X3 m, float k) {
            return new Matrix3X3(m.a * k, m.b * k, m.c * k, m.d * k, m.e * k, m.f * k, m.g * k, m.h * k, m.i * k);
        }

        public static Matrix3X3 operator *(float k, Matrix3X3 m) {
            return m * k;
        }

        public static bool operator ==(Matrix3X3 left, Matrix3X3 right) {
            return left.GetColumn(0) == right.GetColumn(0)
                   && left.GetColumn(1) == right.GetColumn(1)
                   && left.GetColumn(2) == right.GetColumn(2);
        }
        public static bool operator !=(Matrix3X3 left, Matrix3X3 right) {
            return !(left == right);
        }

        public bool Equals(Matrix3X3 other) {
            return other.GetColumn(0).Equals(GetColumn(0))
                && other.GetColumn(1).Equals(GetColumn(1))
                && other.GetColumn(2).Equals(GetColumn(2));
        }
        public override bool Equals(object obj) {
            return Equals(obj as Matrix3X3);
        }
        public override int GetHashCode() {
            return GetColumn(0).GetHashCode() ^ GetColumn(1).GetHashCode() ^ GetColumn(2).GetHashCode();
        }
    }
}