namespace NextBreach.Stream;

using System.Numerics;
using System.Text;
using Enums;
using Factories;
using Structures;
using Structures.Entity;
using Stream = System.IO.Stream;

public class RMeshWriter(Stream stream) : BinaryWriter(stream)
{
    public override void Write(string value)
    {
        Write(value.Length);
        Write(Encoding.ASCII.GetBytes(value));
    }

    public void WriteVerifyText()
    {
        Write("RoomMesh");
    }

    public void Write(Texture? texture, byte flag)
    {
        Write(flag);

        if (flag == 0)
        {
            return;
        }

        if (!texture.HasValue)
        {
            throw new ArgumentNullException(nameof(texture));
        }

        Write(texture.Value.Name);
    }

    public void Write(Vector3 vector3)
    {
        Write(vector3.X);
        Write(vector3.Y);
        Write(vector3.Z);
    }

    public void Write(Vertex vertex)
    {
        Write(vertex.Position);

        Write(vertex.DiffuseUv.X);
        Write(vertex.DiffuseUv.Y);

        Write(vertex.LightmapUv.X);
        Write(vertex.LightmapUv.Y);

        Write(vertex.Color.R);
        Write(vertex.Color.G);
        Write(vertex.Color.B);
    }

    public void Write(Triangle triangle)
    {
        Write(triangle.VertexA);
        Write(triangle.VertexB);
        Write(triangle.VertexC);
    }

    public void Write(InvisibleCollision invisibleCollision)
    {
        Write(1);

        Write(invisibleCollision.Vertices.Length);

        foreach (var vertex in invisibleCollision.Vertices)
        {
            Write(vertex);
        }

        Write(invisibleCollision.Triangles.Length);

        foreach (var triangle in invisibleCollision.Triangles)
        {
            Write(triangle);
        }
    }

    public void Write(IEntity entity)
    {
        var entityName = EntityFactory.Entities.FirstOrDefault(x => x.Value == entity.GetType()).Key;

        if (entityName == null)
        {
            throw new ArgumentException("Entity not registered.");
        }

        Write(entityName);
        entity.Write(this);
    }

    public void Write(Mesh mesh)
    {
        var hasLightmap = mesh.Textures.Any(x => x.Type == TextureType.Lightmap);

        if (!hasLightmap)
        {
            Write((byte)0);
        }

        foreach (var texture in mesh.Textures)
        {
            Write(texture, hasLightmap ? (byte)1 : (byte)3);
        }

        Write(mesh.Vertices.Length);

        foreach (var vertex in mesh.Vertices)
        {
            Write(vertex);
        }

        Write(mesh.Triangles.Length);

        foreach (var triangle in mesh.Triangles)
        {
            Write(triangle);
        }
    }

    public void Write(Mesh[] meshes)
    {
        Write(meshes.Length);

        foreach (var mesh in meshes)
        {
            Write(mesh);
        }
    }

    public void Write(IEntity[] entities)
    {
        Write(entities.Length);

        foreach (var entity in entities)
        {
            Write(entity);
        }
    }
}