using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // Scene name to load, set in the editor

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that triggered the collider has the tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Load the specified scene by its name
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
