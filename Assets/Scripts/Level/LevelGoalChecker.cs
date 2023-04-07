using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for checking if the level goal was achieved or not
/// </summary>
public class LevelGoalChecker : MonoBehaviour, IInteractable, IResettable
{
    [SerializeField] private GameEvent gameEndEvent;
    [SerializeField] private float gameEndDelay;

    private IResourceReceiver resourceReceiver;
    private List<StorageItem> levelGoal;

    private IStorage storage;

    public List<StorageItem> LevelGoal { get => levelGoal; set => levelGoal = value; }
    public bool DisableInteraction { get; set; }

    public void InitializeLevelGoalChecker(List<StorageItem> levelGoal)
    {
        this.levelGoal = levelGoal;
    }

    private void Awake()
    {
        resourceReceiver = GetComponent<ResourceReceiver>();
        storage = GetComponent<Storage>();
    }

    public void CheckLevelGoalStatus()
    {
        int goalsAchieved = 0;

        // Traverse level goal
        foreach (StorageItem levelGoalStorageItem in levelGoal)
        {
            // Get corresponding resource in storage
            if (storage.TryGetCorrespondingStorageItem(levelGoalStorageItem.Resource, out StorageItem correspondingStorageItem))
            {
                // Check If the resource goal is acheived
                if (levelGoalStorageItem.Amount <= correspondingStorageItem.Amount)
                {
                    // Increment the number of achieved goals
                    goalsAchieved++;

                    // Check if the corresponding resource is in the list of receivables
                    if (resourceReceiver.Receivables.Contains(correspondingStorageItem.Resource))
                    {
                        // Remove resource from the list of receivables
                        resourceReceiver.RemoveReceivable(correspondingStorageItem.Resource);
                    }
                }
                else
                {
                    // Exit if required resource amount is bigger than resource amount in storage
                    continue;
                }
            }
            else
            {
                // Exit if no corresponding resource found in storage
                continue;
            }
        }

        // If all the level goals are aheived, wait untill the player dispenses all the resources and raise game end event 
        if (goalsAchieved == levelGoal.Count)
            StartCoroutine(WaitUntillAllItemsReceived());
    }

    private IEnumerator WaitUntillAllItemsReceived()
    {
        // Wait untill all resources received
        yield return new WaitUntil(() => !DisableInteraction);

        // Wait for game end delay
        yield return new WaitForSeconds(gameEndDelay);

        // Play win sound
        AudioManager.Instance.PlaySFX(AudioID.Won);

        // Raise game end event
        if (gameEndEvent != null)
            gameEndEvent.Raise();

        Debug.Log("<color=green>LEVEL COMPLETE</color>");
    }

    public void ResetGameObject()
    {
        Destroy(gameObject);
    }
}
