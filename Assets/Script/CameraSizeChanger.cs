using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSizeChanger : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public EnemySpawner enemy;
    public GameObject player;
    public GameObject enemyTrapPrefab;
    public GameObject deathBirdPrefab;

    public float cameraSizeChangerCap1 = 300;
    public float cameraSizeChangerCap2 = 30;
    bool a = false;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
       /* if (GameData.GetTotalActualEnemy() >= cameraSizeChangerCap1 && a == false)
        {
            cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.OrthographicSize, 22, 5);
            cameraSizeChangerCap1 = cameraSizeChangerCap1 = 350;
            Instantiate(enemyTrapPrefab, player.transform.position, Quaternion.identity);
            a = true;
        }
        
        if (GameData.GetTotalActualEnemy() >= cameraSizeChangerCap2)
        {
            Instantiate(deathBirdPrefab, player.transform.position, Quaternion.identity);
            cameraSizeChangerCap2 *= 2;
        }*/
    }

    
}
