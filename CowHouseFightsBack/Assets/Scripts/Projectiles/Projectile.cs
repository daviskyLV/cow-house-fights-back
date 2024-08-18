using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected double lifetime;
}
