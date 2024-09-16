namespace NextBreach.Structures.Entity;

using System.Numerics;
using Stream;

public struct ModelEntity : IEntity
{
    public Vector3 Position { get; set; }
    public string Name { get; set; }
    public Vector3 Rotation { get; set; }
    public Vector3 Scale { get; set; }

    public void Read(RMeshReader reader)
    {
        var name = reader.ReadString();
        Name = name;

        var position = reader.ReadCoordination();
        Position = position;

        var rotation = reader.ReadCoordination();
        Rotation = rotation;

        var scale = reader.ReadCoordination();
        Scale = scale;
    }

    public void Write(RMeshWriter writer)
    {
        writer.Write(Name);
        writer.Write(Position);
        writer.Write(Rotation);
        writer.Write(Scale);
    }
}