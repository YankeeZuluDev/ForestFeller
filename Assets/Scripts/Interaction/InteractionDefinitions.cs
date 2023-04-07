using System.Collections;
using UnityEngine;

/// <summary>
/// This class is collection of player interaction definitions
/// </summary>
public class InteractionDefinitions
{
    /// <summary>
    /// Interact with deposit
    /// </summary>
    public IEnumerator Interact(ResourceDeposit deposit, Transform playerTransform, PlayerStateMachine playerStateMachine, Tool playerTool, float interactionDuration)
    {
        // Get corresponding tool
        playerTool.SetTool(deposit);

        // Look at tween
        yield return Tweens.SmoothlyLookAt(playerTransform, deposit.transform, 0.3f); // look at duration

        // Enter interacting state
        playerStateMachine.SetState(playerStateMachine.ChoppingState);

        // Wait for interactionDuration
        yield return new WaitForSeconds(interactionDuration);

        // Take down deposit
        deposit.TakeDown();

        // Enter idle state
        playerStateMachine.SetState(playerStateMachine.IdleState);
    }

    /// <summary>
    /// Interact with factory
    /// </summary>
    public IEnumerator Interact(Factory factory, IResourceProvider playerProvider)
    {
        IResourceReceiver factoryReceiver = factory.GetComponent<ResourceReceiver>();
        playerProvider.Dispense(factoryReceiver);
        yield break;
    }

    /// <summary>
    /// Interact with stack
    /// </summary>
    public IEnumerator Interact(ResourceStack stack, IResourceReceiver playerReceiver) //םאמבמנמע
    {
        // get stack provider
        IResourceProvider stackProvider = stack.GetComponent<ResourceProvider>();
        stackProvider.Dispense(playerReceiver);
        yield break;
    }

    /// <summary>
    /// Interact with level goal checker
    /// </summary>
    public IEnumerator Interact(LevelGoalChecker levelGoalChecker, IResourceProvider playerProvider)
    {
        IResourceReceiver levelGoalReceiver = levelGoalChecker.GetComponent<ResourceReceiver>();
        playerProvider.Dispense(levelGoalReceiver);
        yield break;
    }
}
