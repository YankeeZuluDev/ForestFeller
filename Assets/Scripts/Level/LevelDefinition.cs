using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A data class, that keeps level info
/// </summary>

[CreateAssetMenu(menuName = "Level Definition")]

public class LevelDefinition : ScriptableObject
{
    [SerializeField] private List<StorageItem> levelResouceGoal;
    [SerializeField] private SpawnableData[] spawnableData;
    [SerializeField] private float terrainSize;

    public List<StorageItem> LevelResouceGoal { get => levelResouceGoal; set => levelResouceGoal = value; }
    public SpawnableData[] SpawnableData { get => spawnableData; set => spawnableData = value; }
    public float TerrainSize { get => terrainSize; set => terrainSize = value; }
}
