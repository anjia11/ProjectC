using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move Atribut")]
    public float moveVelocity = 10.0f;

    [Header("Jump")]
    public float jumpForcce = 15f;
    public int jumlahJump = 1;
    public float jumpHeightMultiplier = 0.5f;

    [Header("Wall Slide")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Jump")]
    public float wallJumpVelocity = 15f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);
    

    [Header("In Air")] public float coyoteTime = 0.2f;

    [Header("Dash")]
    public float dashVelocity = 10f;
    public float dashTime = 0.2f;

    [Header("Combo Attack")]
    public float comboResetTime = 2f;
    public Vector2 [] attackMovement;
}