namespace NextBreach.Map;

using Stream;
using Structures;
using Structures.Entity;
using Stream = System.IO.Stream;

public class RoomMesh
{
    public Mesh[] Meshes { get; }
    public InvisibleCollision? InvisibleCollision { get; }
    public IEntity[] Entities { get; }

    private RoomMesh(Mesh[] meshes, InvisibleCollision? invisibleCollision, IEntity[]? entities = null)
    {
        Meshes = meshes;
        InvisibleCollision = invisibleCollision;
        Entities = entities ?? [];
    }

    public static RoomMesh Load(Stream stream)
    {
        var roomMeshStream = new RMeshReader(stream);
        var meshes = roomMeshStream.ReadMeshes();
        var invisibleCollision = roomMeshStream.ReadInvisibleCollision();
        var entities = roomMeshStream.ReadEntities();
        roomMeshStream.Close();

        return new RoomMesh(meshes, invisibleCollision, entities);
    }

    public static RoomMesh Load(byte[] buffer)
    {
        return Load(new MemoryStream(buffer));
    }

    public static RoomMesh Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Could not find RMesh file.", filePath);
        }

        return Load(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read));
    }
}