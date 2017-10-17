using System;
using UnityEngine;
using HutongGames.PlayMaker;

namespace PixelCrushers.DialogueSystem.PlayMaker {
	
	[ActionCategory("Dialogue System")]
	[HutongGames.PlayMaker.TooltipAttribute("Changes the Dialogue Manager's default dialogue UI.")]
	public class SetDialogueUI : FsmStateAction {

        public FsmGameObject dialogueUI;
		
		public override void OnEnter() {
            if (dialogueUI == null || dialogueUI.Value == null) return;
            DialogueManager.UseDialogueUI(dialogueUI.Value);
			Finish();
		}
		
	}
	
}