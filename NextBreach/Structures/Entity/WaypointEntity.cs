namespace NextBreach.Structures.Entity;

using System.Numerics;
using Stream;

public struct WaypointEntity : IEntity
{
    public Vector3 Position { get; set; }

    public void Read(RMeshReader reader)
    {
        var position = reader.ReadCoordination();
        Position = position;
    }

    public void Write(RMeshWriter writer)
    {
        writer.Write(Position);
    }
}