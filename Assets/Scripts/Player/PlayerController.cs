using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;

    [Header("Dash")]
    public float dashSpeed = 12f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.2f;
    private bool isDashing;
    private float dashTime;
    private float lastDashTime = -999f;

    [Header("Jump (Horizontal Hop)")]
    public float jumpDistance = 3f;
    public float jumpDuration = 0.3f;
    public float jumpCooldown = 1f;
    private bool isJumping;
    private float jumpTime;
    private float lastJumpTime = -999f;
    private float jumpStartX;
    private float jumpTargetX;
    private int jumpDirection = 1;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDashing && !isJumping)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }
        else
        {
            moveInput = Vector2.zero;
        }

        if (Input.GetButtonDown("Dash") && Time.time >= lastDashTime + dashCooldown && !isDashing)
        {
            StartDash();
        }

        if (Input.GetButtonDown("Jump") && Time.time >= lastJumpTime + jumpCooldown && !isJumping && !isDashing)
        {
            if (Mathf.Abs(moveInput.x) > 0.1f)
            {
                StartJump(moveInput.x > 0 ? 1 : -1);
            }
            else
            {
                StartJump(jumpDirection);
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = moveInput * dashSpeed;
            dashTime -= Time.fixedDeltaTime;
            if (dashTime <= 0f)
            {
                isDashing = false;
                rb.linearVelocity = Vector2.zero;
            }
        }
        else if (isJumping)
        {
            jumpTime += Time.fixedDeltaTime;
            float t = jumpTime / jumpDuration;
            t = Mathf.Clamp01(t);

            float newX = Mathf.Lerp(jumpStartX, jumpTargetX, t);
            Vector2 pos = rb.position;
            pos.x = newX;
            rb.MovePosition(pos);

            if (t >= 1f)
            {
                isJumping = false;
                lastJumpTime = Time.time;
            }
        }
        else
        {
            rb.linearVelocity = moveInput * moveSpeed;

            if (Mathf.Abs(moveInput.x) > 0.1f)
                jumpDirection = moveInput.x > 0 ? 1 : -1;
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        lastDashTime = Time.time;
    }

    void StartJump(int direction)
    {
        isJumping = true;
        jumpTime = 0f;
        jumpStartX = rb.position.x;
        jumpTargetX = jumpStartX + direction * jumpDistance;
        jumpDirection = direction;
        rb.linearVelocity = Vector2.zero;
    }
}
