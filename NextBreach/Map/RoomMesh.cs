namespace NextBreach.Map;

using Stream;
using Structures;
using Structures.Entity;
using Stream = System.IO.Stream;

public class RoomMesh(
    Mesh[] meshes,
    InvisibleCollision? invisibleCollision = null,
    TriggerBox[]? triggerBoxes = null,
    IEntity[]? entities = null,
    string verifyText = "RoomMesh")
{
    public string VerifyText { get; private set; } = verifyText;
    public Mesh[] Meshes { get; set; } = meshes;
    public InvisibleCollision? InvisibleCollision { get; set; } = invisibleCollision;
    public TriggerBox[] TriggerBoxes { get; set; } = triggerBoxes ?? [];
    public IEntity[] Entities { get; set; } = entities ?? [];

    public static RoomMesh Load(Stream stream)
    {
        var roomMeshStream = new RMeshReader(stream);
        var meshes = roomMeshStream.ReadMeshes();
        var invisibleCollision = roomMeshStream.ReadInvisibleCollision();
        TriggerBox[]? triggerBoxes = null;
        if (roomMeshStream.HasTriggerBox)
        {
            triggerBoxes = roomMeshStream.ReadTriggerBoxes();
        }
        var entities = roomMeshStream.ReadEntities();
        roomMeshStream.Close();

        return new RoomMesh(meshes, invisibleCollision, triggerBoxes, entities, roomMeshStream.VerifyText);
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
        stream.WriteVerifyText(VerifyText);
        stream.Write(Meshes);

        if (InvisibleCollision.HasValue)
        {
            stream.Write(InvisibleCollision.Value);
        }
        else
        {
            stream.Write(0);
        }

        if (TriggerBoxes.Length > 0)
        {
            stream.Write(TriggerBoxes);
        }

        stream.Write(Entities);
        stream.Close();
    }
}