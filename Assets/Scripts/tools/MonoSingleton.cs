using UnityEngine;

namespace tools
{
    public class MonoSingletonDDO<T> : MonoBehaviour where T : MonoSingletonDDO<T>
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance==null)
            {
                Instance = (T)this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            } 
        }
    }
    
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance==null)
            {
                Instance = (T)this;
            }
            else
            {
                Destroy(gameObject);
            } 
        }
    }
}
