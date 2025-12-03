using UnityEngine;

public class MisilVarita : MonoBehaviour
{
    public float speed = 15f;
    public float tiempoVida = 1.5f;
    public int damage = 25;

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
            speed * Time.deltaTime
        );
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.Recibirdano(damage);
                Destroy(gameObject);
            }
        }
    }
}