using System.Collections;
using UnityEngine;

public class LanzadorHacha : MonoBehaviour
{
    public GameObject armaPrefab;
    public float ratioDeDisparo = 1f; // Armas por segundo
    // Nivel del arma
    public int lvl = 1;
    void Start()
    {
        StartCoroutine(dispararArma());
    }

    public IEnumerator dispararArma()
    {
        while (true)
        {
            int i = 0;
            while (lvl > i)
            {
                GameObject hacha = Instantiate(armaPrefab, transform.position, transform.rotation * armaPrefab.transform.rotation);
                hacha.GetComponent<Hacha>().nivelArma = lvl;
                yield return new WaitForSeconds(0.2f);
                i++;
            }
            yield return new WaitForSeconds(ratioDeDisparo);
        }
    }
}
