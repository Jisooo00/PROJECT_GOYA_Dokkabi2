
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{

    public static Player instance;
    public float moveSpeed = 10f; //조절 필요
    public Rigidbody2D rb;
    
    private Vector2 movement;
    private Vector2 dest;
    
    [NonSerialized] public bool bInputUp = false;
    [NonSerialized] public bool bInputDown = false;
    [NonSerialized] public bool bInputLeft = false;
    [NonSerialized] public bool bInputRight = false;
    [NonSerialized] public bool bIsDialogPlaying = false;
    [NonSerialized] public bool bIgnoreInput = false;

    public UIManager_tmp UIManagerTMP;
    
    public bool IS_MOVING
    {
        get
        {
            return instance.bForceMoving || bIsMoving;
        }
    }
    
    private bool bIsMoving = false;
    private bool bForceMoving = false;
    public Vector3 dirVec;
    [NonSerialized] public GameObject mScanObject = null;
    
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
        if (bIsDialogPlaying || bForceMoving || bIgnoreInput)
            return;
        
        CheckMovement();

        if (bIsMoving)
        {
            movement.x = bInputLeft ? -1 : bInputRight ? 1: 0;
            movement.y = bInputUp ? 1 : bInputDown ? -1 : 0;
        }
        else
        {
            movement = Vector2.zero;
/*            
#if UNITY_EDITOR || UNITY_WEBGL
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if (movement != Vector2.zero)
            {
                UIManagerTMP.ForceJoystickMove((int)movement.x,(int)movement.y);
            }
            else if(!UIManagerTMP.IsJoystickZero)
                UIManagerTMP.ForceJoystickMove((int)movement.x,(int)movement.y);
#endif*/
        }
       
        dirVec = CharacterAppearance.instance.GetDirection();

    }

    void CheckMovement()
    {

        bool bMoveX = (bInputLeft || bInputRight);
        bool bMoveY = (bInputUp || bInputDown);

        bIsMoving = (bMoveX  || bMoveY);
        
    }
    void FixedUpdate()
    {
        float debuf = movement.x != 0f && movement.y != 0f ? 0.8f : 1.0f; 
        if (bForceMoving)
        {
            var dir = dest - rb.position;
            if (dir.magnitude <= 0.1f)
            {
                rb.position  = dest;
                bForceMoving = false;
                return;
            }
            else
            {
                rb.MovePosition(rb.position + dir.normalized * moveSpeed * Time.fixedDeltaTime);
                dirVec = CharacterAppearance.instance.GetDirection();
            }
        }
        else
        {
            rb.MovePosition(rb.position + movement * moveSpeed*debuf * Time.fixedDeltaTime);
        }
        
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == 9)
        {
            Debug.LogError("??");
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6 && other.transform.parent.gameObject)
        {
            mScanObject = null;
        }
        
        if (other.gameObject.layer == 9 && other.gameObject)
        {
            mScanObject = null;
        }
    }

    public void SetInputPos(Vector2 inputPos)
    {
        float moveX = inputPos.x;
        float moveY = inputPos.y;

        bInputUp    = moveX == 0 ? moveY > 0 : moveY > 0.25f; 
        bInputDown = moveX == 0 ? moveY < 0 : moveY < -0.25f; 
        bInputLeft  = moveY == 0? moveX < 0 : moveX < -0.25f;
        bInputRight = moveY == 0? moveX > 0 : moveX > 0.25f; 
        
    }


    public void SetForceStop(){
        bInputUp    = false; 
        bInputDown = false; 
        bInputLeft  = false; 
        bInputRight = false; 
    }
    
    public void ForceMoveTo(Vector2 destination)
    {
        bForceMoving = true;
        dest = destination;
    }
    
    
}
