using UnityEngine;

public class LanzadorOrbital : MonoBehaviour
{
    public GameObject bulletPrefab;
    private int lvl = 1;
    private int it = 0;

    void Update()
    {
        while (it < lvl)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            it++;
        }
    }
}