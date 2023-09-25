using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerCameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cmCamera;
    [SerializeField] private float shakeIntensity = 0.75f;
    [SerializeField] private float shakeTime = 0.2f;
    private CinemachineBasicMultiChannelPerlin cbcmp;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { return; }
        cbcmp = cmCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbcmp.m_AmplitudeGain = 0.05f;
    }

    private IEnumerator ShakeCoroutine()
    {
        float timer = shakeTime;
        while (timer > 0)
        {
            cbcmp.m_AmplitudeGain = shakeIntensity;
            timer -= Time.deltaTime;
            yield return null;
        }
        StopShake();
    }

    public void ShakeCamera()
    {
        StartCoroutine(ShakeCoroutine());
    }

    public void StopShake()
    {
        cbcmp.m_AmplitudeGain = 0.05f;
    }
}
