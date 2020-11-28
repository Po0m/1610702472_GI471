using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class W5_SpawnerManager : MonoBehaviour
{
    private List<W5_Spawner> spawnerList = new List<W5_Spawner>();

    public static W5_SpawnerManager instance;

    private void Start()
    {
        instance = this;
        Init();
    }

    private void Init()
    {
        W5_Spawner[] spawnerArr = FindObjectsOfType<W5_Spawner>();


        spawnerList = spawnerArr.ToList<W5_Spawner>();
    }

    public void Spawn()
    {
        foreach(W5_Spawner spawner in spawnerList)
        {
            spawner.Spawn();
        }
    }




}
