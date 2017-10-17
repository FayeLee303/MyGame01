using System;
using UnityEngine;
using HutongGames.PlayMaker;

namespace PixelCrushers.DialogueSystem.PlayMaker {
	
	[ActionCategory("Dialogue System")]
	[HutongGames.PlayMaker.TooltipAttribute("Loads a saved game.")]
	public class LoadGame : FsmStateAction {
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[HutongGames.PlayMaker.TooltipAttribute("The variable containing savegame data")]
		public FsmString saveData;
		
		public override void Reset() {
			saveData = null;
		}
		
		public override void OnEnter() {	
			string data = (saveData == null) ? null : saveData.Value;
			if (string.IsNullOrEmpty(data)) {
				LogError("Saved game data is an empty string");
			} else {
				var levelManager = GameObject.FindObjectOfType<LevelManager>();
				if (levelManager != null) {
					levelManager.LoadGame(data);
				} else {
					PersistentDataManager.ApplySaveData(data);
				}
			}
			Finish();
		}
		
	}
	
}