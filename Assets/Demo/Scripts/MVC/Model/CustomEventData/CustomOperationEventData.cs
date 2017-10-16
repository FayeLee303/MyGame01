using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomOperationEventData : CustomEventData {

    public RoleModel.Direction dir { get; set; }
    public GameConfig.TurnDirection turnDir { get; set; }
    public GameConfig.OperationEvent OperationEventType { get; set; }
    public bool ismoving { get; set; }
    public Transform playerTransform { get; set; }
    public float input_H { get; set; }//横竖轴上的输入
    public float input_V { get; set; }
    public ItemObj itemObj { get; set; }
}
