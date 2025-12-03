using System.Collections;
using UnityEngine;

public class LanzadorFrost : MonoBehaviour
{
    public GameObject armaPrefab;
    
    void Start()
    {
        Instantiate(armaPrefab, transform.position, transform.rotation);
    }
}