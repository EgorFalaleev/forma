using UnityEngine;
using UnityEngine.SceneManagement;

namespace Forma.Runtime.Common
{
    public static class Constants
    {
        public static class Scenes
        {
            public static readonly int Bootstrap =
                SceneUtility.GetBuildIndexByScenePath("Bootstrap");

            public static readonly int Game =
                SceneUtility.GetBuildIndexByScenePath("Game");
        }

        public static class Math
        {
            public const float Sqrt3 = 1.7320508f;
        }

        public static class Layers
        {
            public static readonly int HexGrid = LayerMask.NameToLayer("HexGrid");
        }
    }
}