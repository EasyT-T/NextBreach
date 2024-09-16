namespace NextBreach.Structures;

public struct Mesh
{
    public Texture[] Textures { get; set; }
    public Vertex[] Vertices { get; set; }
    public Triangle[] Triangles { get; set; }
}