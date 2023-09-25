using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRoot : MonoBehaviour
{
    public static AsyncOperation async;
    public static UnityEvent<bool> isLood=new UnityEvent<bool>();
    private static Hashtable has = new Hashtable();

    private void Start()
    {
        StartCoroutine(looding());
    }

    private void Update()
    {
        if (async.progress != null && async.progress >= 0.9f)
        {
            async.allowSceneActivation = true;
        }
    }
    public static T Load<T>(string path) where T : Object
    {
        if (has.ContainsKey(path))
        {
            return has[path] as T;
        }
        T t = Resources.Load<T>(path);
        has[path] = t;
        return t;
    }


    IEnumerator looding()
    {
        DontDestroyOnLoad(gameObject);
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        yield return null;
    }
}
