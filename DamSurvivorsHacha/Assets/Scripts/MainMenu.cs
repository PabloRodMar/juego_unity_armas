using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Main"); 
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("La aplicación ha salido."); // Esto solo se ve en el editor
    }
}