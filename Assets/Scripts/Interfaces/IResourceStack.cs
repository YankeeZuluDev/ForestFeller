/// <summary>
///  An interface for resource stack
/// </summary>
public interface IResourceStack
{
    void Add(ScriptableResource scriptableResource, int amount);
    void Remove(ScriptableResource scriptableResource, int amount);
}
