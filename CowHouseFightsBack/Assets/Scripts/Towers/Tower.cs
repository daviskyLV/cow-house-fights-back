using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour
{
    public event Action OnTowerDestroyed;
    /// <summary>
    /// Invoked whenever a projectile is shot, target gameobject
    /// </summary>
    protected event Action<GameObject> OnShootProjectile;
    
    [SerializeField] protected Rigidbody rb;
    [SerializeField] private double shootingInterval;

    private GameObject manualTarget;
    private GameObject automaticTarget;
    private GameObject enemyFolder;
    private bool setup = false;
    
    private double lastTimeShot = 0;
    private double lastTimeCheckedEnemies = 0;

    public void Setup(GameObject enemyFolder)
    {
        if (setup)
            return;
        
        this.enemyFolder = enemyFolder;
        setup = true;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (!setup)
            return;
        
        OnTowerDestroyed?.Invoke();
    }

    protected void Update()
    {
        if (!setup)
            return;

        var curTime = Time.timeAsDouble;
        
        if (manualTarget)
        {
            if (!(curTime >= lastTimeShot + shootingInterval)) return;
            lastTimeShot = curTime;
            OnShootProjectile?.Invoke(manualTarget);
            return;
        }

        if (automaticTarget)
        {
            if (!(curTime >= lastTimeShot + shootingInterval)) return;
            lastTimeShot = curTime;
            OnShootProjectile?.Invoke(automaticTarget);
            return;
        }
        
        // no target set, checking for a new one if can
        if (curTime < lastTimeCheckedEnemies + 1)
            return;
        
        LookForTarget();
        if (automaticTarget)
        {
            OnShootProjectile?.Invoke(automaticTarget);
            lastTimeShot = curTime;
        }
    }

    /// <summary>
    /// Set the target which will be targeted
    /// </summary>
    /// <param name="newTarget">The target, null if no target</param>
    public void SetTarget(GameObject newTarget)
    {
        manualTarget = newTarget;
        if (!newTarget)
        {
            LookForTarget();
        }
    }

    /// <summary>
    /// Set the manual target to null and look for a new automatic target
    /// </summary>
    private void LookForTarget()
    {
        manualTarget = null;
        var t = enemyFolder.transform;
        if (t.childCount == 0)
        {
            automaticTarget = null;
            return;
        }

        var chosenEnemy = Random.Range(0, t.childCount);
        automaticTarget = t.GetChild(chosenEnemy).gameObject;
    }
}
