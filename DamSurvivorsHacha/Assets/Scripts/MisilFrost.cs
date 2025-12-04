using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class FrostZone : MonoBehaviour
{
    [Header("Datos")]
    public float ralentizacion = 0.3f;

    public GameObject player;
    public int damage = 1;
    public int lvl = 1;
    public float ticDano = 1f;
    private List<GameObject> listaEnemigos;

    void Start()
    {
        listaEnemigos = new List<GameObject>();

        StartCoroutine(recibirDano());
        player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            transform.SetParent(player.transform);
        }
    }

    void Update()
    {

    }

    // De aquí para abajo es reciclado del código del rayo, con alguna cosa nueva

    private void OnTriggerStay(Collider other)
    {
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
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy)
            {
                enemy.AplicarRalentizacion(ralentizacion * lvl * 1.3f);
            }
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
            EnemyController enemy = other.GetComponent<EnemyController>();
            listaEnemigos.Remove(other.GameObject());
            if (enemy)
            {
                enemy.QuitarRalentizacion();
            }
        }
    }
}