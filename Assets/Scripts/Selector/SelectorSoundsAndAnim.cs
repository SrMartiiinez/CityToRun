
using UnityEngine;

public class SelectorSoundsAndAnim : MonoBehaviour
{
    [SerializeField] private Vector3 finalPosition;
    private Vector3 inicialPosition;


    private void Awake()
    {
        inicialPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, finalPosition, 1f);
    }

    private void OnDisable()
    {
        transform.position = inicialPosition;
    }


}
