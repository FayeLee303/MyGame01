using System;
using UnityEngine;
using HutongGames.PlayMaker;

namespace PixelCrushers.DialogueSystem.PlayMaker {
	
	[ActionCategory("Dialogue System")]
	[HutongGames.PlayMaker.TooltipAttribute("Send a message to the sequencer.")]
	public class SendSequencerMessage : FsmStateAction {
		
		[RequiredField]
		[HutongGames.PlayMaker.TooltipAttribute("The message to send to the sequencer")]
		public FsmString message;
		
		public override void Reset() {
			if (message != null) message.Value = string.Empty;
		}
		
		public override void OnEnter() {
			if (message != null) Sequencer.Message(message.Value);
			Finish();
		}
		
	}
	
}