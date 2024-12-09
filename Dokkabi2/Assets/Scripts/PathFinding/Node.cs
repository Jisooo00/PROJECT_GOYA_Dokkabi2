using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2Int position; // 노드의 위치
    public bool walkable; // 이동 가능 여부
    public Node parent; // 경로를 추적하기 위한 부모 노드

    public int gCost; // 시작점에서 현재 노드까지의 비용
    public int hCost; // 현재 노드에서 목표까지의 예상 비용
    public int FCost => gCost + hCost; // 총 비용

    public Node(Vector2Int position, bool walkable)
    {
        this.position = position;
        this.walkable = walkable;
    }
}