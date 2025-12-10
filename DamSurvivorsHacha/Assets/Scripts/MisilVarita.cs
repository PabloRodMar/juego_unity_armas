using UnityEngine;

public class MisilVarita : MonoBehaviour
{
    public float speed = 20f;
    public float tiempoVida = 2f;
    public int damage = 25;

    public bool unlocked;
    public int nivelArma = 1;

    private Transform objetivo;

    public void SetObjetivo(Transform obj)
    {
        objetivo = obj;
    }

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        if (objetivo == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            objetivo.position,
            speed * Time.deltaTime * nivelArma * 0.75f
        );
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.Recibirdano(damage * nivelArma * 1.2f);
                Destroy(gameObject);
            }
        }
    }
}