using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
    [CreateAssetMenu(menuName ="Flyweight/Flyweight Settings")]
    public class FlyweightSettings : ScriptableObject
    {

        /*ScriptableObject, Unity'nin daha hafif ve performans odaklý veri saklama sýnýfýdýr.
         * ScriptableObject'ler, bir GameObject'e baðlý deðildir;
         * daha çok, paylaþýlan veri ve ayarlarý saklamak için kullanýlýr*/

        public FlyweightType type;
        public float speed = 20f;
        public float lifetime = 5f;
        public float damage = 20f;
        public GameObject prefab;
        

      
        public Flyweight Create()
        {
            var go = Instantiate(prefab); //nesneyi sahneye kopyalar
            go.SetActive(false);
            go.name = prefab.name;

            var flyweight = go.AddComponent<Flyweight>();
            flyweight.settings = this;

            return flyweight;
        }
        public void OnGet(Flyweight f) => f.gameObject.SetActive(true);
        public void OnRelease(Flyweight f) => f.gameObject.SetActive(false);

        public void OnDestroyPoolObject(Flyweight f) => Destroy(f.gameObject);
    }

    public enum FlyweightType { bubble}
}
