using UnityEngine;
using System.Collections.Generic;

public class LanzadorOrbital : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int lvl = 0;
    public int lastlvl;

    private List<GameObject> orbitals = new List<GameObject>();

    void Start()
    {
        lastlvl = lvl;
        CrearOrbitals();
    }

    void Update()
    {
        if (lastlvl != lvl)
        {
            lastlvl = lvl;
            CrearOrbitals();
        }
    }

    private void CrearOrbitals()
    {
        // Eliminar bolas viejas
        foreach (var orb in orbitals)
        {
            if (orb != null) Destroy(orb);
        }
        orbitals.Clear();

        // Esto hace que al nivel 1 sean 3 bolas y las bolas solo se añadan cada 2 niveles
        int numBolas = 3 + Mathf.FloorToInt((lvl - 1) / 2f);
        // Crear las nuevas
        float angleStep = 360f / numBolas;

        for (int i = 0; i < numBolas; i++)
        {
            float angle = angleStep * i;

            GameObject newOrb = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            MisilOrbital mo = newOrb.GetComponent<MisilOrbital>();
            mo.nivelArma = lvl;
            mo.initialAngle = angle;

            orbitals.Add(newOrb);
        }
    }
}