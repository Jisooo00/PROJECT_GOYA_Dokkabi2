using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public bool bPointClickMode = false;
    public Pathfinding pathfinding;

    private List<Node> path;
    private int currentPathIndex;
    public GridManager gridManager; 

    private Vector2 movement;
    private Vector2 dest;

    [NonSerialized] public bool bInputUp = false;
    [NonSerialized] public bool bInputDown = false;
    [NonSerialized] public bool bInputLeft = false;
    [NonSerialized] public bool bInputRight = false;

    public bool IS_MOVING
    {
        get { return instance.bForceMoving || bIsMoving; }
    }

    private bool bIsMoving = false;
    private bool bForceMoving = false;
    public Vector3 dirVec;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (bPointClickMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2Int gridPosition = gridManager.GridPositionFromWorld(worldPosition);

                Debug.Log($"World Position: {worldPosition}, Grid Position: {gridPosition}");

                path = pathfinding.FindPath(
                    gridManager.GridPositionFromWorld(transform.position),
                    gridPosition);

                if (path == null)
                {
                    Debug.Log("No path found!");
                }
                else
                {
                    currentPathIndex = 0;
                }
            }
        }
        else
        {
            CheckMovement();

            if (bIsMoving)
            {
                movement.x = bInputLeft ? -1 : bInputRight ? 1 : 0;
                movement.y = bInputUp ? 1 : bInputDown ? -1 : 0;
            }
            else
            {
                movement = Vector2.zero;
#if UNITY_EDITOR || UNITY_WEBGL
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
#endif

            }

            dirVec = CharacterAppearance.instance.GetDirection();
        }
        

    }

    void CheckMovement()
    {

        bool bMoveX = (bInputLeft || bInputRight);
        bool bMoveY = (bInputUp || bInputDown);

        bIsMoving = (bMoveX || bMoveY);

    }

    void FixedUpdate()
    {
        if (bPointClickMode)
        {
            if (path != null && currentPathIndex < path.Count)
            {
                Vector2 targetPosition = (Vector2)path[currentPathIndex].position;
                Vector2 direction = (targetPosition - rb.position).normalized;
                float distance = Vector2.Distance(rb.position, targetPosition);

                // 장애물 충돌 체크
                RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, distance, 6);
                if (hit.collider == null) // 장애물이 없으면 정상 이동
                {
                    rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime));
                }
                else
                {
                    // 장애물 충돌 시, 표면을 따라 슬라이딩 처리
                    Vector2 slideDirection = Vector2.Perpendicular(hit.normal).normalized;
                    rb.MovePosition(rb.position + slideDirection * moveSpeed * Time.fixedDeltaTime);

                    Debug.Log("Sliding along obstacle");
                }

                if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
                {
                    currentPathIndex++;
                }
            }
        }
        else
        {
            float debuf = movement.x != 0f && movement.y != 0f ? 0.8f : 1.0f;
            if (bForceMoving)
            {
                var dir = dest - rb.position;
                if (dir.magnitude <= 0.1f)
                {
                    rb.position = dest;
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
                rb.MovePosition(rb.position + movement * moveSpeed * debuf * Time.fixedDeltaTime);
            }
        }
        
    }


    public void SetInputPos(Vector2 inputPos)
    {
        float moveX = inputPos.x;
        float moveY = inputPos.y;

        bInputUp = moveX == 0 ? moveY > 0 : moveY > 0.25f;
        bInputDown = moveX == 0 ? moveY < 0 : moveY < -0.25f;
        bInputLeft = moveY == 0 ? moveX < 0 : moveX < -0.25f;
        bInputRight = moveY == 0 ? moveX > 0 : moveX > 0.25f;

    }

    public void SetForceStop()
    {
        bInputUp = false;
        bInputDown = false;
        bInputLeft = false;
        bInputRight = false;
    }

    public void ForceMoveTo(Vector2 destination)
    {
        bForceMoving = true;
        dest = destination;
    }
    
    void OnDrawGizmos()
    {
        if (!bPointClickMode) return;
        if (path == null) return;

        Gizmos.color = Color.green;
        foreach (Node node in path)
        {
            Gizmos.DrawCube(new Vector3(node.position.x, node.position.y, 0), Vector3.one * 0.5f);
        }
    }

}
