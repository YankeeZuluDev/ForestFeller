using UnityEngine;

/// <summary>
/// State machine for the player
/// </summary>

[RequireComponent(typeof(InteractableDetector))]
[RequireComponent(typeof(InteractionHandler))]
[RequireComponent(typeof(Animator))]

public class PlayerStateMachine : MonoBehaviour
{
    #region State Dependencies

    private Joystick joystick;
    private InteractableDetector interactableDetector;
    private InteractionHandler interactionHandler;
    private Animator playerAnimator;

    public Joystick Joystick => joystick;
    public InteractableDetector InteractableDetector => interactableDetector;
    public InteractionHandler InteractionHandler => interactionHandler;
    public Animator PlayerAnimator => playerAnimator;

    #endregion

    #region Player States

    public IdleState IdleState = new IdleState();
    public RunningState RunningState = new RunningState();
    public ChoppingState ChoppingState = new ChoppingState();
    public MiningState MiningState = new MiningState();

    #endregion

    private AbstractState currentState;

    public void InitalizePlayerStateMachine(Joystick joystick)
    {
        this.joystick = joystick;
    }

    private void Awake()
    {
        interactableDetector = GetComponent<InteractableDetector>();
        interactionHandler = GetComponent<InteractionHandler>();
        playerAnimator = GetComponent<Animator>();

        // Set initial state
        SetState(IdleState);
    }

    private void Update() => currentState.UpdateState(this);

    public void SetState(AbstractState newState)
    {
        if (currentState == newState) return;

        // Set current state to new state
        currentState = newState;
        currentState.StartState(this);
    }
}
