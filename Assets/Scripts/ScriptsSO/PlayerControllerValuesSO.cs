using UnityEngine;

[CreateAssetMenu(fileName = "PlayerControllerValues", menuName = "Scriptable Objects/PlayerControllerValues")]
public class PlayerControllerValuesSO : ScriptableObject
{
    //Movement variables
    [Header("Movement Variables")]
    [Range(1f, 50f)] public float maxWalkSpeed;
    [Range(1f, 50f)] public float groundAcceleration;
    [Range(1f, 50f)] public float groundDecceleration;
    [Range(1f, 50f)] public float airAcceleration;
    [Range(1f, 50f)] public float airDeceleration;

    //Jump variables
    [Header("Jump Variables")]
    [Range(1f, 50f)] public float maxJumpHeight = 4f;
    [Range(1f, 50f)] public float maxJumpDistance = 2f;
    [Range(1f, 50f)] public float fallGravityMultiplaier = 2f;



    public float Gravity { get; set; }
    public float InitialJumpVelocity { get; set; }

    //Gravity && InitialJumpVelocity calculation based on maxJumpHeight and maxJumpDistance desired
    private void CalculateValues()
    {
        InitialJumpVelocity = (2 * maxJumpHeight * maxWalkSpeed) / maxJumpDistance;
        Gravity = -(2 * maxJumpHeight * Mathf.Pow(maxWalkSpeed, 2) / Mathf.Pow(maxJumpDistance, 2));
    }

    private void OnValidate()
    {
        CalculateValues();
    }
}
