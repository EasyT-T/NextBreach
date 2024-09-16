namespace NextBreach.Structures;

using System.Numerics;

public struct InvisibleCollision
{
    public Vector3[] Vertices { get; set; }
    public Triangle[] Triangles { get; set; }
}