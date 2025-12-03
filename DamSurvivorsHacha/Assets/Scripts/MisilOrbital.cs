using UnityEngine;

public class MisilOrbital : MonoBehaviour
{
    public GameObject player; 
    public float rotationSpeed = 100f;
    public float radius = 2f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + Vector3.right * radius;
    }

    private void Update()
    {
        transform.RotateAround(player.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}