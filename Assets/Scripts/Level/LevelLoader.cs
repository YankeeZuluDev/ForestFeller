using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for loading the game
/// </summary>
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject terrainPrefab;
    [SerializeField] private GameObject resourcePoolPrefab;
    [SerializeField] private GameObject UIManagerPrefab;
    [SerializeField] private GameObject audioManagerPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject followCameraPrefab;
    [SerializeField] private GameObject levelGoalCheckerPrefab;

    [SerializeField] private List<LevelDefinition> levelDefinitionsList;

    private int currentLevelID;

    #region Dependencies

    private Transform levelParent;
    private Transform managersParent;
    private Transform spawnablesParent;
    private UIManager uIManager;
    private AudioManager audioManager;
    private PlayerMovement playerMovement;
    private PlayerStateMachine playerStateMachine;
    private PlayerIconDisplay playerIconDisplay;
    private FollowCamera followCamera;
    private LevelGoalChecker levelGoalChecker;
    private IResourceReceiver levelGoalCheckerReceiver;
    private TruckIconDisplay truckIconDisplay;
    private GameObject terrainGameObject;

    #endregion

    private void Awake()
    {
        // Get current level ID from PlayerPrefs
        currentLevelID = PlayerPrefs.GetInt("Level", 0);

        LoadParents();
        LoadGame();
        InitializeGame();
        LoadCurrentLevel();
    }

    #region Load game

    private void LoadParents()
    {
        managersParent = new GameObject("ManagersParent").transform;
        levelParent = new GameObject("LevelParent").transform;
        spawnablesParent = new GameObject("SpawnablesParent").transform;
        spawnablesParent.SetParent(levelParent);
    }

    private void LoadGame()
    {
        // Load resource pool
        Instantiate(resourcePoolPrefab, managersParent);

        // Load UI 
        uIManager = Instantiate(UIManagerPrefab, managersParent).GetComponent<UIManager>();

        // Load audio manager
        audioManager = Instantiate(audioManagerPrefab, managersParent).GetComponent<AudioManager>();

        // Load player
        GameObject playerGameObject = Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation, levelParent);
        playerMovement = playerGameObject.GetComponent<PlayerMovement>();
        playerStateMachine = playerGameObject.GetComponent<PlayerStateMachine>();
        playerIconDisplay = playerGameObject.GetComponent<PlayerIconDisplay>();

        // Load follow camera
        followCamera = Instantiate(followCameraPrefab, followCameraPrefab.transform.position, followCameraPrefab.transform.rotation).GetComponent<FollowCamera>();
    }

    private void InitializeGame()
    {
        // Initialzie UI
        Camera camera = followCamera.GetComponent<Camera>();
        uIManager.InitializeUIManager(camera);

        // Initialize audio manager
        audioManager.InitializeAudioManagerDictionary();

        // Initialzie player
        playerMovement.InitializePlayerMovement(uIManager.Joystick);
        playerStateMachine.InitalizePlayerStateMachine(uIManager.Joystick);
        playerIconDisplay.InitializePlayerIconDisplay(uIManager.UIIconDisplay);

        // Initialize follow camera
        followCamera.InitializeFollowCamera(playerMovement);
    }

    #endregion

    #region Load Level, save level and increment current level ID 

    public void LoadCurrentLevel()
    {
        // Get current level definition
        LevelDefinition currentLevel = GetCurrentLevelDefinition(currentLevelID);

        // Load truck
        levelGoalChecker = Instantiate(levelGoalCheckerPrefab, levelGoalCheckerPrefab.transform.position, levelGoalCheckerPrefab.transform.rotation, levelParent).GetComponent<LevelGoalChecker>();
        levelGoalCheckerReceiver = levelGoalChecker.GetComponent<IResourceReceiver>();
        truckIconDisplay = levelGoalChecker.GetComponent<TruckIconDisplay>();
        levelGoalChecker.InitializeLevelGoalChecker(currentLevel.LevelResouceGoal);
        levelGoalCheckerReceiver.Receivables = currentLevel.LevelResouceGoal.ToResourceList();
        truckIconDisplay.InitialzieTruckIconDisplay(currentLevel.LevelResouceGoal);

        // Load terrain
        if (terrainGameObject == null)
            terrainGameObject = Instantiate(terrainPrefab, levelParent);

        // Set terrain scale
        terrainGameObject.transform.localScale = new Vector3(currentLevel.TerrainSize, currentLevel.TerrainSize, 1);

        // Load spawnables
        LoadSpawnables(currentLevel);
    }

    private void LoadSpawnables(LevelDefinition levelDefinition)
    {
        foreach (SpawnableData spawnable in levelDefinition.SpawnableData)
        {
            GameObject spawnableGameObject = Instantiate(spawnable.SpawnablePrefab, spawnable.Position, spawnable.Rotation, spawnablesParent);

            spawnableGameObject.transform.localScale = spawnable.Scale;
        }
    }

    public void SaveCurrentLevel()
    {
        // Save current level ID to PlayerPrefs
        PlayerPrefs.SetInt("Level", currentLevelID);
    }

    public void IncrementCurrentLevelID()
    {
        // Exit if current level ID exceeds amount of level definition in list
        if (currentLevelID >= levelDefinitionsList.Count)
            return;

        // Increment current level ID
        currentLevelID++;
    }

    #endregion

    private LevelDefinition GetCurrentLevelDefinition(int levelID)
    {
        // If level ID exceeds amount of level definition in list, load random
        if (levelID >= levelDefinitionsList.Count)
            return levelDefinitionsList[Random.Range(0, levelDefinitionsList.Count)];

        // Else return corresponding level definition
        return levelDefinitionsList[levelID];
    }
}
