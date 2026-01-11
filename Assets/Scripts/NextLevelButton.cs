using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        
        if (button != null)
            button.onClick.AddListener(LoadNextLevel);
    }

    private void LoadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;
        
        SceneManager.LoadScene(nextLevel);
    }

    private void OnDestroy()
    {
        if (button != null)
            button.onClick.RemoveListener(LoadNextLevel);
    }
}
