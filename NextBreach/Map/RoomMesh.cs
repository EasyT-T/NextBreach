namespace NextBreach.Map;

using Stream;
using Structures;
using Structures.Entity;
using Stream = System.IO.Stream;

public class RoomMesh
{
    public Mesh[] Meshes { get; set; }
    public InvisibleCollision? InvisibleCollision { get; set; }
    public IEntity[] Entities { get; set; }

    public RoomMesh(Mesh[] meshes, InvisibleCollision? invisibleCollision, IEntity[]? entities = null)
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

    public void SaveToFile(string filePath)
    {
        var stream = new RMeshWriter(File.Create(filePath));
        stream.WriteVerifyText();
        stream.Write(Meshes);
        if (InvisibleCollision.HasValue)
        {
            stream.Write(InvisibleCollision.Value);
        }
        stream.Write(Entities);
        stream.Close();
    }
}