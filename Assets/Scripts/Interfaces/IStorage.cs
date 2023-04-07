/// <summary>
///  An interface for resource storage
/// </summary>
public interface IStorage
{
    void Add(ScriptableResource scriptableResource, int amount);
    void Remove(ScriptableResource scriptableResource, int amount);
    bool TryGetCorrespondingStorageItem(ScriptableResource scriptableResource, out StorageItem correspondingStorageItem);
}
