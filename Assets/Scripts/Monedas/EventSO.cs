using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_Event", menuName = "Event_SO", order = 81)]
public class EventSO : ScriptableObject
{
    //Lista de escuchadores de eventos
    private List<EventListener> elisteners = new List<EventListener>();

    //Método que registra en la lista los escuchadores
    public void Register(EventListener listener)
    {
        elisteners.Add(listener);
    }

    //Método que borra los escuchadores de la lista
    public void Unregister(EventListener listener)
    {
        elisteners.Remove(listener);
    }

    //Recorre la lista de escuchadores y ejecuta el método de los eventos para cada listener
    public void Ocurred(GameObject go)
    {
        for (int i = 0; i < elisteners.Count; i++)
        {
            elisteners[i].OnEventOccurs(go);
        }
    }
}
