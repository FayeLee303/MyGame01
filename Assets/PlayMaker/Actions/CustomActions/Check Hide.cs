using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("CustomActions")]
    [Tooltip("判断指定物体是否被遮挡，返回一个布尔值和遮挡物")]
    public class CheckHide : FsmStateAction
    {
        [RequiredField]
        [Tooltip("指定需要判断的物体")]
        public FsmGameObject target;

        [UIHint(UIHint.Variable)]
        [Tooltip("返回是否被遮挡")]
        public FsmBool IsHide;

        [UIHint(UIHint.Variable)]
        [Tooltip("返回遮挡物")]
        public FsmGameObject Shelter;

        public bool everyFrame;

        protected Ray ray;
        protected RaycastHit hit;

        public override void Reset()
        {
            target = null;
            IsHide = new FsmBool();
            Shelter = new FsmGameObject();
        }

        public override void OnEnter()
        {
            HideCheck();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            HideCheck();
        }

        void HideCheck()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.Value.transform.position);
            ray = Camera.main.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
               if(hit.collider.gameObject.layer == 9)
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red);
                    Shelter.Value = hit.collider.gameObject;
                    IsHide.Value = true;
                }
                else
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.green);
                    Shelter.Value = null;
                    IsHide.Value = false;
                }
            }
        }
    }
}
