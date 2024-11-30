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
    float weaponRange; //atis mesafesi(ne kadar uzaða atis yapabiliriz)

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


    //her atýþ arasýna 0.5 gibi belirli bir fark koyar.Yani 0.5 sn aralýk ile atýþ yapmamýzý saðlar
    void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + rateofFire;

            currentAmmo--; //bubble sayisi azaliyo
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            ShootRay();
        }

      //  UpdateAmmoUI();

    }

    void ShootRay()
    {

        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, weaponRange)) //merminin cikacagi yer,yönü,hedef,mesafe(bizden çikan ray bir yere vurursa)
        {
            /*if (hit.transform.tag == "Enemy")
            {
                EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
                Instantiate(bloodEffect, hit.point, transform.rotation);
                enemy.ReduceHealth(damage);
            }
            else if (hit.transform.tag == "Head")
            {
                EnemyHealth enemy = hit.transform.GetComponentInParent<EnemyHealth>(); //parent'i içindeki scripte eriþeceðimiz için böyle yazdýk
                enemy.ReduceHealth(100f); //kafasýna vurunca direkt ölecek
                Instantiate(headShootBlood, hit.point, transform.rotation);
                hit.transform.gameObject.SetActive(false);
            }
            else if (hit.transform.tag == "Metal")
            {
                pistolAS.PlayOneShot(shootMetalAC);
                Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)); //Instantiate ile gameobject oluþturabiliriz.
            }
            else
            {
                Debug.Log("Something else");
            }*/
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
        if (Time.time > nextFire)  //sürekli tetiðe basamamak için
        {
            nextFire = Time.time + rateofFire;
          //  pistolAS.PlayOneShot(emptyFire);
          // anim.SetTrigger("Empty");
        }
    }
}
