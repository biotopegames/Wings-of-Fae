using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameProgress gameProgress;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make the GameManager persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Methods to interact with the GameProgress data
    public bool IsLevelCompleted(int level)
    {
        switch (level)
        {
            case 1:
                return gameProgress.level1Completed;
            case 2:
                return gameProgress.level2Completed;
            case 3:
                return gameProgress.level3Completed;
            case 4:
                return gameProgress.level4Completed;
            case 5:
                return gameProgress.level5Completed;
            default:
                return false;
        }
    }

    public void MarkLevelCompleted(int level)
    {
        switch (level)
        {
            case 1:
                gameProgress.level1Completed = true;
                break;
            case 2:
                gameProgress.level2Completed = true;
                break;
            case 3:
                gameProgress.level3Completed = true;
                break;
            case 4:
                gameProgress.level4Completed = true;
                break;
            case 5:
                gameProgress.level5Completed = true;
                break;
            default:
                Debug.LogWarning("Invalid level number");
                break;
        }
    }
}
