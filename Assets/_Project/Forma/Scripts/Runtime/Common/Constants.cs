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
        
        public static class HexMetrics
        {
            public const float OuterRadius = 10f;
            public const float InnerRadius = OuterRadius * 0.866025404f;

            public static readonly Vector3[] Corners =
            {
                new(0f, 0f, OuterRadius),
                new(InnerRadius, 0f, 0.5f * OuterRadius),
                new(InnerRadius, 0f, -0.5f * OuterRadius),
                new(0f, 0f, -OuterRadius),
                new(-InnerRadius, 0f, -0.5f * OuterRadius),
                new(-InnerRadius, 0f, 0.5f * OuterRadius),
                new(0f, 0f, OuterRadius)
            };
        }
    }
}