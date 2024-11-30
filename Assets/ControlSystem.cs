using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    public float moveSpeed = 5f;  // Normal hareket h�z�
    public float sprintSpeed = 8f;  // Ko�ma h�z� (Shift ile aktif olur)
    public float rotationSpeed = 140f;  // Karakterin d�n�� h�z�
    public float jumpForce = 5f;  // Karakterin z�plama kuvveti

    private Rigidbody rb;
    private bool isGrounded;
    private float currentSpeed;  // �u anki h�z
    public CharacterAnimation characterAnimation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // Karakterin fiziksel olarak d�nmesini engelle
        characterAnimation = GameObject.FindWithTag("Player").GetComponent<CharacterAnimation>();

        currentSpeed = moveSpeed;  // Ba�lang��ta normal h�z ayarlan�r
    }

    void Update()
    {
        HandleSprint();  // Shift tu�una bas�lma durumunu kontrol eder
        MoveAndRotate();
        Jump();
    }

    void HandleSprint()
    {
        // Shift'e bas�l�ysa h�zlan, de�ilse normal h�za d�n
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
    }

    void MoveAndRotate()
    {
        float moveDirection = Input.GetAxis("Vertical");  // W/S veya Up/Down ok tu�lar�
        float turnDirection = Input.GetAxis("Horizontal");  // A/D veya Left/Right ok tu�lar�

        // Hareket i�lemi
        Vector3 move = transform.forward * moveDirection * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);

        // D�n�� i�lemi
        float turn = turnDirection * rotationSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        // Animasyon kontrol�
        if (moveDirection != 0 || turnDirection != 0) // Herhangi bir giri� varsa
        {
            characterAnimation.SetRunning();
        }
        else
        {
            characterAnimation.SetIdle();
        }
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
