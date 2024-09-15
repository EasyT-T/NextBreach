namespace NextBreach.Structures.Entity;

using Stream;

public interface IEntity
{
    void Read(RMeshReader reader);
    void Write(RMeshWriter writer);
}