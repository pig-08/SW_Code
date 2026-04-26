using Gwamegi.Code.Core.EventSystems;
using UnityEngine;

namespace SW.Code.EventSystems
{
    public static class UiEvents
    {
        public static StopOnOffEvent StopOnOffEvent = new StopOnOffEvent();
    }

    public class StopOnOffEvent : GameEvent
    {
        public bool isOnOff;
    }
}