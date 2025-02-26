using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Shapes;
using UnityEngine;

namespace Quests
{
    [ExecuteAlways]
    public class WorldMap : ImmediateModeShapeDrawer
    {
        [SerializeField] private int _pointNb;
        [SerializeField] private float _boundingCubeSize;
        [SerializeField] private float _townRadii;
        [SerializeField, Range(0f, 1f)] private float _roadDensity;
        private Town[] _towns;
        private Edge[] _roads;

        public override void DrawShapes(Camera cam) {
            if (_roads == null ||_towns == null) {
                return;
            }
            
            using (Draw.Command(cam)) {
                Draw.LineGeometry = LineGeometry.Flat2D;
                Draw.ThicknessSpace = ThicknessSpace.Pixels;
                Draw.Thickness = 2;
                Draw.Matrix = transform.localToWorldMatrix;
                
                foreach (Edge edge in _roads) {
                    Draw.Line(new Vector3(edge.A.x, edge.A.y, 0), new Vector3(edge.B.x, edge.B.y, 0));
                }
                
                foreach (Town town in _towns) {
                    town.DrawTown(_townRadii);
                }
            }
        }

        [Button]
        public void GenerateMap() {
            _towns = new Town[_pointNb];
            Vector2[] points = new Vector2[_pointNb];
            
            //TODO Generate Points more distant from one another
            for (int i = 0; i < _pointNb; i++) {
                Vector2 pos = new Vector2(Random.Range(-_boundingCubeSize, _boundingCubeSize), Random.Range(-_boundingCubeSize, _boundingCubeSize));
                points[i] = pos;
                _towns[i] = new Town(pos);
            }

            Triangle[] triangles = DelaunayTriangulation.Triangulate(points);
            
            List<Edge> edges = new List<Edge>();
            foreach (Triangle triangle in triangles) {
                edges.Add(new Edge(triangle.A, triangle.B));
                edges.Add(new Edge(triangle.B, triangle.C));
                edges.Add(new Edge(triangle.C, triangle.A));
            }

            _roads = edges.Distinct().ToArray();
            
            List<Edge> matchedRoads = new List<Edge>();
            foreach (Edge road in _roads) {
                if (Random.value < _roadDensity) {
                    matchedRoads.Add(road);
                }
            }

            _roads = matchedRoads.ToArray();
            
            //TODO Remove unconnected points
        }
    }

    public class Town
    {
        private Vector2 _position;
        private float _populationScale;
        
        public Town(Vector2 position) {
            _position = position;
            _populationScale = Random.Range(0.2f,1f);
        }
        
        public Town(Vector2 position, float populationScale) {
            _position = position;
            _populationScale = populationScale;
        }

        public void DrawTown(float radius) {
            Draw.Disc(_position, radius * _populationScale, DiscColors.Flat(Color.red));
        }
    }
}
