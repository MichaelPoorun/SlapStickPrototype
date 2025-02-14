using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float strafeSpeed;
    public float jumpForce;

    public Rigidbody hips;
    public bool isGrounded;
    private float jumpCharge = 0f;
    private float maxChargeTime = 2f;
    private float baseJumpForce = 5f;
    private float jumpForceMultiplier = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hips = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            jumpCharge += Time.deltaTime;
            jumpCharge = Mathf.Clamp(jumpCharge, 0f, maxChargeTime);
        }

        if (Input.GetButtonUp("Jump") && isGrounded)
        {
            float finalJumpForce = baseJumpForce + (jumpCharge / maxChargeTime) * jumpForce * jumpForceMultiplier;
            hips.AddForce(new Vector3(0, finalJumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
            jumpCharge = 0f;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hips.AddForce(hips.transform.forward * speed * 1.5f);
            }
            else
            {
                hips.AddForce(hips.transform.forward * speed);
            }

        }

        if (Input.GetKey(KeyCode.S))
        {
            hips.AddForce(-hips.transform.right * strafeSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            hips.AddForce(-hips.transform.forward * speed);
        }

        if (Input.GetKey(KeyCode.W))
        {
            hips.AddForce(hips.transform.right * strafeSpeed);
        }
    }
}
