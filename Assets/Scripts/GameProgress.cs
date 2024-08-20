using UnityEngine;

[CreateAssetMenu(fileName = "GameProgress", menuName = "Game/GameProgress")]
public class GameProgress : ScriptableObject
{
    public bool level1Completed;
    public bool level2Completed;
    public bool level3Completed;
    public bool level4Completed;
    public bool level5Completed;
}
