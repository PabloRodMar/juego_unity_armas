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
    public int cantidadMisiles = 2;
    public LayerMask enemyLayer;

    [Header("Proyectil")]
    public GameObject proyectilPrefab;
    public Transform player;

    void Start()
    {
        StartCoroutine(DispararAmbos());
    }

    IEnumerator DispararAmbos()
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
            .Take(cantidadMisiles)
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

            yield return new WaitForSeconds(cooldownMisiles);
        }
    }
}