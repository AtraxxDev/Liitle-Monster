using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class abrirnivel : MonoBehaviour
{
    public void CargarNivel(string Nombreniv)
    {
        SceneManager.LoadScene(Nombreniv);
        
    }
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

}
