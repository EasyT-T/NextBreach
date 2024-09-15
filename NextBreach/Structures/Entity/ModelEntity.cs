namespace NextBreach.Structures.Entity;

using System.Numerics;
using Stream;

public struct ModelEntity : IEntity
{
    public Vector3 Position { get; private set; }
    public string Name { get; private set; }
    public Vector3 Rotation { get; private set; }
    public Vector3 Scale { get; private set; }

    public void Create(RMeshReader reader)
    {
        var name = Path.GetFileNameWithoutExtension(reader.ReadString());
        Name = name;

        var position = reader.ReadCoordination() * 8.0f / 2048.0f;
        Position = position;

        var rotation = reader.ReadCoordination();
        Rotation = rotation;

        var scale = reader.ReadCoordination();
        Scale = scale;
    }
}