using UnityEngine;

namespace Gekkou
{

    public abstract class SingletonMonobehavior<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _Instance;

        public static bool IsExists
        {
            get { return _Instance != null; }
        }

        public static T Instance
        {
            get
            {
                // Not Setting
                if (_Instance == null)
                {
                    // Search on scene
                    _Instance = FindObjectOfType<T>();

                    // Not on the scene
                    if (_Instance == null)
                    {
                        // Generated from resource prefabs
                        var obj = Instantiate((GameObject)Resources.Load(typeof(T).Name));
                        _Instance = obj.GetComponent<T>();

                        // Error if not in Resource
                        if (_Instance == null)
                            Debug.Log($"Error:{typeof(T).Name}");
                    }
                }

                return _Instance;
            }
            protected set
            {
                // only protected Setting
                _Instance = value;
            }
        }

        protected virtual void Awake()
        {
            // Deleted because it already exists
            if (this != Instance)
                Destroy(this.gameObject);

            DontDestroyOnLoad(this.gameObject);
        }
    }

}
