using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementControl : MonoBehaviour
{
    [Header("---CONFIG---:")]
    [SerializeField] float groundCheckerRadius;
    [SerializeField] Vector2 groundCheckerOffset;

    [Space]
    [Header("REFERRENCES:")]
    [SerializeField]
    private Rigidbody2D rb;

    public float moveSpeed;
    public float jumpForce;

    public bool allowJump /*{ get; protected set; }*/;

    public bool hasRigidbody { get { return rb; } }
    Vector2 m;  // movement
    public Vector2 movement { get { return m; } }
    public Vector2 velocity
    {
        get
        {
            Vector2 final = rb ? rb.velocity : Vector2.zero;
            return final;
        }
        set
        {
            if (rb) rb.velocity = value;
        }
    }
    public Vector2 position
    {
        get
        {
            Vector2 final = rb ? rb.position : Vector2.zero;
            return final;
        }
        set
        {
            if (rb) rb.position = value;
        }
    }
    public bool isGrounded;

    // Internal:
    float inputX;
    PlayerInput input;
    Player player;
    [SerializeField] GameObject landing;
    void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        inputX = input.Horizontal;
        if (inputX > 0) transform.localEulerAngles = Vector3.zero;
        else if (inputX < 0) transform.localEulerAngles = Vector3.up * 180;
        m.x = isGrounded ? inputX : inputX != 0 ? inputX : movement.x;
        if (!isGrounded)
        {
            m.x = Mathf.MoveTowards(movement.x, 0, Time.deltaTime / 5);     // reduce movement in air
        }

        // Check if grounded:
        allowJump = true;
        Collider2D[] cols = Physics2D.OverlapCircleAll(groundCheckerOffset + new Vector2(transform.position.x, transform.position.y), groundCheckerRadius);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].CompareTag("JumpPad"))
            {
                allowJump = false;
            }
            if (cols[i].gameObject != gameObject && !cols[i].isTrigger && !cols[i].CompareTag("Portal"))
            {
                if (!isGrounded)
                {
                    PoolingObject.Instance.SpawnFromPool(landing, transform.position, Quaternion.identity);
                    isGrounded = true;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (rb)
        {
            Vector2 veloc = rb.velocity;
            veloc.x = movement.x * (moveSpeed / 10);
            rb.velocity = veloc;
            if (rb.velocity != Vector2.zero && isGrounded)
                player.currentAnims.PlayAnimMove();
            else
                player.currentAnims.PlayAnimIdle();
        }
        if (input.IsJump && isGrounded && allowJump)
            Jump();
    }

    // For local movement controlling: 
    public void InputMovement(float x)
    {
        // Movement input:
        inputX = x;
    }

    public void Jump()
    {
        if (!rb) return;

        Vector2 veloc = rb.velocity;
        veloc.y = jumpForce;
        rb.velocity = veloc;

        // Don't allow jumping right after a jump:
        allowJump = false;
        isGrounded = false;
    }

    public void DestroyRigidbody()
    {
        Destroy(this);
        Destroy(rb);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + new Vector3(groundCheckerOffset.x, groundCheckerOffset.y), groundCheckerRadius);
    }
}
