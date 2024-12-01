using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithMouse : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Mouse hassasiyeti
    public Transform playerBody; // Karakterin d�n���n� kontrol edecek transform (genellikle karakterin body�si)
    public float verticalMovementIntensity = 0.1f; // Yukar�/a�a�� hareket i�in yo�unluk (ne kadar hareket edecek)

    private float xRotation = 0f; // Kamera i�in dikey d�n�� (yukar�-a�a��)
    private float yRotation = 0f; // Karakterin yatay d�n��� (sa�a/sola)
    private float initialYPosition; // Karakterin ba�lang��taki y pozisyonu

    void Start()
    {
        // Cursor'� gizle ve kilitle
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/

        // Ba�lang�� y pozisyonunu kaydet
        initialYPosition = playerBody.position.y;
    }

    void Update()
    {
        // Mouse hareketlerini al
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Yatay d�n��: Karakterin v�cudu sa�a/sola d�ner (y ekseni)
        playerBody.Rotate(Vector3.up * mouseX);

        // Dikey d�n��: Kameran�n yukar�/a�a�� d�nd�r�lmesi
        xRotation -= mouseY; // Dikey hareketi al�yoruz
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Kameran�n �ok yukar� veya a�a��ya d�nmesini engelle

        // Kameran�n d�n���n� uygula (sadece dikey hareket)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Kamera yaln�zca dikeyde d�ner

        // Yukar�-a�a�� hareket: Mouse Y hareketine g�re pozisyonu ayarla
        float verticalOffset = Mathf.Clamp(-mouseY * verticalMovementIntensity, -0.2f, 0.2f); // Yukar�/a�a�� hareket s�n�rland�r�l�r
        playerBody.position = new Vector3(playerBody.position.x, initialYPosition + verticalOffset, playerBody.position.z);
    }
}
