using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithMouse : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Mouse hassasiyeti
    public Transform playerBody; // Karakterin dönüþünü kontrol edecek transform (genellikle karakterin body’si)

    private float xRotation = 0f; // Kamera için dikey dönüþ (yukarý-aþaðý)
    private float yRotation = 0f; // Karakterin yatay dönüþü (saða/sola)

    void Start()
    {
        // Cursor'ý gizle ve kilitle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Mouse hareketlerini al
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Yatay dönüþ: Karakterin vücudu saða/sola döner (y ekseni)
        playerBody.Rotate(Vector3.up * mouseX);

        // Dikey dönüþ: Kameranýn yukarý/aþaðý döndürülmesi
        xRotation -= mouseY; // Dikey hareketi alýyoruz
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Kameranýn çok yukarý veya aþaðýya dönmesini engelle

        // Kameranýn dönüþünü uygula (sadece dikey hareket)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Kamera yalnýzca dikeyde döner
    }
}
