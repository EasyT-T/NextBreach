namespace NextBreach.Structures.Entity;

using System.Numerics;
using Enums;
using Stream;

public struct MeshEntity : IEntity
{
    public Vector3 Position { get; set; }
    public string Name { get; set; }
    public Vector3 Rotation { get; set; }
    public Vector3 Scale { get; set; }
    public bool HasCollision { get; set; }
    public int MeshFx { get; set; }
    public Texture Texture { get; set; }

    public void Read(RMeshReader reader)
    {
        var position = reader.ReadCoordination();
        Position = position;

        var name = reader.ReadString();
        Name = name;

        var rotation = reader.ReadCoordination();
        Rotation = rotation;

        var scale = reader.ReadCoordination();
        Scale = scale;

        var hasCollision = Convert.ToBoolean(reader.ReadByte());
        HasCollision = hasCollision;

        var meshFx = reader.ReadInt32();
        MeshFx = meshFx;

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
        writer.Write(Name);
        writer.Write(Rotation);
        writer.Write(Scale);
        writer.Write(HasCollision);
        writer.Write(MeshFx);
        writer.Write(Texture.Name);
    }
}