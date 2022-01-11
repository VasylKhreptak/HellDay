using UnityEngine;

public class WhiteZombieAIMovement : ZombieAIMovement
{
    [SerializeField] private PitChecker _pitChecker;

    protected override bool CanJump()
    {
        return (_obstacleChecker.isObstacleClose || _pitChecker.isPitNearp) &&
               _groundChecker.IsGrounded();
    }
}