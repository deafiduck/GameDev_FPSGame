using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gun : MonoBehaviour
{
    RaycastHit hit;

    public int currentAmmo = 12;  //şu anki bubble sayisi
    public int maxAmmo = 12; //gun'un bubble alma kapasitesi
    public int carriedAmmo = 60;  //tasıdıgımız bubble

    [SerializeField]
    float rateofFire;  //ne kadar sürede ates edilecegini hesaplar
    float nextFire = 0; //baslangic süresi

    [SerializeField]
    float weaponRange; //atis mesafesi(ne kadar uzağa atis yapabiliriz)

    public Transform shootPoint; //bubble'in cikis noktasi
    public GameObject bulletPrefab; // bubble prefabı

    public float damage = 20f;
    bool isReloading;

    private void Update()
    {
        if (currentAmmo > 0 && Input.GetButton("Fire1")) //mouse'un sol tusuna basinca ve mermin varsa ates et
        {
            Shoot();
        }
        else if (currentAmmo < 0 && Input.GetButton("Fire1") && !isReloading)
        {
            EmptyFire();
        }
        else if (Input.GetKeyDown(KeyCode.R) && currentAmmo <= maxAmmo && !isReloading)
        {
            isReloading = true;
            Reload();
        }
    }

    void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + rateofFire;
           
            currentAmmo--; //bubble sayisi azaliyo
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            ShootRay();
        }
    }

    void ShootRay()
    {
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, weaponRange)) //merminin cikacagi yer, yönü, hedef, mesafe
        {
            if (hit.transform.tag == "Enemy")
            {
                EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.ReduceHealth(damage);
                    Debug.Log("Enemy hit, health reduced by: " + damage);
                }
            }
            else
            {
                Debug.Log("Hit something else");
            }
        }
    }

    void Reload()
    {
        if (carriedAmmo <= 0) return;
        //anim.SetTrigger("Reload");
        //pistolAS.PlayOneShot(reloadAC);
        //StartCoroutine(ReloadCountDown(2f));
        //UpdateAmmoUI();
    }

    void EmptyFire()
    {
        if (Time.time > nextFire)  //sürekli tetige basamamak için
        {
            nextFire = Time.time + rateofFire;
            //  pistolAS.PlayOneShot(emptyFire);
            // anim.SetTrigger("Empty");
        }
    }
}