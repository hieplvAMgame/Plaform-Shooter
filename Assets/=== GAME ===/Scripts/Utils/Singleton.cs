using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Private static instance of the Singleton
    private static T _instance;

    // Property to access the instance
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // Search for an existing instance of the Singleton in the scene
                _instance = FindObjectOfType<T>();

                // If there isn't one, create a new one
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";

                    // Optional: Don't destroy the instance when loading a new scene
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    // Virtual Awake method in case any subclass wants to implement its own Awake logic
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            Debug.Log($"<color=green>[HIEPLV]</color> Create Singleton <color=cyan>{this.name}</color>");
            DontDestroyOnLoad(gameObject); // Optional: keep across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure there's only one instance
        }
    }
}
