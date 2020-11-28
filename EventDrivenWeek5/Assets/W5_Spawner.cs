using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class W5_Spawner : MonoBehaviour
{
    public GameObject zombie;
    public UnityEvent OnSpawn;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Spawn();
        }
    }

    private void OnEnable()
    {
        W5_EventManager.instace.StartListening("Spawn", Spawn);
    }

    private void OnDisable()
    {
        W5_EventManager.instace.StopListening("Spawn", Spawn);
    }

    public void Spawn()
    {
        Instantiate(zombie, this.transform.position, this.transform.rotation);
        OnSpawn.Invoke();

    }



}
