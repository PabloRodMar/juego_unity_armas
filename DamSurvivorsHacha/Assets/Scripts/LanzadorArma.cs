using System.Collections;
using UnityEngine;

public class LanzadorArma : MonoBehaviour
{
    public GameObject armaPrefab;
    public float ratioDeDisparo = 1f; // Armas por segundo
    void Start()
    {
        StartCoroutine(dispararArma());
    }

    public IEnumerator dispararArma()
    {
        while (true)
        {
            Instantiate(armaPrefab, transform.position, transform.rotation * armaPrefab.transform.rotation);
            yield return new WaitForSeconds(ratioDeDisparo);
        }
    }
}
