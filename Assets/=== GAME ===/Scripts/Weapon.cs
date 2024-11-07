using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Weapon : MonoBehaviour
{
    float fireRate => data.fireRate;
    GameObject projectile => data.projectile;

    WeaponData data;

    [SerializeField] string ownerTag;
    [SerializeField] GameObject _owner;     // Setup khi nhat sung
    IDamageable owner;
    float angle;
    float _fireRateTime = 0;
    Transform shootingPoint;
    [Space]
    [SerializeField] List<WeaponData> datas;
    bool allowShoot;
    private void Awake()
    {
        Setup(_owner.GetComponent<IDamageable>());
        SelectWeapon(0);
        _fireRateTime = fireRate + 1;
    }
    public void Setup(IDamageable owner)
    {
        this.owner = owner;
        ownerTag = owner.Tag;
        allowShoot = true;
    }
    public float RemainingFireratePercent
    {
        get
        {
            if (_fireRateTime >= fireRate) return 0;
            return (float)(fireRate - _fireRateTime) / fireRate;
        }
    }

    CharacterHPBar bar;
    public void RegistWithUIBar(CharacterHPBar bar)
    {
        this.bar = bar;
    }
    private void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        Vector3 dir = pos - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
#if UNITY_EDITOR
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
#endif
        if (_fireRateTime >= fireRate)
        {
            if (!owner.IsShoot) return;
            Shoot();
            _fireRateTime = 0;
        }
        else
        {
            _fireRateTime += Time.deltaTime;
            bar.UpdateBarUI(RemainingFireratePercent);
        }
    }
    void Shoot()
    {
        GameObject bullet = PoolingObject.Instance.SpawnFromPool(projectile, shootingPoint.transform.position, Quaternion.Euler(Vector3.forward * angle));
        bullet.tag = ownerTag;
        bullet.SetActive(true);
    }
    [Button("Select Weapon")]
    public void SelectWeapon(int id)
    {
        data = datas.Find(x => x.id == id);
        shootingPoint = transform.GetChild(id).GetChild(0);
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == id)
                transform.GetChild(i).gameObject.SetActive(true);
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}

