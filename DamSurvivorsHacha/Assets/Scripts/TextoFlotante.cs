using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer = 1f; 
    private float moveSpeed = 2f; 

    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    //Esta función la llamaremos justo al crear el objeto para ponerle la cantidad
    public void Setup(float damageAmount)
    {
        textMesh.text = damageAmount.ToString();
    }

    void Update()
    {
        //1. Mover hacia arriba
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        //2. Mirar siempre a la cámara (billboard effect)
        //Invertimos la dirección para que mire DE FRENTE y no nos dé la espalda
        var cam = Camera.main.transform;
        transform.forward = cam.forward;

        //3. Temporizador para desaparecer
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            //Efecto de desvanecimiento (opcional, pero queda pro)
            Color textColor = textMesh.color;
            textColor.a -= 3f * Time.deltaTime;// Se vuelve transparente rápido
            textMesh.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}