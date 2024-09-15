namespace NextBreach.Structures;

using System.Drawing;
using System.Numerics;

public struct Vertex
{
    public Vector3 Position { get; init; }
    public Vector2 DiffuseUv { get; init; }
    public Vector2 LightmapUv { get; init; }
    public Color Color { get; init; }
}