using UnityEngine;

public class Experience : MonoBehaviour
{
    public float speed = 5f;               
    public float attractionRadius = 3f;    
    public int tier = 1;                   // 1 = Gris, 2 = Verde, 3 = Azul, 4 = Morado, 5 = Dorado
    public int[] xpValues = {1, 5, 10, 25, 50}; // Experiencia que da cada tier

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Si está dentro del radio, se mueve hacia el jugador
        if (distance <= attractionRadius)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aumentamos la experiencia del jugador
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                int xpToAdd = xpValues[Mathf.Clamp(tier - 1, 0, xpValues.Length - 1)];
                playerStats.ExpActual += xpToAdd;
            }

            Destroy(gameObject); // Destruir la experiencia
        }
    }
    
}