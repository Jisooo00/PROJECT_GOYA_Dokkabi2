using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap tilemap;
    public LayerMask obstacleLayer;
    public float nodeSize = 1f;

    private Node[,] grid;
    private Vector2Int gridOrigin; // 그리드 원점 (bounds.xMin, bounds.yMin)
    private Vector2Int gridSize;

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        BoundsInt bounds = tilemap.cellBounds; // 타일맵 전체 크기
        gridSize = new Vector2Int(bounds.size.x, bounds.size.y);
        gridOrigin = new Vector2Int(bounds.xMin, bounds.yMin); // 그리드의 음수 기준점

        grid = new Node[gridSize.x, gridSize.y];

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector2 worldPoint = tilemap.CellToWorld(new Vector3Int(x, y, 0)) + new Vector3(0.5f, 0.5f, 0);
                bool walkable = !Physics2D.OverlapCircle(worldPoint, nodeSize / 2, obstacleLayer);
                grid[x - gridOrigin.x, y - gridOrigin.y] = new Node(new Vector2Int(x, y), walkable);
            }
        }
    }

    public Node GetNodeAt(Vector2Int position)
    {
        Vector2Int adjustedPosition = position - gridOrigin; // 음수 보정
        if (adjustedPosition.x >= 0 && adjustedPosition.x < gridSize.x &&
            adjustedPosition.y >= 0 && adjustedPosition.y < gridSize.y)
        {
            return grid[adjustedPosition.x, adjustedPosition.y];
        }
        return null; // 유효하지 않은 위치
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                Vector2Int neighborPos = node.position + new Vector2Int(x, y);

                if (neighborPos.x >= gridOrigin.x && neighborPos.x < gridOrigin.x + gridSize.x &&
                    neighborPos.y >= gridOrigin.y && neighborPos.y < gridOrigin.y + gridSize.y)
                {
                    neighbors.Add(GetNodeAt(neighborPos));
                }
            }
        }

        return neighbors;
    }

    public Vector2Int GridPositionFromWorld(Vector2 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition); // 월드 좌표 → 셀 좌표
        return new Vector2Int(cellPosition.x, cellPosition.y);
    }
}