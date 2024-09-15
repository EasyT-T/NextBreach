namespace NextBreach.Structures;

using System.Numerics;

public struct InvisibleCollision
{
    public Vector3[] Vertices { get; init; }
    public Triangle[] Triangles { get; init; }
}