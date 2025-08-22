using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{

    public int hp = 10;
    public Vector3 pos;
    Rigidbody rb;
    public float speed = 10f;
    public float jumpforce = 7f;
    public Vector3 wallNormal;
    public float wallPushForse = 2f;
    private bool isGrounded = true;
    private bool isWall = false;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private float wallJumpLockTime = 0.2f;
    private float wallJumpTimer = 0f;
    private bool isWallJumping = false;

    private void Start()
    {
        pos = this.transform.position;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    //public float Get_PosX()
    //{
    //    return pos[1];
    //}
    //public float Get_PosY()
    //{
    //   return pos[2];
    //}
    void Update()
    {
        if (rb.linearVelocity.y < 0 || isWall)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
                isGrounded = false;
            }
            else if (isWall)
            {
                WallJump();
            }

        }
    }
    void WallJump()
    {
        Vector3 velosity = rb.linearVelocity;
        velosity.y = 0;
        velosity.x = 0f;
        velosity.z = 0f;
        rb.linearVelocity = velosity;

        Vector3 jumpDirection = Vector3.up + wallNormal;
        rb.AddForce(Vector3.up * jumpforce + jumpDirection.normalized * wallPushForse, ForceMode.Impulse);

        wallJumpTimer = wallJumpLockTime;
        isWallJumping = true;
    }

    void FixedUpdate()
    {
        if (isWallJumping)
        {
            wallJumpTimer -= Time.deltaTime;
            if (wallJumpTimer <= 0f)
            {
                isWallJumping = false;
            }
        }

        if (!isWallJumping)
        {
            float moveX = Input.GetAxis("Horizontal");

            Vector3 move = new Vector3(moveX, 0f, 0f);
            Vector3 newPos = transform.position += move * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
        }
        //if (rb.linearVelocity.y < 0)
        //{
        //    rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        //}
        //else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        //{
        //    rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        //}

        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //{
        //    rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        //    isGrounded = false;
        //}
    }
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Vector3 normal = contact.normal;

            // Вертикальное столкновение (внизу)
            if (Vector3.Angle(normal, Vector3.up) < 45f)
            {
                isGrounded = true;
                isWall = false;
                Debug.Log("Ground detected");
            }
            // Горизонтальное столкновение (вбок)
            else if (Vector3.Angle(normal, Vector3.up) > 45f && Vector3.Angle(normal, Vector3.up) < 135f)
            {
                isWall = true;
                wallNormal = contact.normal;
                Debug.Log("Wall detected");
            }
            // Столкновение сверху (например, потолок)
            else if (Vector3.Angle(normal, Vector3.down) < 45f)
            {
                Debug.Log("Ceiling detected");
            }
        }
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    isGrounded = true;
        //}

    }
}
