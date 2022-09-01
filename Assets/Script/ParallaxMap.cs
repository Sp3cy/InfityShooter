using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMap : MonoBehaviour
{
    private Transform camTransform;
    private Transform mapHolder;

    [SerializeField] private Vector2 mapBlock;

    // Start is called before the first frame update
    void Start()
    {
        camTransform = Camera.main.transform;
        mapHolder = GameObject.FindGameObjectWithTag("MapHolder").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(camTransform.position.x - mapHolder.position.x) >= mapBlock.x)
        {
            float offsetPosX = (camTransform.position.x - mapHolder.position.x) % mapBlock.x;
            mapHolder.position = new Vector3(camTransform.position.x + offsetPosX, mapHolder.position.y);
        }

        if (Mathf.Abs(camTransform.position.y - mapHolder.position.y) >= mapBlock.y)
        {
            float offsetPosY = (camTransform.position.y - mapHolder.position.y) % mapBlock.y;
            mapHolder.position = new Vector3(mapHolder.position.x, camTransform.position.y + offsetPosY);
        }
    }
}
