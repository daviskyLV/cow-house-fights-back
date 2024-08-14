using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    [SerializeField]
    [Tooltip("Degrees per second")]
    private float angularSpeed;
    [SerializeField] private GameObject healthbarGO;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private NavMeshAgent agent;
    
    private float health;
    
    private NavMeshPath navPath;
    private int targetPathpoint = 0;
    private Transform alternativeTarget;
    
    private Transform towersGO;
    private PlayfieldController playfield;
    private Transform barn;

    private bool setup = false;
    
    // Start is called before the first frame update
    void Start()
    {
        health = Random.Range(0.0f, maxHealth);
        healthbarGO.GetComponent<Healthbar>().UpdateHealthValue(health/maxHealth);
        TowerController.OnTowerPlaced += RecheckPath;
        TowerController.OnTowerDestroyed += RecheckPath;
    }

    private void FixedUpdate()
    {
        if (!setup)
            return;
        
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        // Checking alternative target scenarios
        if (alternativeTarget)
        {
            var targetSameY = new Vector3(alternativeTarget.position.x, rb.position.y, alternativeTarget.position.z);
            var distToTarget = Vector3.Distance(rb.position, targetSameY);
            
            if (distToTarget < agent.radius)
            {
                Debug.Log($"Alternative target {alternativeTarget.name} at {alternativeTarget.position} reached!");
                return;
            }
            
            // Moving towards alternative target
            var timeRequired = distToTarget / speed;
            rb.MovePosition(Vector3.Lerp(rb.position, targetSameY, Time.fixedDeltaTime / timeRequired));
            return;
        }
        
        // Moving towards current point
        var curPoint = navPath.corners[targetPathpoint];
        var curPointSameY = new Vector3(curPoint.x, rb.position.y, curPoint.z);
        var distToCurPoint = Vector3.Distance(rb.position, curPointSameY);

        if (distToCurPoint < agent.radius)
        {
            // reached next point
            if (navPath.corners.Length == targetPathpoint + 1)
            {
                // last point, all done
                return;
            }

            targetPathpoint++;
            return;
        }
        
        // Moving towards next point
        var nextPointTime = distToCurPoint / speed;
        rb.MovePosition(Vector3.Lerp(rb.position, curPointSameY, Time.fixedDeltaTime / nextPointTime));
    }

    private void RecheckPath()
    {
        if (!setup)
            return;
            
        navPath = playfield.CalculateNavigationPath(transform.position, barn.position, agent.agentTypeID, navPath);
        if (navPath.corners.Length == 0)
        {
            // Cant get to barn
            LookForAlternative();
        }
        else
        {
            // Can get to barn, ignore towers and follow the path
            alternativeTarget = null;
            targetPathpoint = 0;
        }
    }

    /// <summary>
    /// Sets the alternative target
    /// </summary>
    private void LookForAlternative()
    {
        if (towersGO.childCount == 0)
        {
            // No towers? Try to brute force the way to barn
            alternativeTarget = barn;
            return;
        }
        
        // Picking closest tower
        alternativeTarget = barn;
        var closestDist = float.MaxValue;
        for (int i = 0; i < towersGO.childCount; i++)
        {
            var alt = towersGO.GetChild(i);
            var dist = Vector3.Distance(alternativeTarget.position, alt.position);
            
            if (!(dist < closestDist)) continue;
            alternativeTarget = alt;
            closestDist = dist;
        }
    }

    /// <summary>
    /// Set up the enemy so that it can start stealing food
    /// </summary>
    /// <param name="towersGO">The towers collection Transform</param>
    /// <param name="playfield">Playfield controller which manages nav meshes</param>
    /// <param name="barn">The end goal (position) of the enemy</param>
    public void Setup(Transform towersGO, PlayfieldController playfield, Transform barn)
    {
        if (setup)
            return;

        this.towersGO = towersGO;
        this.playfield = playfield;
        this.barn = barn;
        setup = true;
        
        healthbarGO.GetComponent<Billboard>().ChangePlayersCamera(Camera.main);
        RecheckPath();
    }
}
