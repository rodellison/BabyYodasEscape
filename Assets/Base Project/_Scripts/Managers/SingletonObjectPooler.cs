
namespace Base_Project._Scripts.Managers
{
    public abstract class SingletonObjectPooler : ObjectPooler
    {
        public static SingletonObjectPooler SharedInstance;
        void Awake()
        {
            //This is the main difference between this version (SingletonObjectPooler), and the one it inherits from (GameObjectPooler).
            //In this Singleton version, the code here ensures there is only one Pool created. 
            if (SharedInstance != null && SharedInstance != this)
                Destroy(gameObject);
            else
                SharedInstance = this;
        }

    }
}