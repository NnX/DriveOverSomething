using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timer = 3f;

    private void Start()
    {
        Invoke("DeactivateGameobject", timer);
    }

    void DeactivateGameobject()
    {
        gameObject.SetActive(false);
    }
}
