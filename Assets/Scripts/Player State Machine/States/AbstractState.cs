/// <summary>
/// This is a base abstract class for all player states
/// </summary>
public abstract class AbstractState
{
    public abstract void StartState(PlayerStateMachine context);
    public abstract void UpdateState(PlayerStateMachine context);
}
