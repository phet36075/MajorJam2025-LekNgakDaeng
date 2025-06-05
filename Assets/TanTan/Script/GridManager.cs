using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;
using UnityEngine.UIElements;
using Unity.Mathematics;
using System.Collections.Generic;
using UnityEngine.AI;
using NavMeshPlus.Components;

public class GridManager : MonoBehaviour
{
    public List<CoordScript> coord;
    [SerializeField] NavMeshSurface[] navMesh;

    [Header("Grid Size")]
    [SerializeField] int gridRow;
    [SerializeField] int gridColumn;

    [Header("Grid Cell Size")]
    public float gridWidth;
    public float gridHeight;

    [Header("Grid Setup")]
    [SerializeField] Vector2 offset;

    [Header("Preview")]
    [SerializeField] bool showGrid = true;
    [SerializeField] bool centerGrid = true;

    [Header("Cell Snap")]
    [SerializeField] GameObject cellPrefab;
    
    [Header("Misc.")]
    [SerializeField] Color gridColor;

    private void Awake()
    {
        coord = new List<CoordScript>();

        DrawGrid();
    }

    private void Start()
    {
        navMesh = FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None);

        foreach (NavMeshSurface surface in navMesh)
        {
            surface.BuildNavMesh();
        }
    }

    void DrawGrid()
    {
        if (cellPrefab == null)
        {
            Debug.LogError("Cell Prefab not assigned!");
            return;
        }

        float cellSizeX = Mathf.Abs(gridWidth);
        float cellSizeY = Mathf.Abs(gridHeight);

        Vector3 origin = transform.position;

        if (centerGrid)
        {
            origin.x -= (gridColumn * cellSizeX) / 2f;
            origin.y -= (gridRow * cellSizeY) / 2f;
        }

        origin += (Vector3)offset;

        for (int x = 0; x < gridColumn; x++)
        {
            for (int y = 0; y < gridRow; y++)
            {
                Vector3 cellCenter = origin + new Vector3(x * cellSizeX + cellSizeX / 2f, y * cellSizeY + cellSizeY / 2f, 0f);
                GameObject gm = Instantiate(cellPrefab, cellCenter, Quaternion.identity, transform);
                CoordScript cs = gm.GetComponent<CoordScript>();
                coord.Add(cs);
                cs.SetCoord(x, y);
                gm.name = $"[{x},{y}]";
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (!showGrid) return;

        float cellWidth = Mathf.Abs(gridWidth);
        float cellHeight = Mathf.Abs(gridHeight);

        Gizmos.color = gridColor;
        Vector3 origin = transform.position;

        if (centerGrid)
        {
            origin.x -= (gridColumn * cellWidth) / 2f;
            origin.y -= (gridRow * cellHeight) / 2f;
        }

        origin += (Vector3)offset;

        for (int x = 0; x <= gridColumn; x++)
        {
            Vector3 start = origin + new Vector3(x * cellWidth, 0f, 0f);
            Vector3 end = start + new Vector3(0f, gridRow * cellHeight, 0f);
            Gizmos.DrawLine(start, end);
        }

        for (int y = 0; y <= gridRow; y++)
        {
            Vector3 start = origin + new Vector3(0f, y * cellHeight, 0f);
            Vector3 end = start + new Vector3(gridColumn * cellWidth, 0f, 0f);
            Gizmos.DrawLine(start, end);
        }

    }
}
