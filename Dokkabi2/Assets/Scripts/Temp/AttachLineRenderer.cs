using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachLineRenderer : MonoBehaviour
{
    public Transform start;
    public Transform target;
    public LineRenderer line;
 
    private float length;

    private Reflector _reflector;
    
    void Start()
    {
        line = GetComponent<LineRenderer>();
        length = (target.position - start.position).magnitude;
    }

    void Update()
    {
        line.SetPosition(0, start.position);
        line.SetPosition(1, target.position);
        
        var dir = target.position - start.position;
        RaycastHit2D hit = Physics2D.Raycast(start.position, dir , length);
        if(hit.collider != null)
        {
            target.position = new Vector3(target.position.x, hit.transform.localPosition.y);
            if (hit.transform.gameObject.layer == 11 && _reflector == null){
                _reflector = hit.transform.gameObject.GetComponent<Reflector>();
                SetRefelctor();
            }
        }else if (hit.collider == null && _reflector != null)
        {
            _reflector = null;
        }
    }
    void SetRefelctor()
    {
        _reflector.SetIncidence(start.transform);
    }
}


 