using UnityEngine;

public class MiningState : AbstractState
{
    public override void StartState(PlayerStateMachine context)
    {
        Debug.Log("Minign");
        context.PlayerAnimator.SetTrigger("shouldMine");
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
