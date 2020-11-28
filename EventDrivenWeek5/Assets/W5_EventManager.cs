using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class W5_EventManager : MonoBehaviour
{
    Dictionary<string, UnityEvent> eventDict = new Dictionary<string, UnityEvent>();

    public static W5_EventManager instace;

    private void Awake()
    {
        instace = this;
    }

    public void StartListening(string eventName, UnityAction listener)//เริ่ม listening
    {

        if(eventDict.ContainsKey(eventName))//เคยสร้าง listener
        {
            UnityEvent thisEventDict = eventDict[eventName];
            thisEventDict.AddListener(listener);
        }
        else//ยังไม่เคยสร้าง listener
        {
            UnityEvent thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            eventDict.Add(eventName, thisEvent);
        }
    }
    public void StopListening(string eventName, UnityAction listener)//หยุด listening
    {
        if(eventDict.ContainsKey(eventName))
        {
            UnityEvent thisEvent = eventDict[eventName];
            thisEvent.RemoveListener(listener);
        }
    }
    public void TriggerEvent(string eventName)//triggrt event ต่าง ๆ ที่เราจะใช้
    {
        if(eventDict.ContainsKey(eventName))
        {
            UnityEvent thisEvent = eventDict[eventName];
            thisEvent.Invoke();
        }
    }


}
