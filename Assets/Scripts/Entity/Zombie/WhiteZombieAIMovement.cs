using UnityEngine;

public class WhiteZombieAIMovement : ZombieAIMovement
{
    [SerializeField] private PitChecker _pitChecker;

    protected override bool CanJump()
    {
        return (_obstacleChecker.isObstacleClose == true  || _pitChecker.isPitNearp == true) &&
               _groundChecker.isGrounded == true;
    }
}
