using Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    //public class EventManager : Singleton<EventManager>
    //{
    //    private Dictionary<TypeOfEvent, List<Action>> dictionaryOfEvents = new Dictionary<TypeOfEvent, List<Action>>();

    //    private List<Action<CryptidBehaviour>> cryptidDeathEvent;

    //    protected override void Awake()
    //    {
    //        base.Awake();
    //        Init();
    //    }

    //    private void Init()
    //    {
    //        foreach (var eventName in (TypeOfEvent[])Enum.GetValues(typeof(TypeOfEvent)))
    //        {//create a list for each event
    //            dictionaryOfEvents.Add(eventName, new List<Action>());
    //        }

    //        cryptidDeathEvent = new List<Action<CryptidBehaviour>>();

    //    }

    //    public void AddListener(TypeOfEvent eventName, Action callback)
    //    {
    //        //since list is a reference type, just reference the list and add the callback
    //        var list = dictionaryOfEvents[eventName];
    //        list.Add(callback);
    //    }

    //    public void RemoveListener(TypeOfEvent eventName, Action callback)
    //    {
    //        var list = dictionaryOfEvents[eventName];
    //        list.Remove(callback); //remove the callback
    //    }
    //    public void AlertListeners(TypeOfEvent eventName)
    //    {
    //        var list = dictionaryOfEvents[eventName];
    //        //copy the list so that if any listener is deleted in 
    //        //the current list of event, it will still continue
    //        //calling out the listeners until the function ends
    //        var copyList = new List<Action>(list);
    //        foreach (var action in copyList)
    //        {
    //            action.Invoke();
    //        }
    //    }

    //    public void CryptidDeathAddListener(Action<CryptidBehaviour> callback)
    //    {
    //        //since list is a reference type, just reference the list and add the callback
    //        cryptidDeathEvent.Add(callback);
    //    }

    //    public void CryptidDeathRemoveListener(Action<CryptidBehaviour> callback)
    //    {
    //        cryptidDeathEvent.Remove(callback); //remove the callback
    //    }

    //    public void CryptidDeathAlertListeners(CryptidBehaviour cryptid)
    //    {
    //        var copyList = new List<Action<CryptidBehaviour>>(cryptidDeathEvent);
    //        foreach (var action in copyList)
    //        {
    //            action.Invoke(cryptid);
    //        }
    //    }

    //    public void ResetEventManager()
    //    {
    //        dictionaryOfEvents.Clear();
    //        cryptidDeathEvent.Clear();
    //        Init();
    //    }



    //}
    
    //public enum TypeOfEvent
    //{
    //    WinEvent,
    //    LoseEvent,
    //    ReloadUI
    //}
}