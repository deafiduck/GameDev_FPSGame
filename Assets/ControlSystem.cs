using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    public float moveSpeed = 5f;  // Karakterin hareket h�z�
    public float rotationSpeed = 720f;  // Karakterin d�n�� h�z�
    public float jumpForce = 5f;  // Karakterin z�plama kuvveti
    public LayerMask groundLayers;  // Z�plama kontrol� i�in zemin katmanlar�
    public Transform groundCheck;  // Zemin kontrol� i�in nokta
    public float groundCheckRadius = 0.1f;  // Zemin kontrol� i�in yar��ap

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // Karakterin fiziksel olarak d�nmesini engelle
    }

    void Update()
    {
        Move();
        Rotate();
        Jump();
    }

    void Move()
    {
        float moveDirection = Input.GetAxis("Vertical");  // W/S veya Up/Down ok tu�lar�
        Vector3 move = transform.forward * moveDirection * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);
    }

    void Rotate()
    {
        float turnDirection = Input.GetAxis("Horizontal");  // A/D veya Left/Right ok tu�lar�
        float turn = turnDirection * rotationSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayers);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
