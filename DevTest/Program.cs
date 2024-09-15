namespace DevTest;

using NextBreach.Map;

internal static class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please specify the RoomMesh file directory.");
            return;
        }

        var filePath = args[0];

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Can't find the RoomMesh file.");
            return;
        }

        using var output = new StreamWriter(File.Create("mesh.log"));

        var roomMesh = RoomMesh.Load(filePath);
        var totalTexture = 0;
        var totalVertices = 0;
        var totalTriangles = 0;

        for (var i = 0; i < roomMesh.Meshes.Length; i++)
        {
            output.WriteLine($"=== Mesh {i} ===");
            var mesh = roomMesh.Meshes[i];

            for (var j = 0; j < mesh.Textures.Length; j++)
            {
                output.WriteLine($"[Texture {i}-{j}] {mesh.Textures[j].Name}({mesh.Textures[j].Type})");
                totalTexture++;
            }

            for (var j = 0; j < mesh.Vertices.Length; j++)
            {
                output.WriteLine($"[Vertex {i}-{j}] {mesh.Vertices[j].Position} | {mesh.Vertices[j].Color} | DiffUV {mesh.Vertices[j].DiffuseUv} | LmUV {mesh.Vertices[j].LightmapUv}");
                totalVertices++;
            }

            for (var j = 0; j < mesh.Triangles.Length; j++)
            {
                output.WriteLine($"[Triangle {i}-{j}] {mesh.Triangles[j].VertexA} {mesh.Triangles[j].VertexB} {mesh.Triangles[j].VertexC}");
                totalTriangles++;
            }
        }

        for (var i = 0; i < roomMesh.Entities.Length; i++)
        {
            var entity = roomMesh.Entities[i];
            output.WriteLine($"[Entity {i}] {entity.GetType().Name}");
        }

        output.WriteLine();
        output.WriteLine("Total meshes: " + roomMesh.Meshes.Length);
        output.WriteLine("Total textures: " + totalTexture);
        output.WriteLine("Total vertices: " + totalVertices);
        output.WriteLine("Total triangles: " + totalTriangles);
        output.WriteLine("Total entities: " + roomMesh.Entities.Length);
    }
}