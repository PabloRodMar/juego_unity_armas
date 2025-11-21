using UnityEngine;

public class Rayo : MonoBehaviour
{
    [Header("Datos del Rayo")]
    private GameObject player;
    public float tiempoVida = 2.5f;
    public int damage = 25;

    // Update is called once per frame
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + player.transform.forward;
        if (player)
        {
            transform.SetParent(player.transform);
        }
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position + player.transform.forward * 4f;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.Recibirdano(damage);
            }
        }
    }

}