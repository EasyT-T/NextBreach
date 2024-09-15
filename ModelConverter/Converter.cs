namespace ModelConverter;

using NextBreach.Map;
using NextBreach.Structures;
using NextBreach.Structures.Entity;

public static class Converter
{
    public static void Convert(string fileName)
    {
        var directoryPath = Path.GetDirectoryName(fileName);
        var rmeshName = Path.GetFileNameWithoutExtension(fileName);

        var roomMesh = RoomMesh.Load(fileName);
        var entities = new IEntity[roomMesh.Entities.Length];
        Array.Copy(roomMesh.Entities, entities, entities.Length);

        for (var i = 0; i < roomMesh.Entities.Length; i++)
        {
            var entity = roomMesh.Entities[i];
            if (entity is not ModelEntity modelEntity)
            {
                continue;
            }

            var meshEntity = new MeshEntity
            {
                Position = modelEntity.Position,
                Name = modelEntity.Name,
                Rotation = modelEntity.Rotation,
                Scale = modelEntity.Scale,
                HasCollision = true,
                MeshFx = 0,
                Texture = new Texture
                {
                    Name = string.Empty,
                },
            };

            entities[i] = meshEntity;
        }

        roomMesh.Entities = entities;
        roomMesh.SaveToFile(Path.Combine(directoryPath ?? "./", $"{rmeshName}_opt.rmesh"));
    }
}