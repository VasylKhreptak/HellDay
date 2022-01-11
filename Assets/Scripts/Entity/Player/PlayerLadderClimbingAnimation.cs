using UnityEngine;

public class PlayerLadderClimbingAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;

    [Header("Preferences")]
    [SerializeField] private int _updateFramerate = 6;

    private readonly int ClimbingLadder = Animator.StringToHash("IsClimbingLadder");

    private Coroutine _configurableUpdate;

    private void OnEnable()
    {
        ConfigurableUpdate.StartUpdate(this, ref _configurableUpdate, _updateFramerate,
            () => { ControlLadderClimbingAnimation(); });
    }

    private void OnDisable()
    {
        ConfigurableUpdate.StopUpdate(this, ref _configurableUpdate);
    }

    private void ControlLadderClimbingAnimation()
    {
        _animator.SetBool(ClimbingLadder, LadderMovement.isClimbing);
    }
}