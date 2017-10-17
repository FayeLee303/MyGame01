using System;
using System.Collections;
using UnityEngine;
using HutongGames.PlayMaker;
#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
using UnityEngine.SceneManagement;
#endif

namespace PixelCrushers.DialogueSystem.PlayMaker
{

    [ActionCategory("Dialogue System")]
    [HutongGames.PlayMaker.TooltipAttribute("Loads a level with Dialogue System persistent data.")]
    public class LoadLevel : FsmStateAction
    {

        [RequiredField]
        [HutongGames.PlayMaker.TooltipAttribute("The name of the level to load. NOTE: Must be in the list of levels defined in File->Build Settings... ")]
        public FsmString levelName;

        [HutongGames.PlayMaker.TooltipAttribute("Load the level additively, keeping the current scene. NOTE: Not used if the scene has a LevelManager.")]
        public bool additive;

        [HutongGames.PlayMaker.TooltipAttribute("Load the level asynchronously in the background. NOTE: Not used if the scene has a LevelManager.")]
        public bool async;

        [HutongGames.PlayMaker.TooltipAttribute("Reset the Dialogue System state before loading.")]
        public bool resetDialogueDatabase = false;

        [HutongGames.PlayMaker.TooltipAttribute("If Reset Dialogue Database is ticked, tick this to reset to the initial database or untick to keep all loaded databases.")]
        public bool resetToInitialDatabase = false;

        [HutongGames.PlayMaker.TooltipAttribute("After loading the level, apply persistent data saved in the Dialogue System's Lua environment.")]
        public bool applyPersistentData = true;

        [HutongGames.PlayMaker.TooltipAttribute("Delay this many frames before applying persistent data to the newly-loaded scene. Allows GameObjects to run their Start methods first.")]
        public int framesToWaitBeforeApplyData = 0;

        [HutongGames.PlayMaker.TooltipAttribute("Event to send when the level has loaded. NOTE: This only makes sense if the FSM is still in the scene! Not used if the scene has a LevelManager.")]
        public FsmEvent loadedEvent;

        [HutongGames.PlayMaker.TooltipAttribute("Keep this GameObject in the new level. NOTE: The GameObject and components is disabled then enabled on load; uncheck Reset On Disable to keep the active state.")]
        public FsmBool dontDestroyOnLoad;

        private AsyncOperation asyncOperation = null;

        public override void Reset()
        {
            if (levelName != null) levelName.Value = string.Empty;
            additive = false;
            async = false;
            loadedEvent = null;
            dontDestroyOnLoad = false;
        }

        public override void OnEnter()
        {
            string level = (levelName == null) ? null : levelName.Value;
            if (string.IsNullOrEmpty(level))
            {
                LogError("Level name is an empty string");
            }
            else
            {
                if (dontDestroyOnLoad.Value)
                {
                    var root = Owner.transform.root;
                    UnityEngine.Object.DontDestroyOnLoad(root.gameObject);
                }
                var levelManager = GameObject.FindObjectOfType<LevelManager>();
                if (levelManager != null && !resetDialogueDatabase)
                {
                    levelManager.LoadLevel(level);
                }
                else
                {
                    PersistentDataManager.LevelWillBeUnloaded();
                    if (resetDialogueDatabase)
                    {
                        DatabaseResetOptions databaseResetOption = resetToInitialDatabase ? DatabaseResetOptions.RevertToDefault : DatabaseResetOptions.KeepAllLoaded;
                        DialogueManager.ResetDatabase(databaseResetOption);
                    }
                    else
                    {
                        if (resetToInitialDatabase)
                        {
                            LogWarning("Reset To Initial Database is ticked, but it has no effect because Reset Dialogue Database is unticked.");
                        }
                        PersistentDataManager.Record();
                    }
#if UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
                    if (async)
                    {
                        if (additive)
                        {
                            asyncOperation = Application.LoadLevelAdditiveAsync(level);
                        }
                        else
                        {
                            asyncOperation = Application.LoadLevelAsync(level);
                        }
                        return; // Don't finish yet.
                    }
                    else
                    {
                        if (additive)
                        {
                            Application.LoadLevelAdditive(level);
                        }
                        else
                        {
                            Application.LoadLevel(level);
                        }
#else
					if (async) {
						if (additive) {
							asyncOperation = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
						} else {
							asyncOperation = SceneManager.LoadSceneAsync(level);
						}
						return; // Don't finish yet.
					} else {
						if (additive) {
							SceneManager.LoadScene(level, LoadSceneMode.Additive);
						} else {
							SceneManager.LoadScene(level);
						}
#endif
                    }
                }
            }
            DoneLoadingLevel();
        }

        public override void OnUpdate()
        {
            if (asyncOperation != null && asyncOperation.isDone)
            {
                DoneLoadingLevel();
            }
        }

        private void DoneLoadingLevel()
        {
            DialogueManager.Instance.StartCoroutine(DoneLoadingLevelCoroutine());
        }

        private IEnumerator DoneLoadingLevelCoroutine()
        {
            Fsm.Event(loadedEvent);
            for (int i = 0; i < framesToWaitBeforeApplyData; i++)
            {
                yield return null;
            }
            if (applyPersistentData) PersistentDataManager.Apply();
            Finish();
        }



    }

}