using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float acceleration;
    [SerializeField] float hitRadius;
    [SerializeField] float lifeTime;
    [SerializeField] GameObject hitVfx;
    [Space(5)] int damage = 5;
    RaycastHit2D hit;
    float lt;
    private void Start()
    {
        RefreshlastPos();
        lt = 0;
    }
    int xDir = 1;
    private void Update()
    {
        RefreshlastPos();
        CheckHit();
        float curSpeed = xDir * speed;
        curSpeed += acceleration * Time.deltaTime;
        transform.Translate(transform.right * curSpeed * Time.deltaTime, Space.World);

        // Life time projectile
        if (lifeTime > 0)
        {
            if (lt < lifeTime)
            {
                lt += Time.deltaTime;
            }
            else
            {
                // Destroy bullet
                lt = 0;
                DestroyProjectile();
            }
        }
    }
    public void Setup(int dir)
    {
        xDir = dir;
    }
    void CheckHit()
    {
        hit = Physics2D.CircleCast(transform.position, hitRadius,
            new Vector2(transform.position.x, transform.position.y) - lastPos, 2);
        if (hit)
        {
            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log("Contact ground!");
                DestroyProjectile();
            }
            if (hit.collider.TryGetComponent(out IDamageable component))
            {
                if (component != null && !hit.collider.CompareTag(tag))
                {
                    component.TakeDamage(damage);
                    DestroyProjectile();
                }
            }
        }
    }
    void DestroyProjectile()
    {
        // SFX

        // VFX
        Vector3 euler = new Vector3(0, transform.localEulerAngles.y*xDir, transform.localEulerAngles.z);
        GameObject _h = PoolingObject.Instance.SpawnFromPool(hitVfx, transform.position, Quaternion.identity);
        _h.transform.localEulerAngles = euler;
        gameObject.SetActive(false);
    }
    Vector2 lastPos;
    public void RefreshlastPos()
    {
        lastPos = transform.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
    }
}
