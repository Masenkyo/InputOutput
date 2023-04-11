using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatformSpawn : MonoBehaviour
{
    #region Variables
    public GameObject platformPrefab;
    public GameObject spawnPrefab;
    float speed;
    public Vector3 direction;
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
        speed += Time.deltaTime / 10;
        direction = new Vector3 (speed, 0, 0);
        transform.position += direction * Time.deltaTime;
        Platform();
    }
    void Platform()
    {
        time += Time.deltaTime * speed;
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
