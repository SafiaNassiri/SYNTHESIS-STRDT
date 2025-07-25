using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float dashSpeed = 12f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    [Header("Health")]
    public int maxHealth = 5;
    private int currentHealth;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 moveInput;
    private bool isDashing = false;
    private float dashTime;
    private float lastDashTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        HandleInput();
        HandleAnimations();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastDashTime + dashCooldown)
        {
            StartDash();
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = moveInput.normalized * dashSpeed;
            if (Time.time >= dashTime)
            {
                isDashing = false;
            }
        }
        else
        {
            rb.linearVelocity = moveInput.normalized * moveSpeed;
        }
    }

    void HandleInput()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void StartDash()
    {
        isDashing = true;
        dashTime = Time.time + dashDuration;
        lastDashTime = Time.time;
    }

    void HandleAnimations()
    {
        if (animator != null)
        {
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
            animator.SetFloat("Speed", moveInput.sqrMagnitude);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (animator != null)
            animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        rb.linearVelocity = Vector2.zero;
        if (animator != null)
            animator.SetTrigger("Die");

        // Disable input or reload scene
        this.enabled = false;
    }
}
