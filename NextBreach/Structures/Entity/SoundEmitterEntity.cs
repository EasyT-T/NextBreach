namespace NextBreach.Structures.Entity;

using System.Numerics;
using Stream;

public struct SoundEmitterEntity : IEntity
{
    public Vector3 Position { get; private set; }
    public int Id { get; private set; }
    public float Range { get; private set; }

    public void Create(RMeshReader reader)
    {
        var position = reader.ReadCoordination() * 8.0f / 2048.0f;
        Position = position;

        var id = reader.ReadInt32();
        Id = id;

        var range = reader.ReadSingle();
        Range = range;
    }
}