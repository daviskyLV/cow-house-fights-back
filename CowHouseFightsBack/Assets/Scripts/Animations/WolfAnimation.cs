using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WolfAnimation : EnemyAnimation
{
    [SerializeField] private GameObject targetGO;
    [SerializeField] private MultiAimConstraint neckConstraint;
    [SerializeField] private MultiAimConstraint headConstraint;

    private Transform target;
    private Vector3 targetPosition;

    private void Update()
    {
        if (target)
        {
            targetGO.transform.position = target.position;
            return;
        }
        
        if (targetPosition == null)
            return;

        targetGO.transform.position = targetPosition;
    }

    public override void SetTarget(Vector3 position)
    {
        targetPosition = position;
    }

    public override void SetTarget(Transform target)
    {
        this.target = target;
    }
}
