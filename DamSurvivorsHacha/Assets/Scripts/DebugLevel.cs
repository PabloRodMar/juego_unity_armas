using UnityEngine;
using UnityEngine.InputSystem;

public class DebugLevel : MonoBehaviour
{
    [Header("Arrastra aquí los scripts de los lanzadores en el Player")]
    public MonoBehaviour[] lanzadores; // todos los scripts de lanzadores en un array

    void Update()
    {
        var k = Keyboard.current;
        if (k == null) return;

        for (int i = 0; i < lanzadores.Length; i++)
        {
            if (lanzadores[i] == null) continue;

            // Compara con las teclas 1..6
            if (i == 0 && k.digit1Key.wasPressedThisFrame) ActivarYLvl(lanzadores[i]);
            if (i == 1 && k.digit2Key.wasPressedThisFrame) ActivarYLvl(lanzadores[i]);
            if (i == 2 && k.digit3Key.wasPressedThisFrame) ActivarYLvl(lanzadores[i]);
            if (i == 3 && k.digit4Key.wasPressedThisFrame) ActivarYLvl(lanzadores[i]);
            if (i == 4 && k.digit5Key.wasPressedThisFrame) ActivarYLvl(lanzadores[i]);
            if (i == 5 && k.digit6Key.wasPressedThisFrame) ActivarYLvl(lanzadores[i]);
        }
    }

    void ActivarYLvl(MonoBehaviour lanzador)
    {
        if (lanzador == null) return;

        // Habilita el script si estaba deshabilitado
        if (!lanzador.enabled) lanzador.enabled = true;

        // Busca el campo de nivel (lvl o nivelArma)
        var type = lanzador.GetType();
        var lvlField = type.GetField("lvl");

        if (lvlField == null)
        {
            Debug.LogError($"{lanzador.name} no tiene campo 'lvl'");
            return;
        }

        int lvl = (int)lvlField.GetValue(lanzador);

        // Si estaba en lvl 0 o 1, se desbloquea en 1; si ya estaba, sube de nivel
        if (lvl < 1) lvl = 1;
        else lvl += 1;

        lvlField.SetValue(lanzador, lvl);

        // Llama a ForzarActualizar si existe (opcional)
        var method = type.GetMethod("ForzarActualizar");
        if (method != null) method.Invoke(lanzador, null);

        Debug.Log($"{lanzador.name} ahora está en nivel {lvl}");
    }
}