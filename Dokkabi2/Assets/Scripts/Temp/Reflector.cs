using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{

    public Transform pivot;
    public Vector2 normal;
    public Transform tmIncidence;
    public Vector2 incidence;
    private Vector2 reflection;
    public Transform target;
    void Start()
    {
        normal = Vector2.right;
        //target.position = Vector2.zero;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rotate();
        }
        
        Debug.DrawRay(pivot.position, normal);
        if (tmIncidence!=null)
        {
            Debug.DrawRay(pivot.position, reflection*-1,Color.red);
        }
    }

    public void Rotate()
    {
        transform.localRotation = Quaternion.Euler(0,0,-45)*transform.localRotation;
        normal = Quaternion.Euler(0, 0, -45)*normal;
        SetIncidenceAngle();
    }
    public void SetIncidence(Transform tm)
    {
        tmIncidence = tm;
        SetIncidenceAngle();
    }
    public void SetIncidenceAngle()
    {
        if (tmIncidence == null)
            return;
        incidence = tmIncidence.position - pivot.position;
        reflection = Vector2.Reflect(incidence.normalized, normal);
        //해당 반사각 값을 활용하여 영역이내에 존재하는지 검사하고
        //조건에 맞는 경우, target값을 반사각 방향으로 Line Renderer 세팅
    }
}
