using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Weapon : MonoBehaviour
{
    [SerializeField] int numberProjectile = 10;
    [SerializeField] float fireRate = 1;
    [SerializeField] float timeReloading;
    [SerializeField] GameObject projectile;
    [SerializeField] string ownerTag;
    [SerializeField] GameObject _owner;
    [SerializeField] IDamageable owner;
    float angle;
    [Header("DEBUG")]
    [SerializeField]
    float _fireRateTime = 0;
    [SerializeField] float _timeReload = 0;
    [SerializeField] int currentProjectile = 0;

    bool allowShoot;
    private void Awake()
    {
        _fireRateTime = _timeReload = 0;
        currentProjectile = numberProjectile;
        Setup(_owner.GetComponent<IDamageable>());
    }
    public void Setup(IDamageable owner)
    {
        this.owner = owner;
        ownerTag = owner.Tag;
        allowShoot = true;
    }

    private void Update()
    {

        if (allowShoot)
        {
            if (_fireRateTime >= fireRate)
            {
                if (!owner.IsShoot) return;
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                Vector3 dir = pos - transform.position;
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Shoot();
                _fireRateTime = 0;
            }
            else
                _fireRateTime += Time.deltaTime;
        }
        else
        {
            if (!allowShoot && _timeReload <= timeReloading)
            {
                _timeReload += Time.deltaTime;
                return;
            }
            allowShoot = true;
            currentProjectile = numberProjectile;
            _timeReload = 0;
            _fireRateTime = fireRate + 1;
        }
    }
    void Shoot()
    {
        GameObject bullet = PoolingObject.Instance.SpawnFromPool(projectile, transform.position, Quaternion.Euler(Vector3.forward * angle));
        bullet.tag = ownerTag;
        bullet.SetActive(true);
        currentProjectile--;
        if (currentProjectile <= 0) allowShoot = false;
    }

}
