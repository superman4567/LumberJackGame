using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableParent : MonoBehaviour
{
    public static Action breakableBroke;
    void Update()
    {
        if (transform.childCount != 0) { return; }

        breakableBroke?.Invoke();
        Destroy(gameObject);
    }
}
