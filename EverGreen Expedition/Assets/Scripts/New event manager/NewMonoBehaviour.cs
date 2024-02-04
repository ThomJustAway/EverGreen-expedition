using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.New_event_manager
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<TypeOfEvent, CustomAction<Action<>> > dictionaryOfEvent;
        
    }

    public class CustomAction<T> where T : Delegate
    {
        public List<T> actions;
        public TypeOfEvent eventType;


    }

    public enum TypeOfEvent
    {
        CryptidDeath,
        Win,
        Lose,
        TowerDeath
    }
}