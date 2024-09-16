namespace NextBreach.Structures.Entity;

using System.Numerics;
using Stream;

public struct SoundEmitterEntity : IEntity
{
    public Vector3 Position { get; set; }
    public int Id { get; set; }
    public float Range { get; set; }

    public void Read(RMeshReader reader)
    {
        var position = reader.ReadCoordination();
        Position = position;

        var id = reader.ReadInt32();
        Id = id;

        var range = reader.ReadSingle();
        Range = range;
    }

    public void Write(RMeshWriter writer)
    {
        writer.Write(Position);
        writer.Write(Id);
        writer.Write(Range);
    }
}