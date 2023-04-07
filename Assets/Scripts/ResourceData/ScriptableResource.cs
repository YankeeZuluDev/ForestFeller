using UnityEngine;

public class ScriptableResource : ScriptableObject
{
    [SerializeField] private GameObject resourcePrefab;
    [SerializeField] private Sprite icon;

    public GameObject Prefab => resourcePrefab;
    public Sprite Icon => icon;
}
