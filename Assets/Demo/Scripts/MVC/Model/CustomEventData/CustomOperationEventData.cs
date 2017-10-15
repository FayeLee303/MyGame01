using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomOperationEventData : CustomEventData {

    public RoleModel.Direction dir { get; set; }
    public GameConfig.TurnDirection turnDir { get; set; }
    public GameConfig.OperationEvent OperationEventType { get; set; }
    public bool ismoving { get; set; }
}
