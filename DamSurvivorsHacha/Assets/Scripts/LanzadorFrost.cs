using UnityEngine;

public class LanzadorFrost : MonoBehaviour
{
    public GameObject armaPrefab;
    public Transform player;
    public int lvl = 1;
    public int lvl_ant;
    private FrostZone armaInstanciada;
    
    void Start()
    {
        lvl_ant = lvl;
        GameObject instancia = Instantiate(armaPrefab, player.position, transform.rotation);
        armaInstanciada = instancia.GetComponent<FrostZone>();
        armaInstanciada.nivelArma = lvl;
    }

    void Update()
    {
        if (lvl != lvl_ant)
        {
            lvl_ant = lvl;
            armaInstanciada.nivelArma = lvl;
        }
    }

}