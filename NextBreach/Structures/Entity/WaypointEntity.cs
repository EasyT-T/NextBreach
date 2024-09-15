namespace NextBreach.Structures.Entity;

using System.Numerics;
using Stream;

public struct WaypointEntity : IEntity
{
    public Vector3 Position { get; private set; }

    public void Create(RMeshReader reader)
    {
        var position = reader.ReadCoordination() * 8.0f / 2048.0f;
        Position = position;
    }
}