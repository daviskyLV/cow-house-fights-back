using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class PlayfieldController : MonoBehaviour
{
    private NavMeshSurface[] surfaces;

    // Start is called before the first frame update
    void Awake()
    {
        surfaces = GetComponents<NavMeshSurface>();
        GameController.OnGameStarted += ClearSurfacesNavmesh;
        TowerController.OnTowerPlaced += RecalculateNavmeshes;
        TowerController.OnTowerDestroyed += RecalculateNavmeshes;
    }

    private void Start()
    {
        // Sorting surfaces based on agent type id
        Array.Sort(surfaces, SurfaceAgentIdComparer);
        int SurfaceAgentIdComparer(NavMeshSurface x, NavMeshSurface y)
        {
            if (x.agentTypeID > y.agentTypeID)
                return -1;
            if (x.agentTypeID < y.agentTypeID)
                return 1;

            return 0;
        }
        
        //ClearSurfacesNavmesh();
        RecalculateNavmeshes();
    }

    private void ClearSurfacesNavmesh()
    {
        foreach (var s in surfaces)
        {
            s.navMeshData = null;
        }
    }

    private void RecalculateNavmeshes() 
    {
        foreach (var s in surfaces)
        {
            s.BuildNavMesh();
        }
    }

    /// <summary>
    /// Returns a navigation path for the agent to go through from start to destination
    /// </summary>
    /// <param name="start">Start position</param>
    /// <param name="destination">Destination position</param>
    /// <param name="agentTypeId">Agent type id</param>
    /// <param name="previousPath"></param>
    /// <returns></returns>
    public NavMeshPath CalculateNavigationPath(Vector3 start, Vector3 destination, int agentTypeId, NavMeshPath previousPath = null)
    {
        previousPath ??= new NavMeshPath();
        
        var agentSurface = surfaces[agentTypeId];
        var filter = new NavMeshQueryFilter
        {
            areaMask = agentSurface.layerMask,
            agentTypeID = agentTypeId
        };
        NavMesh.CalculatePath(start, destination, filter, previousPath);
        
        return previousPath;
    }
}
