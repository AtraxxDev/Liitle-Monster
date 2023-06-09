using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    private bool wasCursorLocked = false;
    public GameObject PanelPause;
    public string nameScene;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Pausar el juego
            Time.timeScale = 0f;
            // Desbloquear el cursor del mouse
            wasCursorLocked = Cursor.lockState == CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // Mostrar el menú de pausa (activar el lienzo del menú de pausa)
            PanelPause.SetActive(true);
        }
        else
        {
            // Reanudar el juego
            Time.timeScale = 1f;
            // Volver a bloquear el cursor del mouse si estaba bloqueado previamente
            if (wasCursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            // Ocultar el menú de pausa (desactivar el lienzo del menú de pausa)
            PanelPause.SetActive(false);

        }
    }

    public void ResumeGame()
    {
        TogglePause();
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        // Reiniciar la escena actual
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);

    }

    public void PrincipalMenu()
    {
        SceneManager.LoadScene(nameScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
