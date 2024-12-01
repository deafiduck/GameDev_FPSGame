using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flyweight;

public class FlyweightGun : MonoBehaviour
{
    public List<FlyweightSettings> flyweights;  // Farklý mermi ayarlarýný içeren liste
    public Transform shootPoint; // Merminin çýkýþ noktasý
    public float rateOfFire = 0.2f; // Ateþ etme sýklýðý
    public int maxAmmo = 12; // Maksimum mermi kapasitesi
    public int currentAmmo; // Þu anki mermi sayýsý
    public int carriedAmmo = 60; // Taþýnan mermi miktarý
    private float nextFireTime = 0; // Bir sonraki ateþ etme zamaný
    private bool isReloading = false; // Yeniden yükleme durumu

    private void Start()
    {
        currentAmmo = maxAmmo;  // Baþlangýçta maksimum mermi kapasitesi
    }

    private void Update()
    {
      
        if (Input.GetButton("Fire1") && Time.time > nextFireTime && currentAmmo > 0 && !isReloading)
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
            nextFireTime = Time.time + rateOfFire;  // Bir sonraki ateþ etme zamanýný belirle

            // Mermi ayarýný seç ve Flyweight nesnesini al
            var flyweight = FlyweightFactory.Spawn(flyweights[0]);

            // Mermiyi doðru noktada konumlandýr
            flyweight.transform.position = shootPoint.position;
            flyweight.transform.rotation = shootPoint.rotation;

            // Mermiyi ateþle
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
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit)) // Merminin çýkacaðý yer, yönü, hedef, mesafe
        {
            // Hedefin "Enemy" olup olmadýðýný kontrol et
            if (hit.transform.CompareTag("Enemy"))
            {
                // Hedefin "EnemyHealth" bileþenini al
                EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    // Düþmana hasar ver
                    enemy.ReduceHealth(flyweight.settings.damage);
                    Debug.Log("Enemy hit, health reduced by: " + flyweight.settings.damage);
                }
            }
            else
            {
                Debug.Log("Hit something else");
            }
        }

        // Mermiyi havuza geri gönder
        FlyweightFactory.ReturnToPool(flyweight);
    }


    IEnumerator ReloadCoroutine()
    {
        yield return Helper.GetWaitForSeconds(1f); 

        int ammoNeeded = maxAmmo - currentAmmo;
        int ammoToReload = Mathf.Min(ammoNeeded, carriedAmmo);

        currentAmmo += ammoToReload;
        carriedAmmo -= ammoToReload;

        isReloading = false;  // Yeniden yükleme bitti
    }

    // Mermi bitince ateþ etme
    void EmptyFire()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + rateOfFire;
            Debug.Log("Out of ammo! Please reload.");
        }
    }
}
