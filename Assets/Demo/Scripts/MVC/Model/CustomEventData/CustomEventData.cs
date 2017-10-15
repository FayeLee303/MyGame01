using System.Collections;
using System.Collections.Generic;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

public class CustomEventData : IEvent {

    public object type { get; set; }
    public IEventDispatcher target { get; set; }
    public object data { get; set; }

    public string str { get; set; }
    public GameObject currentObj { get; set; }
}
