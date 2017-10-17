using System;
using UnityEngine;
using HutongGames.PlayMaker;

namespace PixelCrushers.DialogueSystem.PlayMaker {
	
	[ActionCategory("Dialogue System")]
	[HutongGames.PlayMaker.TooltipAttribute("Gets the value of a localized text table field.")]
	public class GetLocalizedText : FsmStateAction {
		
		[RequiredField]
		[HutongGames.PlayMaker.TooltipAttribute("The localized text table")]
		public LocalizedTextTable localizedTextTable;
		
		[RequiredField]
		[HutongGames.PlayMaker.TooltipAttribute("The field in the table")]
		public FsmString field;
		
		[UIHint(UIHint.Variable)]
		[HutongGames.PlayMaker.TooltipAttribute("Store the result in a String variable")]
		public FsmString storeResult;

		public override void Reset() {
			localizedTextTable = null;
			if (field != null) field.Value = string.Empty;
			storeResult = null;
		}
		
		public override void OnEnter() {
			if (localizedTextTable == null) {
				LogError(string.Format("{0}: Localized text table is null.", DialogueDebug.Prefix));
			} else if ((field == null) || (string.IsNullOrEmpty(field.Value))) {
				LogError(string.Format("{0}: Field is null or blank.", DialogueDebug.Prefix));
			} else if (!localizedTextTable.ContainsField(field.Value)) {
				LogError(string.Format("{0}: Localized text table {1} does not contain a field '{2}'.", new string[] { DialogueDebug.Prefix, localizedTextTable.name, field.Value }));
			} else {
				if (storeResult != null) storeResult.Value = localizedTextTable[field.Value];
			}
			Finish();
		}
		
	}
	
}