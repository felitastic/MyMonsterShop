using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunnerValues", menuName = "MonsterShop/Minigame/EndlessRunner")]
public class EndlessRunner_Values : ScriptableObject
{
    [Tooltip("Speed of the monster forwards")]
    public float VerticalSpeed;
    [Tooltip("Speed of the monster left and right")]
    public float HorizontalSpeed;
    [Tooltip("Speed multiplier for every checkpoint")]
    public float SpeedModifier;
    [Tooltip("Base XP value of one powerup")]
    public float CollectableValue;
    [Tooltip("Added value for the powerup per Checkpoint")]
    public float ValueModifier;
    [Tooltip("Multiplier for reward after reaching the goal")]
    public float GoalReward;
}
