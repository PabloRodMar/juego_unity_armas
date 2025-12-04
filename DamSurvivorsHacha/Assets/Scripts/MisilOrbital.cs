using UnityEngine;

public class MisilOrbital : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 100f;
    public float radius = 2f;
    public float initialAngle;
    public int damage = 20;
    public int nivelArma = 1;
    public float mulDano = 1.5f;
    public float mulVel = 0.5f;

    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // El objetivo de este cálculo matemático (para mí) complejo
        // es determinar dónde "spawneará" la bola al subir de nivel.
        // El primer nivel da más o menos igual, pero los siguientes niveles
        // deben "spawnear" en un ángulo contrario a la bola anterior.
        float rad = initialAngle * Mathf.Deg2Rad;
        offset = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * radius;
    }

    void Update()
    {
        // Rotar el offset sin importar la rotación del player
        offset = Quaternion.Euler(0, rotationSpeed * Time.deltaTime * nivelArma * mulVel, 0) * offset;
        transform.position = player.position + offset;
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