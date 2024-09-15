namespace NextBreach.Structures.Entity;

using System.Drawing;
using System.Numerics;
using Map;
using Stream;
using Math = System.Math;

public struct LightEntity : IEntity
{
    public Vector3 Position { get; set; }
    public float Range { get; set; }
    public float Intensity { get; set; }
    public Color Color { get; set; }

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
    }

    public void Write(RMeshWriter writer)
    {
        writer.Write(Position);
        writer.Write(Range);
        writer.Write($"{Color.R} {Color.G} {Color.B}");
        writer.Write(Intensity);
    }
}