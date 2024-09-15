namespace NextBreach.Structures.Entity;

using System.Numerics;
using Enums;
using Stream;

public struct ScreenEntity : IEntity
{
    public Vector3 Position { get; private set; }
    public Texture Texture { get; private set; }

    public void Create(RMeshReader reader)
    {
        var position = reader.ReadCoordination() * 8.0f / 2048.0f;
        Position = position;

        var texture = new Texture
        {
            Name = reader.ReadString(),
            Type = TextureType.Diffuse,
        };
        Texture = texture;
    }
}