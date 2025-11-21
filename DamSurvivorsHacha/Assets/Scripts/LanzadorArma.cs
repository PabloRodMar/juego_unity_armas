using System.Collections;
using UnityEngine;

public class LanzadorArma : MonoBehaviour
{
    public GameObject armaPrefab;
    public float ratioDeDisparo = 1f; // Armas por segundo
     public float detectionRadius = 5f;
    void Start()
    {
        StartCoroutine(dispararArma());
    }

    public IEnumerator dispararArma()
    {
        while (true)
        {
            Instantiate(armaPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(ratioDeDisparo);
        }
    }
}
