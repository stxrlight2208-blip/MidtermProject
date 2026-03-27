using UnityEngine;

public class FPSController : MonoBehaviour

{
    [Header("Movement")]
    public float speed = 6f;
    public float jumpForce = 5f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 100f;
    public Transform cameraHolder;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    float xRotation = 0f;
    Rigidbody rb;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // ล็อกเมาส์
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Look();
        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 100;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 100;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;
        rb.AddForce(move * speed, ForceMode.Acceleration);
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}