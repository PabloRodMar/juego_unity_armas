using UnityEngine;

public class LanzadorOrbital : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    private float fireTimer = 0f;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if(fireTimer >= fireRate)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            fireTimer = 0f;
        }
    }
}