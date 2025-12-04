using UnityEngine;
using System.Collections.Generic;

public class LanzadorOrbital : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int lvl = 1;
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

        // Crear las nuevas
        float angleStep = 360f / lvl;

        for (int i = 0; i < lvl; i++)
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