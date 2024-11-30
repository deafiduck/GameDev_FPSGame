using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    public float moveSpeed = 5f;  // Karakterin hareket h�z�
    public float rotationSpeed = 140f;  // Karakterin d�n�� h�z�
    public float jumpForce = 5f;  // Karakterin z�plama kuvveti

    private Rigidbody rb;
    private bool isGrounded;
    private CharacterAnimation characterAnimation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // Karakterin fiziksel olarak d�nmesini engelle
        characterAnimation = GetComponent<CharacterAnimation>();
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

        // Karakter hareket ediyorsa animasyonu tetikleyin
       /* if (moveDirection != 0)
        {
            characterAnimation.SetRunning(true);
        }
        else
        {
            characterAnimation.SetRunning(false);
        }*/
    }

    void Rotate()
    {
        float turnDirection = Input.GetAxis("Horizontal");  // A/D veya Left/Right ok tu�lar�
        float turn = turnDirection * rotationSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
        /* if (turnDirection != 0)
        {
            characterAnimation.SetRunning(true);
        }
        else
        {
            characterAnimation.SetRunning(false);
        }*/
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Z�plama kontrol� i�in zeminde olup olmad���m�z� kontrol ediyoruz
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        // Z�plama kontrol� i�in zeminde olmad���m�z� kontrol ediyoruz
        isGrounded = false;
    }
}
