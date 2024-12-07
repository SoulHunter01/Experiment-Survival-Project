using UnityEngine;

public static class ComponentUtil{
    public static T getSafeComponent<T>(GameObject gameObject, string caller = "") where T : Component
    {
        if (gameObject == null){
            Debug.LogError("ComponentUtil: " + caller + " was passed a null GameObject");
            return null;
        }
        if(gameObject.TryGetComponent<T>(out var comp)){
            return comp;
        }
        Debug.LogError("ComponentUtil: " + caller + " could not find component of type " + typeof(T).ToString());
        return null;

        // T component = gameObject.TryGetComponent<T>(out var comp) ? comp : null;
        // if (component == null)
        // {
        //     Debug.LogError("ComponentUtil: " + caller + " could not find component of type " + typeof(T).ToString());
        // }
        // return component;
    }
}