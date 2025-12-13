using UnityEngine.SceneManagement;

namespace Forma.Runtime.Common
{
    public static class Constants
    {
        public static class Scenes
        {
            public static readonly int Bootstrap = SceneUtility.GetBuildIndexByScenePath("Bootstrap");
            public static readonly int Game = SceneUtility.GetBuildIndexByScenePath("Game");
        }
    }
}