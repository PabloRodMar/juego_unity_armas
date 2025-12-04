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
    private float maxHP;
    public float currentHP;
    private int damage;
    private int defense;
    private float speed;
    // Public para comprobar que está bien desde el inspector
    private float speed_original;

    //Datos necesario para cambiar el color al recibir daño//   
    private Renderer render;
    private Color colorOriginal;
    public float tiempoFlash = 0.25f;

    // Prefab para la cantidad de daño
    public GameObject prefabDano;
    
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

    public void Recibirdano(float danio)
    {
        StartCoroutine(FlashDamage());

        float danioFinal = danio - defense;
        if (danioFinal < 0)
        {
            danioFinal = 0;
        }
        currentHP -= danioFinal;
        if (currentHP <= 0)
        {
            Morir();
        }
        if (prefabDano != null)
        {
            GameObject danoEnPantalla = Instantiate(prefabDano, transform.position + Vector3.up, Quaternion.identity);
            danoEnPantalla.GetComponent<DamagePopup>().Setup(danio);
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
