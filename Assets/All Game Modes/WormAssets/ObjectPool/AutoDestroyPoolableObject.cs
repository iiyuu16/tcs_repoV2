using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyPoolableObject : MonoBehaviour
{
    public float autoDestroyTime = 5f;
    private const string DisableMethodName = "Disable";

    public virtual void OnEnable()
    {
        CancelInvoke(DisableMethodName);
        Invoke(DisableMethodName, autoDestroyTime);
    }

    public virtual void OnDisable()
    {
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
}
