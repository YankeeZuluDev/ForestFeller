public class RunningState : AbstractState
{
    public override void StartState(PlayerStateMachine context)
    {
        if (context.InteractionHandler.IsInteracting)
        {
            context.InteractionHandler.StopInteraction();
        }

        context.PlayerAnimator.SetTrigger("shouldRun");
    }

    public override void UpdateState(PlayerStateMachine context)
    {
        // Start idling if no input detected
        if (!context.Joystick.HasInput)
        {
            context.SetState(context.IdleState);
        }
    }
}
