namespace NextBreach.Structures.Entity;

using System.Drawing;
using System.Numerics;
using Stream;
using Math = System.Math;

public struct LightEntity : IEntity
{
    public Vector3 Position { get; private set; }
    public float Range { get; private set; }
    public Color Color { get; private set; }

    public void Create(RMeshReader reader)
    {
        var position = reader.ReadCoordination() * 8.0f / 2048.0f;
        Position = position;

        var range = reader.ReadSingle() / 2000.0f;
        Range = range;

        var fullColor = reader.ReadString().Split(' ');
        var intensity = Math.Min(reader.ReadSingle() * 0.8f, 1.0f);
        var r = (int)(Math.Round(float.Parse(fullColor[0]), MidpointRounding.ToEven) * intensity);
        var g = (int)(Math.Round(float.Parse(fullColor[1]), MidpointRounding.ToEven) * intensity);
        var b = (int)(Math.Round(float.Parse(fullColor[2]), MidpointRounding.ToEven) * intensity);
        var color = Color.FromArgb(r, g, b);
        Color = color;
    }
}