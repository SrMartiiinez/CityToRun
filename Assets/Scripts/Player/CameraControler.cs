using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Control de la c?mara.
 */

public class CameraControler : MonoBehaviour
{
    public Transform Target;
    private Vector3 Offset;
    private float y;
    public float SpeedFollow = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Offset = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 followPos = Target.position + Offset;
        RaycastHit hit;
        //Posiciona la altura de la c?mara tras sobre pasar el l?mite de la posici?n indicada. 
        if (Physics.Raycast(Target.position, Vector3.down, out hit, 2.7f))
            y = Mathf.Lerp(y, hit.point.y, Time.deltaTime * SpeedFollow);
        else y = Mathf.Lerp(y, Target.position.y, Time.deltaTime * SpeedFollow);

        followPos.y = Offset.y + y;
        transform.position = followPos;
    }
}
