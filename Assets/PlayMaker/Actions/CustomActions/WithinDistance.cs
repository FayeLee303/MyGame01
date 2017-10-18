using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("CustomActions")]
    [Tooltip("根据Tag寻找最近的Game Object，可以添加多个Tag作为搜索条件，每个Tag都会返回一个Game Object（或者返回null）")]
    public class WithinDistance : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Measure distance from this GameObject.")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Layer)]
        [Tooltip("射线检测所需要判断的Layer")]
        public FsmInt objectLayerMask;

        [RequiredField]
        [Tooltip("搜索的距离")]
        public FsmFloat viewDistance;

        [CompoundArray("Game Objects", "Tag", "Game Object")]        
        [UIHint(UIHint.Tag)]
        public FsmString[] Tag;
        [UIHint(UIHint.Variable)]
        public FsmGameObject[] gameObjects;

        GameObject closestObj = null;
        private float closestDist = Mathf.Infinity;
        private int count = 0;

        public override void Reset()
        {
            gameObject = null;
            objectLayerMask.Value = 0;
            viewDistance = null;
            gameObjects = new FsmGameObject[3];
            Tag = new FsmString[] { "Untagged", "Untagged", "Untagged" };
            GameObject closestObj = null;
            float closestDist = Mathf.Infinity;
            int count = 0;
        }

        public override void OnEnter()
        {
            Find();
        }

        public override void OnUpdate()
        {
            Find();
        }

        void Find()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            Collider[] hitColliders = Physics.OverlapSphere(go.transform.position, viewDistance.Value);
            if (hitColliders.Length > 0)
            {
                for (int i = 0; i < Tag.Length; i++)
                {
                    count = 0;
                    closestDist = Mathf.Infinity;
                    closestObj = null;
                    foreach (var o in hitColliders)
                    {
                        if (o.gameObject.tag == Tag[i].Value)
                        {
                            count++;
                            var dist = (go.transform.position - o.gameObject.transform.position).sqrMagnitude;
                            if (dist < closestDist)
                            {
                                closestDist = dist;
                                closestObj = o.gameObject;
                            }
                        }
                    }
                    if (count > 0)
                    {
                        gameObjects[i].Value = closestObj;
                        //Debug.Log(gameObjects[i].Value.name);
                    }
                    else
                    {
                        gameObjects[i].Value = null;
                    }
                }
                return;
            }
            else
            {
                return;
            }
        }
    }

}

