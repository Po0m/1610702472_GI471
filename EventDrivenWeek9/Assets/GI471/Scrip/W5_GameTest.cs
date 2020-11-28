using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W5_GameTest : MonoBehaviour
{

    private void Update()//กดปุ่ม F
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            //W5_SpawnerManager.instance.Spawn();
            W5_EventManager.instace.TriggerEvent("Spawn");
        }
    }



}
