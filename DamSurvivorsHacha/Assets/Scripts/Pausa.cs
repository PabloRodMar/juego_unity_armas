using UnityEngine;
using UnityEngine.InputSystem;

public class Pausa : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject continuar;
    public GameObject salir;
    private bool pausa = false;

    private ContolPausa actions;

    void Awake()
    {
        actions = new ContolPausa();
    }

    void OnEnable()
    {
        actions.Enable();
        actions.Parar.Pausar.performed += OnPause;
    }

    void OnDisable()
    {
        actions.Parar.Pausar.performed -= OnPause;
        actions.Disable();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (!pausa) 
        {
            ActivarPausa();
        }
        else 
        {
            DesactivarPausa();
        }
    }

    void ActivarPausa()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        pausa = true;
    }

    public void DesactivarPausa()
    {
        menuPausa.SetActive(false);
        continuar.SetActive(true);
        Time.timeScale = 1f;
        pausa = false;
    }

    public void Salir()
    {
        Debug.Log("El botón está funcionando, aquí debería salir.");
        salir.SetActive(true);
        Application.Quit();
    }
}