using UnityEngine;
public class SceneSingleton<T> : MonoBehaviour where T : SceneSingleton<T>
{
    private static T m_Instance;

    public static T Instance { get => m_Instance; private set => m_Instance = value; }

    protected virtual void Awake()
    {
            if (Instance == null)
            {
                Instance = gameObject.GetComponent<T>(); // In first scene, make us the singleton.
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
                Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
}