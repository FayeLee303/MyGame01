using System;
using UnityEngine;
using HutongGames.PlayMaker;

namespace PixelCrushers.DialogueSystem.PlayMaker {
	
	[ActionCategory("Dialogue System")]
	[HutongGames.PlayMaker.TooltipAttribute("Tells persistent objects that listen for OnDestroy to ignore the next OnDestroy since it's caused by the level being unloaded and not a gameplay action.")]
	public class LevelWillBeUnloaded : FsmStateAction {
		
		public override void OnEnter() {
			PersistentDataManager.LevelWillBeUnloaded();
			Finish();
		}
		
	}
	
}