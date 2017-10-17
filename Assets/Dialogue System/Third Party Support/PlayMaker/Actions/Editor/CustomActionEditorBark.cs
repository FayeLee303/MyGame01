using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

namespace PixelCrushers.DialogueSystem.PlayMaker {

	[CustomActionEditor(typeof(Bark))]
	public class CustomActionEditorBark : CustomActionEditor {

		private ConversationPicker conversationPicker = null;

		public override void OnEnable()	{
			var action = target as Bark;
			if (action.conversation == null) action.conversation = new HutongGames.PlayMaker.FsmString();
			conversationPicker = new ConversationPicker(EditorTools.FindInitialDatabase(), action.conversation.Value, !action.conversation.UseVariable);
		}
		
		public override bool OnGUI() {
			var isDirty = false;

			var action = target as Bark;
			if (action == null) return DrawDefaultInspector();
			
			if (action.conversation == null) action.conversation = new HutongGames.PlayMaker.FsmString();
			action.conversation.UseVariable = EditorGUILayout.Toggle(new GUIContent("Use Variable", "Specify the conversation title with a PlayMaker variable"), action.conversation.UseVariable);
			
			if (action.conversation.UseVariable) {
				EditField("conversation");
			} else {
				conversationPicker.Draw(true);
				if (!string.Equals(action.conversation.Value, conversationPicker.currentConversation)) {
					action.conversation.Value = conversationPicker.currentConversation;
					isDirty = true;
				}
			}

			EditField("speaker");
			EditField("listener");
			
			return isDirty || GUI.changed;
		}
	}
}