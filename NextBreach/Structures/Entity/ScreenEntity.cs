namespace NextBreach.Structures.Entity;

using System.Numerics;
using Enums;
using Map;
using Stream;

public struct ScreenEntity : IEntity
{
    public Vector3 Position { get; set; }
    public Texture Texture { get; set; }

    public void Read(RMeshReader reader)
    {
        var position = reader.ReadCoordination();
        Position = position;

        var texture = new Texture
        {
            Name = reader.ReadString(),
            Type = TextureType.Diffuse,
        };
        Texture = texture;
    }

    public void Write(RMeshWriter writer)
    {
        writer.Write(Position);
        writer.Write(Texture.Name);
    }
}