using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Projectile : MonoBehaviour
{
    public float speed;
    [SerializeField] float hitRadius;
    RaycastHit2D hit;
    private void Start()
    {
        RefreshlastPos();
    }
    int xDir = 1;
    private void Update()
    {
        //RefreshlastPos();
        //CheckHit();

        float curSpeed = xDir * speed;
        transform.Translate(transform.right*curSpeed*Time.deltaTime,Space.World);
    }
    void CheckHit()
    {
        hit =Physics2D.CircleCast(transform.position, hitRadius,
            new Vector2(transform.position.x, transform.position.y)-lastPos,2);
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
