using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flyweight;

public class FlyweightGun : MonoBehaviour
{
    public List<FlyweightSettings> flyweights;  // Farkl� mermi ayarlar�n� i�eren liste
    public Transform shootPoint; // Merminin ��k�� noktas�
    public float rateOfFire = 0.02f; // Ate� etme s�kl���
    public int maxAmmo = 12; // Maksimum mermi kapasitesi
    public int currentAmmo; // �u anki mermi say�s�
    public int carriedAmmo = 60; // Ta��nan mermi miktar�
    private float nextFireTime = 0; // Bir sonraki ate� etme zaman�
    private bool isReloading = false; // Yeniden y�kleme durumu



    public Camera playerCamera;    // Kamera referans�
    public float zoomedFOV = 30f; // Zoomland���nda FOV de�eri
    public float normalFOV = 60f;  // Normal FOV de�eri
    public float zoomSpeed = 5f;   // Zoom h�z�n� kontrol eder

    private bool isZooming = false; // Zoom yap�lma durumu

    private void Start()
    {
        currentAmmo = maxAmmo;  // Ba�lang��ta maksimum mermi kapasitesi
    }

    private void Update()
    {

        if (Input.GetMouseButton(1)) // Sa� t�k: 1
        {
            isZooming = true;
        }
        else
        {
            isZooming = false;
        }

        // Zoom i�lemi
        if (isZooming)
        {
            // Kamera FOV de�erini yava��a zoomlanan de�ere do�ru de�i�tir
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomedFOV, Time.deltaTime * zoomSpeed);
        }
        else
        {
            // FOV de�erini normale d�nd�r
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, normalFOV, Time.deltaTime * zoomSpeed);
        }

        // Sol t�klama (mouse left button) ile ate� etme
        if (Input.GetButton("Fire1") && Time.time > nextFireTime && currentAmmo > 0)
        {
            Shoot();
            ShootRay();
        }

        if (currentAmmo <= 0 && Input.GetButton("Fire1") && !isReloading)
        {
            EmptyFire();
        }

        
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
        {
            Reload();
        }
    }

   
    void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + rateOfFire;  // Bir sonraki ate� etme zaman�n� belirle

            // Mermi ayar�n� se� ve Flyweight nesnesini al
            var flyweight = FlyweightFactory.Spawn(flyweights[0]);

            // Mermiyi do�ru noktada konumland�r
            flyweight.transform.position = shootPoint.position;
            flyweight.transform.rotation = shootPoint.rotation;

            // Mermiyi ate�le
            currentAmmo--;  // Mermiyi azalt

           
        }
        

    }

 
    void Reload()
    {
        if (carriedAmmo > 0 && currentAmmo < maxAmmo)
        {
            isReloading = true;
            StartCoroutine(ReloadCoroutine());
        }
    }

    void ShootRay()
    {
        RaycastHit hit;
        var flyweight = FlyweightFactory.Spawn(flyweights[0]);
        // Raycast ile hedefi kontrol et
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit)) // Merminin ��kaca�� yer, y�n�, hedef, mesafe
        {
            // Hedefin "Enemy" olup olmad���n� kontrol et
            if (hit.transform.CompareTag("Enemy"))
            {
                // Hedefin "EnemyHealth" bile�enini al
                EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    // D��mana hasar ver
                    enemy.ReduceHealth(flyweight.settings.damage);
                    Debug.Log("Enemy hit, health reduced by: " + flyweight.settings.damage);
                }
            }
            else if (hit.transform.CompareTag("CubeWGun"))
            {
                Block block =hit.transform.GetComponent<Block>();
                if (block != null)
                {
                    // D��mana hasar ver
                    //block.ReduceHealth(35);
                    Debug.Log("block nanay a�ko");
                }
            }
            else if (hit.transform.CompareTag("bubbleMonster")){
                BubbleMonster BM = hit.transform.GetComponent<BubbleMonster>();
                if (BM != null)
                {
                    // D��mana hasar ver
                    BM.ReduceHealth(30);
                    Debug.Log("block nanay a�ko");
                }
            }
            else
            {
                Debug.Log("Hit something else");
            }
        }

        // Mermiyi havuza geri g�nder
        FlyweightFactory.ReturnToPool(flyweight);
    }


    IEnumerator ReloadCoroutine()
    {
        yield return Helper.GetWaitForSeconds(1f); 

        int ammoNeeded = maxAmmo - currentAmmo;
        int ammoToReload = Mathf.Min(ammoNeeded, carriedAmmo);

        currentAmmo += ammoToReload;
        carriedAmmo -= ammoToReload;

        isReloading = false;  // Yeniden y�kleme bitti
    }

    // Mermi bitince ate� etme
    void EmptyFire()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + rateOfFire;
            Debug.Log("Out of ammo! Please reload.");
        }
    }
}
