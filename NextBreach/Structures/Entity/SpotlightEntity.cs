namespace NextBreach.Structures.Entity;

using System.Drawing;
using System.Numerics;
using Map;
using Stream;
using Math = System.Math;

public struct SpotlightEntity : IEntity
{
    public Vector3 Position { get; set; }
    public float Range { get; set; }
    public float Intensity { get; set; }
    public Color Color { get; set; }
    public Vector2 Rotation { get; set; }
    public int InnerConeAngle { get; set; }
    public int OuterConeAngle { get; set; }

    public void Read(RMeshReader reader)
    {
        var position = reader.ReadCoordination();
        Position = position;

        var range = reader.ReadSingle();
        Range = range;

        var fullColor = reader.ReadString().Split(' ');
        var r = Convert.ToByte(fullColor[0]);
        var g = Convert.ToByte(fullColor[1]);
        var b = Convert.ToByte(fullColor[2]);
        var color = Color.FromArgb(r, g, b);
        Color = color;

        var intensity = reader.ReadSingle();
        Intensity = intensity;

        var angles = reader.ReadString().Split(' ');
        var pitch = float.Parse(angles[0]);
        var yaw = float.Parse(angles[1]);
        var rotation = new Vector2(pitch, yaw);
        Rotation = rotation;

        var innerConeAngle = reader.ReadInt32();
        InnerConeAngle = innerConeAngle;

        var outerConeAngle = reader.ReadInt32();
        OuterConeAngle = outerConeAngle;
    }

    public void Write(RMeshWriter writer)
    {
        writer.Write(Position);
        writer.Write(Range);
        writer.Write($"{Color.R} {Color.G} {Color.B}");
        writer.Write(Intensity);
        writer.Write($"{Rotation.X} {Rotation.Y}");
        writer.Write(InnerConeAngle);
        writer.Write(OuterConeAngle);
    }
}