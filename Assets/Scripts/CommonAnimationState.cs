public class CommonAnimationState
{
    public bool IsMovingRight;
    public bool IsMovingLeft;
    public bool IsJumping;
    public bool IsFalling;

    public bool IsGravityFlipped;
}

public enum MovementState { idle, running, jumping, falling };
