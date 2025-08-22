using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public GameObject player;

    float horizontalInput;
    float movSpeed = 7f;
    float jmpPower = 5f;

    bool IsFacingRight = true;
    //bool IsJumping = false;

    Rigidbody rb;
    Animator animator;

    private bool IsGrounded = true;

    float grCheckRadius;

    public LayerMask groundMask;

    public Transform lastSavePoint;

    public Transform wpB;

    public Transform cellingCheck;
    public float cellingRad = 0.11f;
    bool isCelling;
    int Jumps;

    //private void Awake()
    //{
    //    DontDestroyOnLoad(player);
    //}
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = /*RigidbodyConstraints.FreezePositionZ | */RigidbodyConstraints.FreezeRotation;
        animator = GetComponent<Animator>();
        if (lastSavePoint != null)
        {
            transform.position = new Vector3(lastSavePoint.position.x, lastSavePoint.position.y + 0.5f, lastSavePoint.position.z);
        }
        Jumps = 0;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        isCelling = Physics.CheckSphere(cellingCheck.position, cellingRad, groundMask);

        FlipSprite();

        if(Input.GetButtonDown("Jump") && Jumps != 2)
        {
             rb.linearVelocity = new Vector3(horizontalInput * movSpeed, jmpPower);
             IsGrounded = false;
             animator.SetBool("isJumping", !IsGrounded);
             Jumps++;
             return;
        }
        if (isCelling && rb.linearVelocity.y >= 0)
        {
            rb.linearVelocity = new Vector3(horizontalInput * movSpeed, -rb.linearVelocity.y);
            Debug.Log("is celling");
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(horizontalInput*movSpeed, rb.linearVelocity.y, 0);
        animator.SetFloat("xVelosity", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    void FlipSprite()
    {
        if(IsFacingRight && horizontalInput < 0f || !IsFacingRight && horizontalInput > 0f)
        {
            IsFacingRight = !IsFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
    public void SetLastSavePoint(Transform savepoint)
    {
        lastSavePoint = savepoint;
    }
    private void OnCollisionEnter(Collision collision)
    {
        IsGrounded = true;
        animator.SetBool("isJumping", !IsGrounded);
        Jumps = 0;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    IsGrounded = true;
    //    animator.SetBool("isJumping", !IsGrounded);
    //}
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(cellingCheck.position, cellingRad);
    }
}

    //public float moveSpeed = 6f;
    //public float jumpForce = 4f;
    //public float gravity = -40f;

    //private CharacterController controller;
    //private Vector3 velocity;
    //private bool isGrounded;
    //private bool isCelling;
    //public Vector3 wallCheckOffset = new Vector3(0.5f, 0.5f, 0);
    //public Vector3 groundCheckOffset = new Vector3(0, 0.5f, 0);

    //public Vector3 startCharacterPosition;

    //public Transform groundCheck;
    //public Transform cellCheck;
    //public float groundDistance = 0.5f;
    ////public float ceilingDistance = 0.
    //public LayerMask groundMask;

    //void Start()
    //{
    //    controller = GetComponent<CharacterController>();
    //    startCharacterPosition = transform.position;
    //}

    //void Update()
    //{
    //    float x = Input.GetAxis("Horizontal");
    //    // Проверка на землю
    //    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    //    isCelling = Physics.CheckSphere(cellCheck.position, groundDistance, groundMask);

    //    Debug.DrawLine(groundCheck.position, groundCheck.position + Vector3.up * 0.1f, Color.green);

    //    if (isGrounded && velocity.y < 0)
    //    {
    //        Debug.Log("Grounded");
    //        velocity.y = -2f;
    //    }
    //    if (isCelling/* && velocity.y < 0*/)
    //    {
    //        Debug.Log("Celling");
    //        velocity.y = -4f;
    //    }
        
    //    if(transform.position.y < 0f)
    //    {
    //        transform.position = startCharacterPosition;
    //        Debug.Log("Return Character");
    //    }
    //    // Ввод
        

    //    // Движение по X (Z фиксируем)
    //    Vector3 move = new Vector3(x, 0f, 0f);
    //    controller.Move(move * moveSpeed * Time.deltaTime);

    //    // Прыжок
    //    if (isGrounded && Input.GetButtonDown("Jump"))
    //            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    //    // Гравитация
    //    velocity.y += gravity * Time.deltaTime;
    //    controller.Move(velocity * Time.deltaTime);
    //}
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    if (groundCheck != null)
    //        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);

    //    Gizmos.color = Color.yellow;
    //    if (cellCheck != null)
    //        Gizmos.DrawWireSphere(cellCheck.position, groundDistance);

    //    //// Визуализация динамической точки wallCheck
    //    //Gizmos.color = Color.red;
    //    //if (Application.isPlaying)
    //    //{
    //    //    float x = Input.GetAxisRaw("Horizontal");
    //    //    Vector3 dynamicWallCheckPos = transform.position + transform.TransformDirection(wallCheckOffset * Mathf.Sign(x != 0 ? x : 1));
    //    //    Gizmos.DrawWireSphere(dynamicWallCheckPos, checkRadius);
    //    //}
    //    //else
    //    //{
    //    //    // Показываем справа по умолчанию в редакторе
    //    //    Vector3 defaultWallCheckPos = transform.position + transform.TransformDirection(wallCheckOffset);
    //    //    Gizmos.DrawWireSphere(defaultWallCheckPos, checkRadius);
    //    //}
    //}

    //public float moveSpeed = 6f;
    //public float jumpForce = 4f;
    //public float gravity = -20f;
    //public Transform groundCheck;
    //public Transform wallCheck;
    //public Transform ceilingCheck;
    //public float checkRadius = 0.3f;
    //public LayerMask groundMask;

    //private CharacterController controller;
    //private Vector3 velocity;
    //private bool isGrounded;
    //private bool isTouchingWall;
    //private bool isUnderCeiling;

    //public Vector3 wallCheckOffset = new Vector3(0.5f, 0.5f, 0);

    //void Start()
    //{
    //    controller = GetComponent<CharacterController>();
    //}

    //void Update()
    //{
    //    float x = Input.GetAxis("Horizontal");

    //    // Проверка на землю
    //    isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundMask);
    //    Vector3 wallCheckPos = transform.position + transform.TransformDirection(wallCheckOffset * Mathf.Sign(x));
    //    isTouchingWall = Physics.CheckSphere(wallCheck.position, checkRadius, groundMask);
    //    isUnderCeiling = Physics.CheckSphere(ceilingCheck.position, checkRadius, groundMask);

    //    if (isGrounded && velocity.y < 0)
    //    {
    //        velocity.y = -2f; // прижатие к земле
    //    }

    //    // Получаем движение по горизонтали
    //    Vector3 move = transform.right * x;

    //    // Применяем движение
    //    controller.Move(move * moveSpeed * Time.deltaTime);

    //    // Прыжок
    //    if (isGrounded && Input.GetButtonDown("Jump"))
    //    {
    //        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    //    }
    //    else if (!isGrounded && isTouchingWall && Input.GetButtonDown("Jump"))
    //    {
    //        // Убираем вертикальную скорость
    //        velocity.y = 0f;

    //        // Прыжок вверх и от стены
    //        Vector3 wallJumpDir = Vector3.up * jumpForce + -transform.right * x; // от стены
    //        velocity = wallJumpDir;
    //    }
    //    if (isUnderCeiling && velocity.y > 0)
    //    {
    //        velocity.y = 0f;
    //    }

    //    // Применяем гравитацию
    //    velocity.y += gravity * Time.deltaTime;
    //    controller.Move(velocity * Time.deltaTime);
    //}

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.green;
    //    if (groundCheck != null)
    //        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);

    //    Gizmos.color = Color.red;
    //    if (wallCheck != null)
    //        Gizmos.DrawWireSphere(wallCheck.position, checkRadius);

    //    Gizmos.color = Color.yellow;
    //    if (ceilingCheck != null)
    //        Gizmos.DrawWireSphere(ceilingCheck.position, checkRadius);
    //}
    //public float groundDistance;

    //public LayerMask terrainLayer;
    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //public Vector3 pos;
    //Rigidbody rb;
    //public float speed = 6f;
    //public float jumpforce = 7f;
    //public Vector3 wallNormal;
    //public float wallPushForse = 6f;
    //private bool isGrounded = true;
    //private bool isWall = false;
    //public float fallMultiplier = 4f;
    //public float lowJumpMultiplier = 2f;

    //private float wallJumpLockTime = 0.2f;
    //private float wallJumpTimer = 0f;
    //private bool isWallJumping = false;

    //private void Start()
    //{
    //    pos = this.transform.position;
    //    rb = GetComponent<Rigidbody>();
    //    rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    //}

    //void Update()
    //{
    //    if (rb.linearVelocity.y < 0 || isWall)
    //    {
    //        rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    //    }
    //    else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
    //    {
    //        rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    //    }

    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (isGrounded)
    //        {
    //            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
    //            isGrounded = false;
    //        }
    //        else if (isWall)
    //        {
    //            WallJump();
    //        }

    //    }
    //}
    //void WallJump()
    //{
    //    Vector3 velosity = rb.linearVelocity;
    //    velosity.y = 0;
    //    velosity.x = 0f;
    //    velosity.z = 0f;
    //    rb.linearVelocity = velosity;

    //    Vector3 jumpDirection = Vector3.up + wallNormal;
    //    rb.AddForce(Vector3.up * jumpforce + jumpDirection.normalized * wallPushForse, ForceMode.Impulse);

    //    wallJumpTimer = wallJumpLockTime;
    //    isWallJumping = true;
    //    isWall = false;
    //}

    //void FixedUpdate()
    //{
    //    if (isWallJumping)
    //    {
    //        wallJumpTimer -= Time.deltaTime;
    //        if (wallJumpTimer <= 0f)
    //        {
    //            isWallJumping = false;
    //        }
    //    }
    //    if (!isWallJumping)
    //    {
    //        float moveX = Input.GetAxis("Horizontal");

    //        Vector3 move = new Vector3(moveX, 0f, 0f);
    //        Vector3 newPos = transform.position += move * speed * Time.fixedDeltaTime;
    //        rb.MovePosition(newPos);
    //    } 
    //}
    //void OnCollisionEnter(Collision collision)
    //{
    //    foreach (ContactPoint contact in collision.contacts)
    //    {
    //        Vector3 normal = contact.normal;

    //        // Вертикальное столкновение (внизу)
    //        if (Vector3.Angle(normal, Vector3.up) < 45f)
    //        {
    //            Debug.Log("Ground detected");
    //        }
    //        // Горизонтальное столкновение (вбок)
    //        else if (Vector3.Angle(normal, Vector3.up) > 45f && Vector3.Angle(normal, Vector3.up) < 135f)
    //        {
    //            Debug.Log("Wall detected");
    //        }
    //        // Столкновение сверху (например, потолок)
    //        else if (Vector3.Angle(normal, Vector3.down) < 45f)
    //        {
    //            Debug.Log("Ceiling detected");
    //        }
    //    }
    //}

