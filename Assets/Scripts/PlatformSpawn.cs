using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatformSpawn : MonoBehaviour
{
    #region Variables
    public GameObject platformPrefab;
    public GameObject spawnPrefab;
    public Vector3 speed = new (1,0,0);
    public List<GameObject> spawnList = new List<GameObject>();
    public float time;
    float number = 5;
    float height = 1;
    #endregion Variables
    void Start()
    {
        spawnPrefab = Instantiate(platformPrefab, new Vector3(transform.position.x, -2, 0), transform.rotation);
        spawnList.Add(spawnPrefab);
    }
    void Update()
    {
        transform.position += speed * Time.deltaTime;
        Platform();
    }
    void Platform()
    {
        time += Time.deltaTime;
        if (time > number)
        {
            number = Random.Range(3f, 7f);
            height = Random.Range(-2f, 2f);
            spawnPrefab = Instantiate(platformPrefab, new Vector3(transform.position.x + 3, height, 0), transform.rotation);
            spawnList.Add(spawnPrefab);
            time = 0;
            if (spawnList.Count == 4)
            {
                Destroy(spawnList[0]);
                spawnList[0] = spawnPrefab;
                spawnList.Remove(spawnPrefab);
            }
        }
    }
}
