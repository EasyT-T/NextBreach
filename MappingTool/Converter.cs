namespace MappingTool;

using NextBreach.Map;
using NextBreach.Structures.Entity;

public static class Converter
{
    public static void Convert(string filePath, string[] mappingList)
    {
        var mappings = mappingList.Select(x =>
        {
            var pair = x.Split("->");
            var original = pair[0];
            var target = pair[1];
            return new KeyValuePair<string, string>(original, target);
        }).ToDictionary();
        var mesh = RoomMesh.Load(filePath);

        for (var i = 0; i < mesh.Entities.Length; i++)
        {
            var entity = mesh.Entities[i];
            if (entity is not MeshEntity meshEntity)
            {
                continue;
            }

            if (!mappings.TryGetValue(meshEntity.Name, out var newName))
            {
                newName = meshEntity.Name;
            }

            mesh.Entities[i] = meshEntity with { Name = newName };
        }

        mesh.SaveToFile(Path.Combine("./Opt", Path.GetFileName(filePath)));
    }
}