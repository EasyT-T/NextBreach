namespace ModelConverter;

using NextBreach.Map;
using NextBreach.Structures;
using NextBreach.Structures.Entity;

public static class Converter
{
    public static void Convert(string filePath)
    {
        var mesh = RoomMesh.Load(filePath);

        for (var i = 0; i < mesh.Entities.Length; i++)
        {
            var entity = mesh.Entities[i];
            if (entity is not ModelEntity modelEntity)
            {
                continue;
            }

            mesh.Entities[i] = new MeshEntity
            {
                Name = modelEntity.Name,
                Position = modelEntity.Position,
                Rotation = modelEntity.Rotation,
                Scale = modelEntity.Scale,
                HasCollision = true,
                MeshFx = 0,
                Texture = new Texture
                {
                    Name = string.Empty,
                },
            };
        }

        mesh.SaveToFile(Path.Combine("./Opt", Path.GetFileName(filePath)));
    }
}