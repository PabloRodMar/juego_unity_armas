using UnityEngine;

public class Hacha : MonoBehaviour
{
    [Header("Datos del Hacha")]
    public float speed = 8f;
    public float tiempoVida = 2.5f;
    public int damage = 25;
    public int nivelArma;
    private float mulDano = 1.5f;
    private float mulVel = 0.5f;

    // Update is called once per frame
    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }
    void Update()
    {
        // El arma va más rápido por nivel. Es para que la subida de nivel sea aún más
        // notable visualmente
        transform.position += transform.forward * speed * nivelArma * mulVel * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                // Cálculo de daño básico, no puedo saber si es o no
                // un buen balanceo
                enemy.Recibirdano(damage * nivelArma * mulDano);
            }
        }
    }
  
}
