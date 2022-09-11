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

    public float cameraSizeChangerCap = 300;


    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.ActualEnemy >= cameraSizeChangerCap)
        {
            cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.OrthographicSize, 22, 5);
            cameraSizeChangerCap = cameraSizeChangerCap * 100;
            Instantiate(enemyTrapPrefab, player.transform.position, Quaternion.identity);
        }
    }

}
