using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;
    public float damage = 20f;

    private void Start()
    {
        Destroy(gameObject, lifetime); // Bubble belirli bir s�re sonra yok edilsin
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime); // Mermiyi ileri do�ru hareket ettir
    }

    private void OnTriggerEnter(Collider other)
    {
        // E�er mermi bir d��mana �arparsa, d��man�n sa�l�k scriptini bul ve hasar ver
       // EnemyHealth enemy = other.GetComponent<EnemyHealth>();
       /* if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }*/

        // Mermi herhangi bir �eye �arpt���nda yok et
        Destroy(gameObject);
    }
}
