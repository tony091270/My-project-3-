using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] ChestSpawnPoints;
    [SerializeField] private List<GameObject> UpgradeChests;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ChestSpawnPoints.Length; i++)
        {
            Instantiate(UpgradeChests[1], ChestSpawnPoints[i].transform.position, Quaternion.identity);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
