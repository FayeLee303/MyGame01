using System;
using UnityEngine;
using HutongGames.PlayMaker;

namespace PixelCrushers.DialogueSystem.PlayMaker {
	
	[ActionCategory("Dialogue System")]
	[HutongGames.PlayMaker.TooltipAttribute("Set the language to use for localization.")]
	public class SetLanguage : FsmStateAction {
		
		[RequiredField]
		[HutongGames.PlayMaker.TooltipAttribute("The current language to use")]
		public FsmString language;
		
		public override void Reset() {
			if (language != null) language.Value = string.Empty;
		}
		
		public override void OnEnter() {
			if (language != null) DialogueManager.SetLanguage(language.Value);
			Finish();
		}
		
	}
	
}