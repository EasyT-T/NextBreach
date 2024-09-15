namespace NextBreach.Structures.Entity;

using System.Drawing;
using System.Numerics;
using Stream;
using Math = System.Math;

public struct SpotlightEntity : IEntity
{
    public Vector3 Position { get; private set; }
    public float Range { get; private set; }
    public Color Color { get; private set; }
    public Vector2 Rotation { get; private set; }
    public int InnerConeAngle { get; private set; }
    public int OuterConeAngle { get; private set; }

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
}