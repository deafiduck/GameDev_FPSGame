using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


namespace Flyweight
{
    public class FlyweightFactory : MonoBehaviour
    {
        [SerializeField] bool collectionCheck;//havuzda her nesne al�nd���nda veya geri verildi�inde koleksiyonun kontrol edilip edilmeyece�ini belirtir
        [SerializeField] int defaultCapacity=20; //havuzda ka� nesne olacak
        [SerializeField] int maxPoolSize=150;
        static FlyweightFactory instance;//singleton i�in

        readonly Dictionary<FlyweightType, IObjectPool<Flyweight>> pools = new();

        void Awake()//singleton
        {

            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public static Flyweight Spawn(FlyweightSettings s) => instance.GetPoolFor(s).Get();
        //havuzda nesne varsa onu al�r yksa olu�turur
        public static void ReturnToPool(Flyweight f) => instance.GetPoolFor(f.settings)?.Release(f);
        //nesneyi havuza geri g�ndeir, release methodunu �a��r�r
        IObjectPool<Flyweight> GetPoolFor(FlyweightSettings settings)
        {
            IObjectPool<Flyweight> pool;
            if (pools.TryGetValue(settings.type, out pool)) return pool;//bu type da havuz varsa onu d�nd�r

            pool = new ObjectPool<Flyweight>(//yoksa olu�turur
                settings.Create,
                settings.OnGet,
                settings.OnRelease,
                settings.OnDestroyPoolObject,
                collectionCheck,
                defaultCapacity,
                maxPoolSize

            );
            pools.Add(settings.type, pool);//havuzu kaydet
            return pool;
        }

    }
}