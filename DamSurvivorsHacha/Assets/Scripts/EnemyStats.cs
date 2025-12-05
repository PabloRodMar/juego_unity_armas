using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/EnemyStats", order = 0)]
public class EnemyStats : ScriptableObject {
    public int MaxHP;
    public int Damage;
    public int Defense;
    public float Speed;

    [Header("Probabilidades de XP (0-100)")]
    public float probXP_Gris = 50f;
    public float probXP_Verde = 30f;
    public float probXP_Azul = 15f;
    public float probXP_Morado = 4f;
    public float probXP_Dorado = 1f;

    [Header("Prefabs de XP")]
    public GameObject xpGris;
    public GameObject xpVerde;
    public GameObject xpAzul;
    public GameObject xpMorado;
    public GameObject xpDorado;
}
