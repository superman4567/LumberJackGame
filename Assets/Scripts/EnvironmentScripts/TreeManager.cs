using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public static TreeManager Instance { get; private set; }
    public List<GameObject> nearbyTrees = new List<GameObject>();

    private int interactionIncrement;
    private float interactionPercentage;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
