using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

namespace PixelCrushers.DialogueSystem.PlayMaker {

	[CustomActionEditor(typeof(RunLua))]
	public class CustomActionEditorRunLua : CustomActionEditor {

		private LuaScriptWizard luaWizard = null;

		public override void OnEnable()	{
			var action = target as RunLua;
			if (action.luaCode == null) action.luaCode = new HutongGames.PlayMaker.FsmString();
			luaWizard = new LuaScriptWizard(EditorTools.FindInitialDatabase());
		}
		
		public override bool OnGUI() {
			var isDirty = false;

			var action = target as RunLua;

			if (!action.luaCode.UseVariable) {
				var newCode = luaWizard.Draw(new GUIContent("Lua Code"), action.luaCode.Value);
				if (!string.Equals(newCode, action.luaCode.Value)) {
					action.luaCode.Value = newCode;
					isDirty = true;
				}
			}

			EditField("debug");
			EditField("storeResult");

			return isDirty || GUI.changed;
		}
	}
}