using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class MisilDoble : MonoBehaviour
{
    [Header("Datos del arma")]
    public float radioBusqueda = 20f;
    public float delayEntreMisiles = 0.15f;
    public int n_proyectiles = 2;
    public LayerMask enemyLayer;

    [Header("Proyectil")]
    public GameObject proyectilPrefab;

    private GameObject player;

    void Start()
    {
        // Posicionar el arma en el jugador
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
        transform.SetParent(player.transform);

        // Comenzar lógica principal
        StartCoroutine(Disparar());

        // Destruir el arma después de su acción
        Destroy(gameObject);
    }



    IEnumerator Disparar()
    {
        // 1. Buscar enemigos en radio
        Collider[] hits = Physics.OverlapSphere(transform.position, radioBusqueda);

        if (hits.Length == 0)
            yield break;

        // 2. Ordenar por distancia y coger los 2 más cercanos
        List<Transform> objetivos = hits
            .Select(h => h.transform)
            .OrderBy(t => Vector3.Distance(transform.position, t.position))
            .Take(n_proyectiles)
            .ToList();

        // 3. Disparar a cada objetivo
        foreach (var objetivo in objetivos)
        {
            DispararMisil(objetivo);
            yield return new WaitForSeconds(delayEntreMisiles);
        }
    }

    void DispararMisil(Transform objetivo)
    {
        // Instanciar proyectil
        GameObject projectile = Instantiate(proyectilPrefab, transform.position, Quaternion.identity);

        MisilVarita misilV = projectile.GetComponent<MisilVarita>();
        misilV.enemigo = objetivo;

        // Calcular dirección fija hacia el enemigo
        Vector3 dir = (objetivo.position - transform.position).normalized;

        // Pasársela al script del proyectil
        projectile.GetComponent<LanzadorVarita>().dispararArma();
    }
}
