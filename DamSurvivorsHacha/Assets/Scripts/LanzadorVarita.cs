using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class LanzadorVarita : MonoBehaviour
{
    [Header("Datos de la Varita")]
    public float radioBusqueda = 20f;
    public float cooldown = 1f;
    public float cooldownMisiles = 0.15f;
    // Realmente se desbloquea a nivel 2, ya que dispara 1 proyectil por nivel
    // y si se desbloquease a nivel 1, sólo dispararía 1 (que no es lo que se pide)
    public int lvl = 1;
    public LayerMask enemyLayer;

    [Header("Proyectil")]
    public GameObject proyectilPrefab;
    public Transform player;

    void Start()
    {
        StartCoroutine(DispararOleada());
    }

    IEnumerator DispararOleada()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);
            StartCoroutine(DispararUno());
        }
    }

    IEnumerator DispararUno()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radioBusqueda, enemyLayer);

        if (hits.Length == 0)
            yield break;

        List<Transform> objetivos = hits
            .Select(h => h.transform)
            .OrderBy(t => Vector3.Distance(transform.position, t.position))
            .Take(lvl)
            .ToList();

        foreach (Transform objetivo in objetivos)
        {
            Vector3 direccion = (objetivo.position - player.position).normalized;

            GameObject misil = Instantiate(
                proyectilPrefab,
                player.position,
                Quaternion.LookRotation(direccion)
        );

            misil.GetComponent<MisilVarita>().SetObjetivo(objetivo);
            misil.GetComponent<MisilVarita>().nivelArma = lvl;

            yield return new WaitForSeconds(cooldownMisiles);
        }
    }
}