using UnityEngine;

public class MisilOrbital : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 100f;
    public float radius = 2f;

    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector3(radius, 0, 0);
    }

    void Update()
    {
        // Rotar el offset sin importar la rotación del player
        offset = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0) * offset;
        transform.position = player.position + offset;
    }
}