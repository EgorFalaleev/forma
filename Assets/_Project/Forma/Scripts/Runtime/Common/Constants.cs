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

        public static class Resources
        {
            const string Prefabs = "Prefabs";

            public static readonly string Player = $"{Prefabs}/Player";
            public static readonly string Tile = $"{Prefabs}/Tile";
            public static readonly string Turret = $"{Prefabs}/Turret";
        }
    }
}