using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
    public class Flyweight : MonoBehaviour
    {
        public FlyweightSettings settings; //iç durumlar

        private void OnEnable()
        {
            StartCoroutine(Destroy(settings.lifetime));
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * settings.speed * Time.deltaTime); // Mermiyi ileri doðru hareket ettir
        }


        IEnumerator Destroy(float delay)
        {
            yield return Helper.GetWaitForSeconds(settings.lifetime);
            // Destroy(gameObject);
            FlyweightFactory.ReturnToPool(this);

        }

      

    }
}
