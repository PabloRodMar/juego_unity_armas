using UnityEngine;

public class Slash : MonoBehaviour
{
    [Header("Datos del Slash")]
    public float speed = 10f;
    public float tiempoVida = 2f;
    public int damage = 250;
    public float detectionRadius = 5f;

    private GameObject objetivo;

    void Start()
    {
        Destroy(gameObject, tiempoVida);

        // Buscar enemigo al inicio
        objetivo = GetNearestEnemyWithinRadius();
    }

    void Update()
    {
        if (objetivo == null || GetNearestEnemyWithinRadius() == null) return;

        Vector3 direccion = objetivo.transform.position - transform.position;
        transform.position += direccion.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.Recibirdano(damage);
            }
            Destroy(gameObject);
        }
    }

    GameObject GetNearestEnemyWithinRadius()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;

        Vector3 pos = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(pos, enemy.transform.position);

            if (dist <= detectionRadius)
            {
                nearest = enemy;
            }
        }

        return nearest;
    }
}