using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// ////////////////////////////////// Variables ///////////////////////
    /// </summary>
    //Referencia al jugador//
    private GameObject player;

    //Info del SO//
    public EnemyStats Stats;

    //Stats propios//
    private int maxHP;
    public int currentHP;
    private int damage;
    private int defense;
    private float speed;
    // Public para comprobar que está bien desde el inspector
    public float speed_original;

    //Datos necesario para cambiar el color al recibir daño//   
    private Renderer render;
    private Color colorOriginal;
    private float tiempoFlash = 0.5f;
    
    /// <summary>
    /// /////////////////////////////////// Funciones Unity ///////////////////////////////
    /// </summary>
    void Awake()
    {
        maxHP = Stats.MaxHP;
        currentHP = maxHP;
        damage = Stats.Damage;
        defense = Stats.Defense;
        speed = Stats.Speed;
    }
    void Start()
    {
        speed_original = speed;
        render = GetComponentInChildren<Renderer>();
        colorOriginal = render.material.color;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //Cojo la direccion
            Vector3 direccion = player.transform.position - transform.position;
            direccion.Normalize();

            //Moverme hacia el jugador
            transform.position += direccion * speed * Time.deltaTime;

        }
    }

    public void Recibirdano(int danio)
    {
        StartCoroutine(FlashDamage());

        int danioFinal = danio - defense;
        if (danioFinal < 0)
        {
            danioFinal = 0;
        }
        currentHP -= danioFinal;
        if (currentHP <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Destroy(gameObject);
    }

    private IEnumerator FlashDamage()
    {
        render.material.color = Color.white;
        yield return new WaitForSeconds(tiempoFlash);
        render.material.color = colorOriginal;
    }

    // Estas dos funciones son utilizadas en la FrostZone, una para cuando entra en el área...
    public void AplicarRalentizacion(float relentizador)
    {
        speed = speed_original * relentizador;
    }

    // ... y otra para cuando sale
    public void QuitarRalentizacion()
    {
        speed = speed_original;
    }
}
