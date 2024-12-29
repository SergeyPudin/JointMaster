using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _spawnPoint;

    public void SpawnProjectile()
    {
        Projectile newProjectile = Instantiate(_projectilePrefab);
        newProjectile.transform.position = _spawnPoint.position;
    }
}