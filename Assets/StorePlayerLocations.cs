using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StorePlayerLocations : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private float savePlayerPosInterval;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float linerenderHeight;

    private List<Vector3> playerPosList = new List<Vector3>();
    private Vector3[] playerPosArray;
    private float timer;
    


    void Start()
    {
        //bring up saved list
        playerPosList.Add(new Vector3(playerPos.position.x, playerPos.position.y + linerenderHeight, playerPos.position.z));
    }

    void Update()
    {
        DistanceAndTimeCheck();

        if (playerPosList.Count < 2) return;
        lineRenderer.positionCount = playerPosList.Count;
        lineRenderer.SetPositions(playerPosArray);
    }

    private void DistanceAndTimeCheck()
    {
        if (playerPosList.Count == 0) return;

        float prevDistance = .5f;
        if (prevDistance >= Vector3.Distance(new Vector3(playerPos.position.x, playerPos.position.y + linerenderHeight, playerPos.position.z), playerPosList.Last())) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = savePlayerPosInterval;
            playerPosList.Add(new Vector3(playerPos.position.x, playerPos.position.y + linerenderHeight,playerPos.position.z));
            UpdateSpriteRendere();
        }
    }

    private void UpdateSpriteRendere()
    {
        playerPosArray = new Vector3[playerPosList.Count];
        for (int i = 0; i < playerPosList.Count; i++)
        {
            playerPosArray[i] = playerPosList[i];
        }
    }
}
