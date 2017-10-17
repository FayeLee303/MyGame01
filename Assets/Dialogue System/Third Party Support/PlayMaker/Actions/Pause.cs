using System;
using UnityEngine;
using HutongGames.PlayMaker;

namespace PixelCrushers.DialogueSystem.PlayMaker {
	
	[ActionCategory("Dialogue System")]
	[HutongGames.PlayMaker.TooltipAttribute("Pauses the Dialogue System.")]
	public class Pause : FsmStateAction {
		
		public override void OnEnter() {
			DialogueManager.Pause();
			Finish();
		}
		
	}
	
}