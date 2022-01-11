using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_RestartButton : MonoBehaviour
{
    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}