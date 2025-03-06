using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Shapes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Quests
{
    [ExecuteAlways]
    public class WorldMap : ImmediateModeShapeDrawer
    {
        [SerializeField] private int _pointNb;
        [SerializeField] private float _boundingCubeSize;
        [SerializeField] private float _townRadii;
        [SerializeField, Range(0f, 1f)] private float _roadDensity;
        [SerializeField] private float _minDistanceBetweenTowns;

        private Town currentTown;
        
        private Town[] _towns;
        private Road[] _roads;

        public override void DrawShapes(Camera cam) {
            if (_roads == null ||_towns == null) {
                return;
            }
            
            using (Draw.Command(cam)) {
                Draw.LineGeometry = LineGeometry.Flat2D;
                Draw.ThicknessSpace = ThicknessSpace.Pixels;
                Draw.Thickness = 2;
                Draw.Matrix = transform.localToWorldMatrix;

                Road[] highlightedRoads = GetRoads(currentTown);
                
                foreach (Road road in _roads) {
                    road.DrawRoad(highlightedRoads.Contains(road));
                }
                
                foreach (Town town in _towns) {
                    town.DrawTown(_townRadii);
                }
            }
        }

        [Button]
        public void GenerateMap() {
            Vector2[] points = new Vector2[_pointNb];
            //TODO Generate Points more distant from one another
            for (int i = 0; i < _pointNb; i++) {
                Vector2 pos = new Vector2(Random.Range(-_boundingCubeSize, _boundingCubeSize), Random.Range(-_boundingCubeSize, _boundingCubeSize));
                points[i] = pos;
            }

            Triangle[] triangles = DelaunayTriangulation.Triangulate(points);
            List<Edge> edges = new List<Edge>();
            foreach (Triangle triangle in triangles) {
                edges.Add(new Edge(triangle.A, triangle.B));
                edges.Add(new Edge(triangle.B, triangle.C));
                edges.Add(new Edge(triangle.C, triangle.A));
            }
            Edge[] roads = edges.Distinct().ToArray();
            List<Edge> matchedRoads = new List<Edge>();
            foreach (Edge road in roads) {
                if (Random.value < _roadDensity) {
                    matchedRoads.Add(road);
                }
            }
            roads = matchedRoads.ToArray();
            
            //Convert to roads and towns
            List<Road> roadsList = new List<Road>();
            HashSet<Town> townsSet = new HashSet<Town>();
            foreach (Edge edge in roads) {
                if (!townsSet.TryGetValue(new Town(edge.A), out Town A)) {
                    A = new Town(edge.A);
                    townsSet.Add(A);
                }
                if (!townsSet.TryGetValue(new Town(edge.B), out Town B)) {
                    B = new Town(edge.B);
                    townsSet.Add(B);
                }
                A.Neighbors.Add(B);
                B.Neighbors.Add(A);
                roadsList.Add(new Road(A,B));
            }

            _towns = townsSet.ToArray();
            _roads = roadsList.Distinct().ToArray();
            
            
            //TODO Remove unconnected points

            currentTown = CalcCenterTown();
            currentTown.PopulationScale = 1.5f;
        }

        public Road[] GetRoads(Town town) {
            return _roads.Where(road => road.A.Equals(town) || road.B.Equals(town)).ToArray();
        }
        
        private Town CalcCenterTown() {
            float minDist = _towns[0].Position.sqrMagnitude;
            Town center = _towns[0];
            for (int i = 0; i < _towns.Length; i++) {
                if (_towns[i].Position.sqrMagnitude < minDist) {
                    minDist = _towns[i].Position.sqrMagnitude;
                    center = _towns[i];
                }
            }

            return center;
        }
    }

    public class Town
    {
        private readonly Vector2 _position;
        public Vector2 Position => _position;

        public List<Town> Neighbors = new List<Town>();

        public float PopulationScale { get; set; }

        public Town(Vector2 position) {
            _position = position;
            PopulationScale = Random.Range(0.2f,1f);
        }
        
        public Town(Vector2 position, float populationScale) {
            _position = position;
            PopulationScale = populationScale;
        }
        
        public void DrawTown(float radius) {
            Draw.Disc(Position, radius * PopulationScale, DiscColors.Flat(Color.red));
        }
        
        public bool Equals(Town other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return _position.Equals(other._position);
        }
        
        public override bool Equals(object obj) {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Town)obj);
        }
        
        public override int GetHashCode() {
            return _position.GetHashCode();
        }
    }

    public class Road
    {
        public Town A { get; private set; }
        public Town B { get; private set; }
        
        public float Length { get; private set; }

        public Road(Town a, Town b) {
            A = a;
            B = b;
            Length = (A.Position - B.Position).magnitude;
        }
        public void DrawRoad(bool highlighted) {
            Draw.Color = highlighted ? Color.magenta : Color.white;
            Draw.Line((Vector3)A.Position, (Vector3)B.Position);
            Draw.Color = Color.white;
        }
    }
}
