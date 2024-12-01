using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithMouse : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Mouse hassasiyeti
    public Transform playerBody; // Karakterin dönüþünü kontrol edecek transform (genellikle karakterin body’si)
    public float verticalMovementIntensity = 0.1f; // Yukarý/aþaðý hareket için yoðunluk (ne kadar hareket edecek)

    private float xRotation = 0f; // Kamera için dikey dönüþ (yukarý-aþaðý)
    private float yRotation = 0f; // Karakterin yatay dönüþü (saða/sola)
    private float initialYPosition; // Karakterin baþlangýçtaki y pozisyonu

    void Start()
    {
        // Cursor'ý gizle ve kilitle
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/

        // Baþlangýç y pozisyonunu kaydet
        initialYPosition = playerBody.position.y;
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

        // Yukarý-aþaðý hareket: Mouse Y hareketine göre pozisyonu ayarla
        float verticalOffset = Mathf.Clamp(-mouseY * verticalMovementIntensity, -0.2f, 0.2f); // Yukarý/aþaðý hareket sýnýrlandýrýlýr
        playerBody.position = new Vector3(playerBody.position.x, initialYPosition + verticalOffset, playerBody.position.z);
    }
}
