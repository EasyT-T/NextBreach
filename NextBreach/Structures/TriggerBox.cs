namespace NextBreach.Structures;

using System.Numerics;

public struct TriggerBox
{
    public string Name { get; set; }
    public int MeshCount { get; set; }
    public Vector3[][] Vertices { get; set; }
    public Triangle[][] Triangles { get; set; }
}