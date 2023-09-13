using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundStatsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roundNumber;
    [SerializeField] TextMeshProUGUI orcsPerRound;
    [SerializeField] TextMeshProUGUI killsPerRound;
    [SerializeField] TextMeshProUGUI multiplier;


    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3) { return; }
        
        roundNumber.text = RoundManager.Instance.currentRound.ToString();
        orcsPerRound.text = RoundManager.Instance.OrcsSpawnedThisRound().ToString();
        killsPerRound.text = RoundManager.Instance.orcsKilledInCurrentRound.ToString();
        multiplier.text = RoundManager.Instance.orcSpawnIncreasePercentage.ToString();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3) { return; }

        roundNumber.text = RoundManager.Instance.currentRound.ToString();
        orcsPerRound.text = RoundManager.Instance.OrcsSpawnedThisRound().ToString();
        killsPerRound.text = RoundManager.Instance.orcsKilledInCurrentRound.ToString();
        multiplier.text = RoundManager.Instance.orcSpawnIncreasePercentage.ToString();
    }
}
