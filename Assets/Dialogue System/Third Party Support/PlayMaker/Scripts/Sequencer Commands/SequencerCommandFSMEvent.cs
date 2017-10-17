using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.PlayMaker;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Implements sequencer command FSMEvent(event, [subject, [fsm]]).
	/// </summary>
	public class SequencerCommandFSMEvent : SequencerCommand {

		public void Start() {
			string eventName = GetParameter(0);
			bool all = string.Equals(GetParameter(1), "all", System.StringComparison.OrdinalIgnoreCase);
			Transform subject = all ? null : GetSubject(1, Sequencer.Speaker);
			string fsmName = GetParameter(2);
			if (string.IsNullOrEmpty(eventName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: FSMEvent(): event name is empty", DialogueDebug.Prefix));
			} else if (!all && (subject == null)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: FSMEvent({1}, {2}, {3}): subject is null", DialogueDebug.Prefix, eventName, GetParameter(1), fsmName));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: FSMEvent({1}, {2}, {3}) sending event to FSM(s)", DialogueDebug.Prefix, eventName, subject.name, fsmName));
				if (all) {
					// Send event to all GameObjects in scene:
					GameObject[] gameObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
					foreach (GameObject go in gameObjects) {
						SendEventToFSMs(go.transform, eventName, fsmName);
					}
				} else {
					// Send event to specified subject:
					SendEventToFSMs(subject, eventName, fsmName);
				}

			}
			Stop();
		}

		private void SendEventToFSMs(Transform subject, string eventName, string fsmName) {
			if (subject == null) return;
			foreach (var fsm in subject.GetComponents<PlayMakerFSM>()) {
				if (string.IsNullOrEmpty(fsmName) || string.Equals(fsmName, fsm.FsmName)) {
					fsm.SendEvent(eventName);
				}
			}
		}
		
	}

}
