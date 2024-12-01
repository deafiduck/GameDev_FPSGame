using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


namespace Flyweight
{
    public class FlyweightFactory : MonoBehaviour
    {
        [SerializeField] bool collectionCheck;//havuzda her nesne alýndýðýnda veya geri verildiðinde koleksiyonun kontrol edilip edilmeyeceðini belirtir
        [SerializeField] int defaultCapacity=20; //havuzda kaç nesne olacak
        [SerializeField] int maxPoolSize=150;
        static FlyweightFactory instance;//singleton için

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
        //havuzda nesne varsa onu alýr yksa oluþturur
        public static void ReturnToPool(Flyweight f) => instance.GetPoolFor(f.settings)?.Release(f);
        //nesneyi havuza geri göndeir, release methodunu çaðýrýr
        IObjectPool<Flyweight> GetPoolFor(FlyweightSettings settings)
        {
            IObjectPool<Flyweight> pool;
            if (pools.TryGetValue(settings.type, out pool)) return pool;//bu type da havuz varsa onu döndür

            pool = new ObjectPool<Flyweight>(//yoksa oluþturur
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