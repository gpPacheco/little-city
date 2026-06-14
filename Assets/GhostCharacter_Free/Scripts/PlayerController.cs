using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 120f;
    public float gravity = -20f;
    public float jumpForce = 8f;
    public float flySpeed = 4f;
    public float dashSpeed = 30f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 2f;

    private Animator animator;
    private CharacterController controller;
    private float verticalVelocity = 0f;
    private bool isFlying = false;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Cooldown do dash
        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        // Ativa o dash
        if (Input.GetKeyDown(KeyCode.Q) && dashCooldownTimer <= 0 && !isDashing)
        {
            isDashing = true;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
        }

        // Durante o dash
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            Vector3 dashMove = transform.forward * dashSpeed * Time.deltaTime;
            controller.Move(dashMove);

            if (dashTimer <= 0)
                isDashing = false;

            return; // ignora o resto do Update durante o dash
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            verticalVelocity = jumpForce;
            isFlying = false;
        }

        // Voar
        if (Input.GetKey(KeyCode.Space) && !controller.isGrounded)
            isFlying = true;

        if (Input.GetKeyUp(KeyCode.Space))
            isFlying = false;

        // Gravidade ou voo
        if (isFlying)
        {
            verticalVelocity = flySpeed;
        }
        else
        {
            if (controller.isGrounded && verticalVelocity < 0)
                verticalVelocity = -1f;
            else
                verticalVelocity += gravity * Time.deltaTime;
        }

        // Movimento
        Vector3 move = transform.forward * v * currentSpeed * Time.deltaTime;
        move.y = verticalVelocity * Time.deltaTime;
        controller.Move(move);

        // Rotação
        transform.Rotate(0, h * rotationSpeed * Time.deltaTime, 0);

        // Animação
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(v));
            animator.SetBool("isRunning", isRunning && Mathf.Abs(v) > 0.1f);
        }
    }
}