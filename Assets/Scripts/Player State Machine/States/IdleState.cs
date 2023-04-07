public class IdleState : AbstractState
{
    public override void StartState(PlayerStateMachine context)
    {
        // Get current interactable
        IInteractable currentInteractable = context.InteractableDetector.CurrentInteractable;

        // Check if the player should interact with something
        if (currentInteractable != null && !currentInteractable.DisableInteraction)
        {
            context.InteractionHandler.StartCoroutine(context.InteractionHandler.StartInteraction());
        }

        context.PlayerAnimator.SetTrigger("shouldIdle");
    }

    public override void UpdateState(PlayerStateMachine context)
    {
        // Start running if input detected
        if (context.Joystick.HasInput)
        {
            context.SetState(context.RunningState);
        }
    }
}
