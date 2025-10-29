using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletShooter : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootCooldown;
    [SerializeField] private Transform _target;

    private void Start()
    {
        StartCoroutine(ShootingRoutine());
    }

    private IEnumerator ShootingRoutine()
    {
        var waitForSeconds = new WaitForSeconds(_shootCooldown);

        while (enabled)
        {
            var direction = (_target.position - transform.position).normalized;
            var newBullet = Instantiate(_bulletPrefab, transform.position + direction, Quaternion.identity);

            if (newBullet.TryGetComponent<Rigidbody>(out var bulletRigidbody))
            {
                bulletRigidbody.transform.up = direction;
                bulletRigidbody.velocity = direction * _bulletSpeed;
            }

            yield return waitForSeconds;
        }
    }
}