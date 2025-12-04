using System.Collections;
using UnityEngine;

public class LanzadorFrost : MonoBehaviour
{
    public GameObject armaPrefab;
    public Transform player;
    
    void Start()
    {
        Instantiate(armaPrefab, player.position, transform.rotation);
    }
}