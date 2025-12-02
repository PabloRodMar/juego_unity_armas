using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Rayo : MonoBehaviour
{
    [Header("Datos del Rayo")]
    private GameObject player;
    public float tiempoVida = 2.5f;
    public int damage = 5;
    public float ticDano = 0.25f;
    private List<GameObject> listaEnemigos;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(recibirDano());
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + player.transform.forward * 3f;
        if (player)
        {
            transform.SetParent(player.transform);
        }
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            listaEnemigos.Add(other.GameObject());
        }
    }

    private IEnumerator recibirDano()
    {
        foreach (GameObject enemigo in listaEnemigos)
        {
            EnemyController controlador_enemigo = enemigo.GetComponent<EnemyController>();
            if (controlador_enemigo)
            {
                controlador_enemigo.Recibirdano(damage);
            }
        }
        yield return new WaitForSeconds(ticDano);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            listaEnemigos.Remove(other.GameObject());
        }
    }
}