using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    public float moveSpeed = 5f;  // Normal hareket hýzý
    public float sprintSpeed = 8f;  // Koþma hýzý (Shift ile aktif olur)
    public float rotationSpeed = 140f;  // Karakterin dönüþ hýzý
    public float jumpForce = 5f;  // Karakterin zýplama kuvveti

    private Rigidbody rb;
    private bool isGrounded;
    private float currentSpeed;  // Þu anki hýz
    public CharacterAnimation characterAnimation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // Karakterin fiziksel olarak dönmesini engelle
        characterAnimation = GameObject.FindWithTag("Player").GetComponent<CharacterAnimation>();

        currentSpeed = moveSpeed;  // Baþlangýçta normal hýz ayarlanýr
    }

    void Update()
    {
        HandleSprint();  // Shift tuþuna basýlma durumunu kontrol eder
        MoveAndRotate();
        Jump();
    }

    void HandleSprint()
    {
        // Shift'e basýlýysa hýzlan, deðilse normal hýza dön
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
        float moveDirection = Input.GetAxis("Vertical");  // W/S veya Up/Down ok tuþlarý
        float turnDirection = Input.GetAxis("Horizontal");  // A/D veya Left/Right ok tuþlarý

        // Hareket iþlemi
        Vector3 move = transform.forward * moveDirection * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);

        // Dönüþ iþlemi
        float turn = turnDirection * rotationSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        // Animasyon kontrolü
        if (moveDirection != 0 || turnDirection != 0) // Herhangi bir giriþ varsa
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
        // Zýplama kontrolü için zeminde olup olmadýðýmýzý kontrol ediyoruz
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        // Zýplama kontrolü için zeminde olmadýðýmýzý kontrol ediyoruz
        isGrounded = false;
    }
}
