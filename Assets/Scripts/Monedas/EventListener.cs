using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //La necesitamos para trabajar con los eventos de Unity

//En vez de usar directamente un evento de unity, creamos nuestra propia clase
//que deriva de los eventos de unity, para un objeto concreto
[System.Serializable]
public class UnityGameObjectEvent : UnityEvent<GameObject> { }

public class EventListener : MonoBehaviour
{
    //Donde está el evento, es decir, que objeto(listener) va a interactuar con ese evento
    public EventSO gEvent;
    //Donde queremos que esté la respuesta al evento
    public UnityGameObjectEvent response = new UnityGameObjectEvent();

    //Se llama cuando se activa el objeto
    private void OnEnable()
    {
        //Va a coger el objeto que realiza el evento
        gEvent.Register(this);
    }

    //Se llama cuando se desactiva el objeto
    private void OnDisable()
    {
        //Borramos el escuchador de la lista
        gEvent.Unregister(this);
    }

    //Método para cuando el evento ocurre
    public void OnEventOccurs(GameObject go)
    {
        //Invocamos el evento en el objeto que queremos que suceda
        response.Invoke(go);
    }
}
