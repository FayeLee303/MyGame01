using System;
using UnityEngine;
using HutongGames.PlayMaker;

namespace PixelCrushers.DialogueSystem.PlayMaker {
	
	[ActionCategory("Dialogue System")]
	[HutongGames.PlayMaker.TooltipAttribute("Unpauses the Dialogue System.")]
	public class Unpause : FsmStateAction {
		
		public override void OnEnter() {
			DialogueManager.Unpause();
			Finish();
		}
		
	}
	
}