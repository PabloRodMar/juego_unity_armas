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

    private void SoltarExperiencia()
    {
        // Tomamos un número entre 0 y 100
        float numero_random = Random.Range(0f, 100f);

        float acumulado = 0f;

        GameObject xpPrefab = null;

        int tier = 1;
        // Explicación: Random.Range() generará un número del 0 al 100.
        // Pongamos que genera un 12. Entramos justo en el orbe gris, y vemos que
        // el número generado es <= que el acumulado (en este caso, 50 para el orbe gris)
        // Esto se repite hasta que, por ejemplo, se genere un 100, que es el único caso en el que
        // un orbe dorado se puede generar.

        // Gris
        acumulado += Stats.probXP_Gris;
        if (numero_random <= acumulado)
        { 
            xpPrefab = Stats.xpGris; 
            tier = 1; 
            SpawnXP(xpPrefab, tier);
            return;
        }

        // Verde
        acumulado += Stats.probXP_Verde;
        if (numero_random <= acumulado) 
        {
            xpPrefab = Stats.xpVerde; 
            tier = 2; 
            SpawnXP(xpPrefab, tier);
            return;
        }

        // Azul
        acumulado += Stats.probXP_Azul;
        if (numero_random <= acumulado) 
        {
            xpPrefab = Stats.xpAzul; 
            tier = 3; 
            SpawnXP(xpPrefab, tier);
            return;
        }

        // Morado
        acumulado += Stats.probXP_Morado;
        if (numero_random <= acumulado) 
        {
            xpPrefab = Stats.xpMorado; 
            tier = 4;
            SpawnXP(xpPrefab, tier);
            return;
        }

        // Dorado
        acumulado += Stats.probXP_Dorado;
        if (numero_random <= acumulado) 
        {
            xpPrefab = Stats.xpDorado; 
            tier = 5;
            SpawnXP(xpPrefab, tier);
            return;
        }
    }

    private void SpawnXP(GameObject prefab, int tier)
    {
        if (prefab != null)
        {
            GameObject xp = Instantiate(prefab, transform.position, Quaternion.identity);
            xp.GetComponent<Experience>().tier = tier;
        }
    }

    private void Morir()
    {
        SoltarExperiencia();
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
