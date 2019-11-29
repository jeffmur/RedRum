using UnityEngine;
public class SceneSingleton<T> : MonoBehaviour where T : SceneSingleton<T>
{
    private static T m_Instance = null;

    protected virtual void Awake()
    {
            if (m_Instance == null)
            {
                m_Instance = gameObject.GetComponent<T>(); // In first scene, make us the singleton.
                DontDestroyOnLoad(gameObject);
            }
            else if (m_Instance != this)
                Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
}