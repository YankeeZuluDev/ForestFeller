using System.Collections;
using UnityEngine;

/// <summary>
/// This class is used to handle player interactions
/// </summary>

[RequireComponent(typeof(InteractableDetector))]
[RequireComponent(typeof(PlayerStateMachine))]
[RequireComponent(typeof(ResourceReceiver))]
[RequireComponent(typeof(ResourceProvider))]
[RequireComponent(typeof(Tool))]

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private float interactionDuration;

    private InteractionDefinitions interactionDefinitions = new();
    private InteractableDetector interactableDetector;
    private PlayerStateMachine playerStateMachine;
    private IResourceReceiver playerReceiver;
    private IResourceProvider playerProvider;
    private Tool tool;

    private IEnumerator currentInteraction;
    private bool isInteracting; 

    public bool IsInteracting => isInteracting;

    private void Awake()
    {
        interactableDetector = GetComponent<InteractableDetector>();
        playerStateMachine = GetComponent<PlayerStateMachine>();
        playerReceiver = GetComponent<ResourceReceiver>();
        playerProvider = GetComponent<ResourceProvider>();
        tool = GetComponent<Tool>();
    }

    public IEnumerator StartInteraction()
    {
        currentInteraction = GetCurrentInteraction();

        isInteracting = true;
        yield return StartCoroutine(currentInteraction); // Start interaction and wait for interaction to end
        isInteracting = false;
    }

    /// <summary>
    /// Get the appropriate interaction depending on the CurrentInteractable type
    /// </summary>
    private IEnumerator GetCurrentInteraction()
    {
        return interactableDetector.CurrentInteractable switch
        {
            ResourceDeposit deposit => interactionDefinitions.Interact(deposit, transform, playerStateMachine, tool, interactionDuration),
            Factory factory => interactionDefinitions.Interact(factory, playerProvider),
            ResourceStack stack => interactionDefinitions.Interact(stack, playerReceiver),
            LevelGoalChecker levelGoalChecker => interactionDefinitions.Interact(levelGoalChecker, playerProvider),
            _ => throw new System.ArgumentException("Interaction is not defined")
        };
    }

    /// <summary>
    /// Stop current interaction
    /// </summary>
    public void StopInteraction()
    {
        StopCoroutine(currentInteraction);
    }
}
