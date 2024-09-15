namespace NextBreach.Factories;

using Structures.Entity;

public static class EntityFactory
{
    public static Dictionary<string, Type> Entities { get; } = new Dictionary<string, Type>
    {
        { "screen", typeof(ScreenEntity) },
        { "waypoint", typeof(WaypointEntity) },
        { "light", typeof(LightEntity) },
        { "spotlight", typeof(SpotlightEntity) },
        { "mesh", typeof(MeshEntity) },
        { "model", typeof(ModelEntity) },
        { "soundemitter", typeof(SoundEmitterEntity) },
    };

    public static IEntity GetEntity(string entityName)
    {
        if (!Entities.TryGetValue(entityName.ToLower(), out var entityType))
        {
            throw new ArgumentException("Invalid entity name. (Not found.)", nameof(entityName));
        }

        var entity = Activator.CreateInstance(entityType) as IEntity;

        return entity ?? throw new ArgumentException("Invalid entity name. (Not a valid entity.)");
    }
}