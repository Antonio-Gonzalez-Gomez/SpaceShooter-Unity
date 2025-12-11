using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] GameObject musicManager;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TMP_Text scoreText;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {

        string score = ((int)(Time.timeSinceLevelLoad * 100)).ToString();
        scoreText.SetText("Score: " + score);
        gameOverPanel.SetActive(true);
        musicManager.GetComponent<AudioSource>().Stop();
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
