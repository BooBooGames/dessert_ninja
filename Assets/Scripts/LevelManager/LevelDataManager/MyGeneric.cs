using UnityEngine;
public static class MyGeneric
{
    public static T Load<T>(string path) where T : Object
    {
        return (T)Resources.Load(path, typeof(T));
    }
}
