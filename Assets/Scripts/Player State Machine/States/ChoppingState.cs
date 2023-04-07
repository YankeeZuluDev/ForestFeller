public class ChoppingState : AbstractState
{
    public override void StartState(PlayerStateMachine context)
    {
        context.PlayerAnimator.SetTrigger("shouldChop");

        AudioManager.Instance.PlaySFX(AudioID.Chop);
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
