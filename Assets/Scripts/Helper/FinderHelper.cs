using UnityEngine;

public class FinderHelper : MonoBehaviour
{
    public static T GetComponentOnObject<T>(GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component != null)
        {
            return component;
        }
        
        component = gameObject.GetComponentInParent<T>();
        if (component != null)
        {
            return component;
        }
        
        component = gameObject.GetComponentInChildren<T>();
        if (component != null)
        {
            return component;
        }

        return null;

    }
}
