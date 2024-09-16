namespace NextBreach.Structures.Entity;

using System.Numerics;
using Stream;

public struct PlayerStartEntity : IEntity
{
    public Vector3 Position { get; set; }
    public Vector3 Rotation { get; set; }

    public void Read(RMeshReader reader)
    {
        var position = reader.ReadCoordination();
        Position = position;

        var angles = reader.ReadString().Split(' ');
        var pitch = float.Parse(angles[0]);
        var yaw = float.Parse(angles[1]);
        var roll = float.Parse(angles[2]);
        var rotation = new Vector3(pitch, yaw, roll);
        Rotation = rotation;
    }

    public void Write(RMeshWriter writer)
    {
        writer.Write(Position);
        writer.Write($"{Rotation.X} {Rotation.Y} {Rotation.Z}");
    }
}