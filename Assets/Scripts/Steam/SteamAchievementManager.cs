using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamAchievementManager : MonoBehaviour
{
    public static SteamAchievementManager instance;

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

    public void IsThisAchievementUnlocked(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);
        Debug.Log($"Achievement {id} status: " + ach.State);
    }

    public void UnlockAchievement(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);
        ach.Trigger();
    }

    public void ClearAchievement(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);
        ach.Clear();
    }
}
