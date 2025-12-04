using System.Collections;
using UnityEngine;

public class LanzadorFrost : MonoBehaviour
{
    public GameObject armaPrefab;
    public Transform player;
    public int lvl = 1;
    
    void Start()
    {
        armaPrefab.GetComponent<FrostZone>().nivelArma = lvl;
        Instantiate(armaPrefab, player.position, transform.rotation);
    }
}