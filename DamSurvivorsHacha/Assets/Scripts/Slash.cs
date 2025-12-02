using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Slash : MonoBehaviour
{
    [Header("Datos del Slash")]
    private GameObject player;
    public float tiempoVida = 2f;
    public int damage = 25;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
        if (player)
        {
            transform.SetParent(player.transform);
        }
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {

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
}