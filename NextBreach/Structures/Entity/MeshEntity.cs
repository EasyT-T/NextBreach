namespace NextBreach.Structures.Entity;

using System.Numerics;
using Enums;
using Stream;

public struct MeshEntity : IEntity
{
    public Vector3 Position { get; private set; }
    public string Name { get; private set; }
    public Vector3 Rotation { get; private set; }
    public Vector3 Scale { get; private set; }
    public bool HasCollision { get; private set; }
    public int MeshFx { get; private set; }
    public Texture Texture { get; private set; }

    public void Create(RMeshReader reader)
    {
        var position = reader.ReadCoordination() * 8.0f / 2048.0f;
        Position = position;

        var name = Path.GetFileNameWithoutExtension(reader.ReadString());
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
}