using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StoreManager : MonoBehaviour
{

    public void TryUnlockSkill(Skill skill)
    {
        //if (playerCoins >= skill.cost)
        //{
        //    bool prerequisitesMet = true;
        //    foreach (Skill prerequisite in skill.prerequisites)
        //    {
        //        if (!IsSkillUnlocked(prerequisite))
        //        {
        //            prerequisitesMet = false;
        //            break;
        //        }
        //    }

        //    if (prerequisitesMet)
        //    {
        //        // Deduct the cost from the player's coins
        //        playerCoins -= skill.cost;

        //        // Unlock the skill
        //        UnlockSkill(skill);

        //        // Update UI and player data
        //        UpdateUI();
        //        SavePlayerData();
        //    }
        //    else
        //    {
        //        // Display a message indicating prerequisites are not met
        //        ShowPrerequisiteMessage();
        //    }
        //}
        //else
        //{
        //    // Display a message indicating not enough coins
        //    ShowInsufficientCoinsMessage();
        //}
    }






}
