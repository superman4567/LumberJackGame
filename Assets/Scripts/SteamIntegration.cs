using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamIntegration : MonoBehaviour
{
    private static SteamIntegration instance;

    private void Awake()
    {
        // Ensure there is only one instance of this script
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        try
        {
            Steamworks.SteamClient.Init(1530710);
            PrintYourName();
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void PrintYourName()
    {
        Debug.Log(Steamworks.SteamClient.Name);
    }

    private void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
    }

    private void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
    }
}
