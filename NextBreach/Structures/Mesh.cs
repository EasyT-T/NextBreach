namespace NextBreach.Structures;

public struct Mesh
{
    public Texture[] Textures { get; init; }
    public Vertex[] Vertices { get; init; }
    public Triangle[] Triangles { get; init; }
}