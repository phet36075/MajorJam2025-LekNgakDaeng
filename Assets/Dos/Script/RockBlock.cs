using UnityEngine;
using NavMeshPlus.Components;

public class RockBlock : MonoBehaviour
{
    public NavMeshSurface[] navSurface => FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None);
    public NavMeshModifier mod => GetComponent<NavMeshModifier>();

    public void RemoveRock()
    {
        mod.area = 0;
        Destroy(gameObject,0.25f);
        foreach (NavMeshSurface surface in navSurface)
        {
            surface.BuildNavMesh();
        }
    }
    public void AddRock()
    {
        foreach (NavMeshSurface surface in navSurface)
        {
            surface.BuildNavMesh();
        }
    }
}
