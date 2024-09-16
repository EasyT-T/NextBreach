namespace NextBreach.Stream;

using System.Drawing;
using System.Numerics;
using System.Text;
using Enums;
using Factories;
using Structures;
using Structures.Entity;
using Stream = System.IO.Stream;

public class RMeshReader : BinaryReader
{
    internal string VerifyText = string.Empty;
    public bool HasTriggerBox => VerifyText.EndsWith("HasTriggerBox");

    public RMeshReader(Stream input) : base(input)
    {
        if (!CheckForValid())
        {
            throw new InvalidOperationException();
        }
    }

    public bool CheckForValid()
    {
        if (string.IsNullOrEmpty(VerifyText))
        {
            VerifyText = ReadString();
        }
        return VerifyText.StartsWith("RoomMesh");
    }

    public override string ReadString()
    {
        var length = ReadInt32();
        var bytes = ReadBytes(length);
        return Encoding.ASCII.GetString(bytes);
    }

    #region Common

    public Texture? ReadTexture()
    {
        var flag = ReadByte();

        if (flag == 0)
        {
            return null;
        }

        var textureName = ReadString();

        if (string.IsNullOrEmpty(textureName))
        {
            return null;
        }

        var textureType = textureName.Contains("_lm") ? TextureType.Lightmap : TextureType.Diffuse;

        return new Texture
        {
            Name = textureName,
            Type = textureType,
        };
    }

    public Vector3 ReadCoordination()
    {
        var x = ReadSingle();
        var y = ReadSingle();
        var z = ReadSingle();

        return new Vector3(x, y, z);
    }

    public Vertex ReadVertex()
    {
        var position = ReadCoordination();

        var diffuseU = ReadSingle();
        var diffuseV = ReadSingle();
        var diffuseUv = new Vector2(diffuseU, diffuseV);

        var lightmapU = ReadSingle();
        var lightmapV = ReadSingle();
        var lightmapUv = new Vector2(lightmapU, lightmapV);

        var r = ReadByte();
        var g = ReadByte();
        var b = ReadByte();
        var color = Color.FromArgb(r, g, b);

        return new Vertex
        {
            Position = position,
            DiffuseUv = diffuseUv,
            LightmapUv = lightmapUv,
            Color = color,
        };
    }

    public Triangle ReadTriangle()
    {
        var vertexA = ReadInt32();
        var vertexB = ReadInt32();
        var vertexC = ReadInt32();

        return new Triangle
        {
            VertexA = vertexA,
            VertexB = vertexB,
            VertexC = vertexC,
        };
    }

    #endregion

    #region Mesh

    public Mesh ReadMesh()
    {
        var textures = new List<Texture>();

        for (var i = 0; i < 2; i++)
        {
            var texture = ReadTexture();

            if (!texture.HasValue)
            {
                continue;
            }

            textures.Add(texture.Value);
        }

        var vertexCount = ReadInt32();
        var vertices = new Vertex[vertexCount];

        for (var i = 0; i < vertexCount; i++)
        {
            var vertex = ReadVertex();
            vertices[i] = vertex;
        }

        var triangleCount = ReadInt32();
        var triangles = new Triangle[triangleCount];

        for (var i = 0; i < triangleCount; i++)
        {
            var triangle = ReadTriangle();
            triangles[i] = triangle;
        }

        return new Mesh
        {
            Textures = textures.ToArray(),
            Vertices = vertices,
            Triangles = triangles,
        };
    }

    public Mesh[] ReadMeshes()
    {
        var count = ReadInt32();
        var meshes = new Mesh[count];

        for (var i = 0; i < count; i++)
        {
            meshes[i] = ReadMesh();
        }

        return meshes;
    }

    #endregion

    #region InvisibleCollision

    public InvisibleCollision? ReadInvisibleCollision()
    {
        var hasInvisibleCollision = Convert.ToBoolean(ReadInt32());

        if (!hasInvisibleCollision)
        {
            return null;
        }

        var vertexCount = ReadInt32();
        var vertices = new Vector3[vertexCount];

        for (var i = 0; i < vertexCount; i++)
        {
            var vertex = ReadCoordination();
            vertices[i] = vertex;
        }

        var triangleCount = ReadInt32();
        var triangles = new Triangle[triangleCount];

        for (var i = 0; i < triangleCount; i++)
        {
            var triangle = ReadTriangle();
            triangles[i] = triangle;
        }

        return new InvisibleCollision
        {
            Vertices = vertices,
            Triangles = triangles,
        };
    }

    #endregion

    #region TriggerBox

    public TriggerBox ReadTriggerBox()
    {
        var meshCount = ReadInt32();
        var vertices = new Vector3[meshCount][];
        var triangles = new Triangle[meshCount][];

        for (var i = 0; i < meshCount; i++)
        {
            var vertexCount = ReadInt32();
            vertices[i] = new Vector3[vertexCount];

            for (var j = 0; j < vertexCount; j++)
            {
                var position = ReadCoordination();
                vertices[i][j] = position;
            }

            var triangleCount = ReadInt32();
            triangles[i] = new Triangle[triangleCount];

            for (var j = 0; j < triangleCount; j++)
            {
                var triangle = ReadTriangle();
                triangles[i][j] = triangle;
            }
        }

        var name = ReadString();

        return new TriggerBox
        {
            Name = name,
            MeshCount = meshCount,
            Vertices = vertices,
            Triangles = triangles,
        };
    }

    public TriggerBox[] ReadTriggerBoxes()
    {
        var triggerCount = ReadInt32();
        var triggerBoxes = new TriggerBox[triggerCount];

        for (var i = 0; i < triggerCount; i++)
        {
            triggerBoxes[i] = ReadTriggerBox();
        }

        return triggerBoxes;
    }

    #endregion

    #region Entity

    public IEntity ReadEntity()
    {
        var entityName = ReadString();

        var entity = EntityFactory.GetEntity(entityName);
        entity.Read(this);

        return entity;
    }

    public IEntity[] ReadEntities()
    {
        var entityCount = ReadInt32();
        var entities = new IEntity[entityCount];

        for (var i = 0; i < entityCount; i++)
        {
            entities[i] = ReadEntity();
        }

        return entities;
    }

    #endregion
}