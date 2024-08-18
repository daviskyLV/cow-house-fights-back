using UnityEngine;

public abstract class EnemyAnimation : MonoBehaviour
{
    public abstract void SetTarget(Vector3 position);
    public abstract void SetTarget(Transform target);
}
