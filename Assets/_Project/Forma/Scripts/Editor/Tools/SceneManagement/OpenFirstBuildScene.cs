using System.IO;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Forma.Editor.Tools.SceneManagement
{
    [Overlay(typeof(SceneView), "Open first scene")]
    public class OpenFirstBuildSceneToolbar : ToolbarOverlay
    {
        public OpenFirstBuildSceneToolbar() : base(
            OpenFirstBuildSceneButton.ID) { }
    }

    [EditorToolbarElement(ID, typeof(SceneView))]
    public class OpenFirstBuildSceneButton : EditorToolbarButton
    {
        public const string ID = "OpenFirstBuildSceneButton";

        public OpenFirstBuildSceneButton()
        {
            UpdateButtonText();
            tooltip = "Open first scene from Build scene list";

            icon =
                EditorGUIUtility.IconContent("SceneAsset Icon").image as
                    Texture2D;

            EditorBuildSettings.sceneListChanged += UpdateButtonText;
            clicked += OpenScene;
        }

        void UpdateButtonText()
        {
            text = GetFirstSceneName();
        }

        void OpenScene()
        {
            if (EditorBuildSettings.scenes.Length == 0)
            {
                Debug.LogWarning("Build Scene List is empty");
                return;
            }

            string scenePath = EditorBuildSettings.scenes[0].path;

            if (EditorSceneManager
                .SaveCurrentModifiedScenesIfUserWantsTo())
                EditorSceneManager.OpenScene(scenePath);
        }

        string GetFirstSceneName()
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(0);

            return string.IsNullOrEmpty(scenePath)
                ? string.Empty
                : Path.GetFileNameWithoutExtension(scenePath);
        }
    }
}