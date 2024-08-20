using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    [SerializeField] private GameObject lostPanel;
    [SerializeField] private GameObject wonPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private LevelManager levelManager;




    private int stardustBalance = 0;
     [SerializeField] private TextMeshProUGUI stardustText; // Reference to the UI Text to display the balance
    private int freedPixiesBalance = 0;

     [SerializeField] private TextMeshProUGUI freedPixiesText; // Reference to the UI Text to display the balance


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LostGame()
    {
        ChangeTimeScale();
        lostPanel.SetActive(true);
    }

    public void WonGame()
    {
        ChangeTimeScale();
        wonPanel.SetActive(true);
    }


    public void ChangeTimeScale()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void PauseGame()
    {
        ChangeTimeScale();

        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
        }
        else
        {
            pausePanel.SetActive(true);
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddStardust(int amount)
    {
        stardustBalance += amount;
        UpdateUI();
    }

    public void AddFreedPixies()
    {
        freedPixiesBalance++;
        UpdateUI();
        if(freedPixiesBalance >= levelManager.pixiesRequiredToComplete)
        {
            levelManager.CompleteLevel();
        }
    }

    public void PlayNextLevel()
    {
        for (int i = 1; i < 6; i++)
        {
            if(GameManager.Instance.IsLevelCompleted(i) == false)
            {
                    SceneManager.LoadScene("Level " + i);
                    return;
            }
        }
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public bool SpendStardust(int amount)
    {
        if (stardustBalance >= amount)
        {
            stardustBalance -= amount;
            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough stardust!");
            return false;
        }
    }

    private void UpdateUI()
    {
        if (stardustText != null && freedPixiesText != null)
        {
            freedPixiesText.text = freedPixiesBalance.ToString() + "/" + levelManager.pixiesRequiredToComplete.ToString();
            stardustText.text = stardustBalance.ToString();
        }
    }
}
