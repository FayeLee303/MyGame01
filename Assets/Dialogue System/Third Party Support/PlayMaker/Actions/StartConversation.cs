using System;
using UnityEngine;
using HutongGames.PlayMaker;

namespace PixelCrushers.DialogueSystem.PlayMaker {
	
	[ActionCategory("Dialogue System")]
	[HutongGames.PlayMaker.TooltipAttribute("Starts a conversation.")]
	public class StartConversation : FsmStateAction {
		
		[RequiredField]
		[HutongGames.PlayMaker.TooltipAttribute("The conversation to start")]
		public FsmString conversation;

		[HutongGames.PlayMaker.TooltipAttribute("The starting dialogue entry ID. Leave at -1 to start at the beginning")]
		public FsmInt startingEntryID;
		
		[HutongGames.PlayMaker.TooltipAttribute("The primary participant in the conversation (e.g., the player)")]
		public FsmGameObject actor;
		
		[HutongGames.PlayMaker.TooltipAttribute("The other participant in the conversation (e.g., the NPC)")]
		public FsmGameObject conversant;
		
		public override void Reset() {
			if (conversation != null) conversation.Value = string.Empty;
			startingEntryID = new FsmInt();
			startingEntryID.Value = -1;
			if (actor != null) actor.Value = null;
			if (conversant != null) conversant.Value = null;
		}
		
		public override void OnEnter() {
			string conversationTitle = (conversation != null) ? conversation.Value : string.Empty;
			Transform actorTransform = ((actor != null) && (actor.Value != null)) ? actor.Value.transform : null;
			Transform conversantTransform = ((conversant != null) && (conversant.Value != null)) ? conversant.Value.transform : null;
			if (actorTransform == null) LogWarning(string.Format("{0}: PlayMaker Action Start Conversation - actor is null", DialogueDebug.Prefix));
			if (string.IsNullOrEmpty(conversationTitle)) LogWarning(string.Format("{0}: PlayMaker Action Start Conversation - conversation title is blank", DialogueDebug.Prefix));
			var entryID = (startingEntryID != null) ? startingEntryID.Value : -1;
			DialogueManager.StartConversation(conversationTitle, actorTransform, conversantTransform, entryID);
			Finish();
		}
		
	}
	
}