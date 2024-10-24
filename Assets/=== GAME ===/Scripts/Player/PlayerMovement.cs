using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("--- CONFIG ---")]
    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;
    [SerializeField] List<CharacterData> characterDatas;
    private void Awake()
    {
        allowJump = false;
        IsGrounded = false;
        rb = GetComponent<Rigidbody2D>();
        jumpForce = characterDatas[0].jumpForce;
        moveSpeed = characterDatas[0].moveSpeed;
    }
    float xInput;
    public bool allowJump { get; protected set; }
    public bool IsGrounded { get; protected set; }

    [Header("--- CHECK GROUND ---")]
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 groundCheckOffset;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundCheckLayerMask;
    private void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        if (xInput > 0) transform.eulerAngles = Vector3.zero;
        else if (xInput < 0) transform.eulerAngles = Vector3.up * 180;
        if (Input.GetKeyDown(KeyCode.Space) && !allowJump && IsGrounded)
            allowJump = true;
    }
    Collider2D[] cols;

    public Vector2 Velocity { get => rb.velocity; set => rb.velocity = value; }

    public Vector2 Position { get => rb.position; set => rb.position = value; }
    private void FixedUpdate()
    {
        Vector2 velo = rb.velocity;
        velo.x = moveSpeed * xInput;
        if (allowJump)
        {
            velo.y = jumpForce;
            allowJump = false;
        }
        rb.velocity = velo;


        // Check Ground

        cols = Physics2D.OverlapCircleAll((Vector2)(groundCheck ? groundCheck.position : transform.position) + groundCheckOffset, groundCheckRadius, groundCheckLayerMask);

        if (cols.Length > 0) IsGrounded = true;
        else IsGrounded = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((groundCheck ? groundCheck.position : transform.position) + new Vector3(groundCheckOffset.x, groundCheckOffset.y), groundCheckRadius);
    }
}
