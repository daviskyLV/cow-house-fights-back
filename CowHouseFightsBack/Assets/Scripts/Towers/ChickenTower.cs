using UnityEngine;

public class ChickenTower : Tower
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Vector3 projectileOffset;
    
    private void Start()
    {
        OnShootProjectile += ShootProjectile;
    }

    private void ShootProjectile(GameObject target)
    {
        // Aiming towards enemy
        var ttp = target.transform.position;
        var tp = transform.position;
        var relativePos = new Vector3(ttp.x, tp.y, ttp.z) - tp; // we need to aim in -Z direction
        var rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;

        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = tp+projectileOffset;
        projectile.transform.LookAt(new Vector3(ttp.x, tp.y, ttp.z)+projectileOffset);
    }
}
