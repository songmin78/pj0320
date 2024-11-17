using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Net;

public class TestMsMove : MonoBehaviour
{

}

public class TileManager
{
    public Tilemap RoadTilemap { get; set; }
    public Transform StartPos { get; set; }
    public List<Transform> EndPosList { get; set; }

    private int _roadLayer = (1 << (int)Define.Layer.Road);

    public List<Vector3Int> FindBestPath()
    {
        List<Vector3Int> bestPath = null;
        int bestPathCost = int.MaxValue;

        Vector3Int startTile = RoadTilemap.WorldToCell(StartPos.position);

        foreach (Transform endPos in EndPosList)
        {
            Vector3Int endTile = RoadTilemap.WorldToCell(endPos.position);

            List<Vector3Int> path = FindPath(startTile, endTile);

            if (path.Count > 0 && GetPathCost(path) < bestPathCost)
            {
                bestPath = path;
                bestPathCost = GetPathCost(path);
            }
        }

        return bestPath;
    }

    // A* 알고리즘
    private List<Vector3Int> FindPath(Vector3Int startTile, Vector3Int endTile)
    {
        List<Vector3Int> openList = new List<Vector3Int>(); // 탐색할 노드 리스트
        HashSet<Vector3Int> closedList = new HashSet<Vector3Int>(); // 이미 탐색된 노드 리스트
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>(); // 어디서 왔는지 추적할 Dictionary
        Dictionary<Vector3Int, int> gCost = new Dictionary<Vector3Int, int>(); //  시작지점부터 현재 노드까지의 비용
        Dictionary<Vector3Int, int> fCost = new Dictionary<Vector3Int, int>(); //  g + h

        openList.Add(startTile);
        gCost[startTile] = 0;
        fCost[startTile] = Heuristic(startTile, endTile);

        while (openList.Count > 0)
        {
            Vector3Int currentTile = GetLowestFCostTile(openList, fCost);

            if (currentTile == endTile)
            {
                return ReconstructPath(cameFrom, currentTile);
            }

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            foreach (Vector3Int neighbor in GetNeighbors(currentTile))
            {
                if (closedList.Contains(neighbor) || !IsRoadTile(neighbor))
                {
                    continue;
                }

                int tentativeGCost = gCost[currentTile] + 1;

                if (!openList.Contains(neighbor))
                {
                    openList.Add(neighbor);
                }
                else if (tentativeGCost >= gCost[neighbor])
                {
                    continue;
                }

                cameFrom[neighbor] = currentTile;
                gCost[neighbor] = tentativeGCost;
                fCost[neighbor] = gCost[neighbor] + Heuristic(neighbor, endTile);
            }
        }

        return new List<Vector3Int>(); // 경로를 찾지 못했을 경우 빈 리스트 반환
    }

    // 인접한 타일 반환
    private List<Vector3Int> GetNeighbors(Vector3Int tile)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>
        {
            new Vector3Int(tile.x + 1, tile.y, tile.z),
            new Vector3Int(tile.x - 1, tile.y, tile.z),
            new Vector3Int(tile.x, tile.y + 1, tile.z),
            new Vector3Int(tile.x, tile.y - 1, tile.z)
        };
        return neighbors;
    }

    private List<Vector3Int> ReconstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int currentTile)
    {
        List<Vector3Int> path = new List<Vector3Int> { currentTile };
        while (cameFrom.ContainsKey(currentTile))
        {
            currentTile = cameFrom[currentTile];
            path.Insert(0, currentTile); // 경로를 거꾸로 재구성
        }
        return path;
    }

    // 가장 낮은 fCost를 가진 타일을 반환
    private Vector3Int GetLowestFCostTile(List<Vector3Int> openList, Dictionary<Vector3Int, int> fCost)
    {
        Vector3Int lowestFCostTile = openList[0];
        foreach (Vector3Int tile in openList)
        {
            if (fCost[tile] < fCost[lowestFCostTile])
            {
                lowestFCostTile = tile;
            }
        }
        return lowestFCostTile;
    }

    // 휴리스틱 함수 (맨해튼 거리)
    private int Heuristic(Vector3Int a, Vector3Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private int GetPathCost(List<Vector3Int> path)
    {
        return path.Count; // 경로 길이를 비용으로 계산
    }

    private bool IsRoadTile(Vector3Int position)
    {
        TileBase tile = RoadTilemap.GetTile(position);
        if (tile != null)
        {
            int tileLayer = RoadTilemap.gameObject.layer;

            if ((_roadLayer & (1 << tileLayer)) != 0)
            {
                return true;
            }

        }
        return false;
    }

}
