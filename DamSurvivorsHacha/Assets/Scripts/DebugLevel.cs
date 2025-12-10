using UnityEngine;
using UnityEngine.InputSystem;

public class DebugLevel : MonoBehaviour
{
    public MonoBehaviour[] lanzadores; // todos los scripts de lanzadores en un array

    void Update()
    {
        // Guarda la tecla que se acaba de pulsar
        var k = Keyboard.current;
        if (k == null) return;

        for (int i = 0; i < lanzadores.Length; i++)
        {
            if (lanzadores[i] == null) continue;

            // Compara con las teclas 1-6.
            // Esta no es la manera convencional que dimos en clase, pero para mí
            // esta es más sencilla de implementar.
            // LvlUP Hacha (Es el elemento 0 de la lista y se activa cuando se pulsa la tecla 1)
            if (i == 0 && k.digit1Key.wasPressedThisFrame == true) LvlUP(lanzadores[i]);
            // LvlUP Hacha (Es el elemento 0 de la lista y se activa cuando se pulsa la tecla 2)
            if (i == 1 && k.digit2Key.wasPressedThisFrame == true) LvlUP(lanzadores[i]);
            // LvlUP Hacha (Es el elemento 0 de la lista y se activa cuando se pulsa la tecla 3)
            if (i == 2 && k.digit3Key.wasPressedThisFrame == true) LvlUP(lanzadores[i]);
            // LvlUP Hacha (Es el elemento 0 de la lista y se activa cuando se pulsa la tecla 4)
            if (i == 3 && k.digit4Key.wasPressedThisFrame == true) LvlUP(lanzadores[i]);
            // LvlUP Hacha (Es el elemento 0 de la lista y se activa cuando se pulsa la tecla 5)
            if (i == 4 && k.digit5Key.wasPressedThisFrame == true) LvlUP(lanzadores[i]);
            // LvlUP Hacha (Es el elemento 0 de la lista y se activa cuando se pulsa la tecla 6)
            if (i == 5 && k.digit6Key.wasPressedThisFrame == true) LvlUP(lanzadores[i]);
        }
    }

    void LvlUP(MonoBehaviour lanzador)
    {
        // Habilita el script si estaba deshabilitado
        if (!lanzador.enabled) {
            lanzador.enabled = true;
        }

        // Busca el campo de nivel
        var type = lanzador.GetType();
        var lvlField = type.GetField("lvl");

        // El (int) hace falta porque si no da error.
        int lvl = (int)lvlField.GetValue(lanzador);

        // Si no estaba desbloqueado (lvl 0) lo pone al 1
        // Si estaba desbloqueado, lo sube de nivel
        if (lvl < 1) {
            lvl = 1;
        }
        else {
            lvl += 1;
        }

        // Va al script que esté modificando en ese momento (Depende de la tecla)
        // y le actualiza su campo de nivel.
        lvlField.SetValue(lanzador, lvl);

        Debug.Log($"{lanzador.name} ahora está en nivel {lvl}");
    }
}