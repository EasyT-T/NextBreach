namespace NextBreach.Structures;

using System.Drawing;
using System.Numerics;

public struct Vertex
{
    public Vector3 Position { get; set; }
    public Vector2 DiffuseUv { get; set; }
    public Vector2 LightmapUv { get; set; }
    public Color Color { get; set; }
}