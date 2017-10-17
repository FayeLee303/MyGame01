using System;
using UnityEngine;
using HutongGames.PlayMaker;

namespace PixelCrushers.DialogueSystem.PlayMaker {
	
	[ActionCategory("Dialogue System")]
	[HutongGames.PlayMaker.TooltipAttribute("Gets the number of quests in a specified state.")]
	public class GetQuestCount : FsmStateAction {

		[RequiredField]
		[HutongGames.PlayMaker.TooltipAttribute("The quest state (unassigned, active, success, or failure)")]
		public FsmString state;
		
		[UIHint(UIHint.Variable)]
		[HutongGames.PlayMaker.TooltipAttribute("Store the result in an Int variable")]
		public FsmInt storeResult;

		public override void Reset() {
			if (state != null) state.Value = string.Empty;
			storeResult = null;
		}
		
		public override void OnEnter() {
			if (PlayMakerTools.IsValueAssigned(state)) {
				string[] quests = QuestLog.GetAllQuests(QuestLog.StringToState(state.Value));
				if (storeResult != null) storeResult.Value = (quests != null) ? quests.Length : 0;
			} else {
				LogError(string.Format("{0}: State must be assigned first.", DialogueDebug.Prefix));
			}
			Finish();
		}
		
	}
	
}