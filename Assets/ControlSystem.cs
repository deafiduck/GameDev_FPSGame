using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    public float moveSpeed = 5f;  // Karakterin hareket hýzý
    public float rotationSpeed = 720f;  // Karakterin dönüþ hýzý
    public float jumpForce = 5f;  // Karakterin zýplama kuvveti
    public LayerMask groundLayers;  // Zýplama kontrolü için zemin katmanlarý
    public Transform groundCheck;  // Zemin kontrolü için nokta
    public float groundCheckRadius = 0.1f;  // Zemin kontrolü için yarýçap

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // Karakterin fiziksel olarak dönmesini engelle
    }

    void Update()
    {
        Move();
        Rotate();
        Jump();
    }

    void Move()
    {
        float moveDirection = Input.GetAxis("Vertical");  // W/S veya Up/Down ok tuþlarý
        Vector3 move = transform.forward * moveDirection * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);
    }

    void Rotate()
    {
        float turnDirection = Input.GetAxis("Horizontal");  // A/D veya Left/Right ok tuþlarý
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
