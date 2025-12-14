using Forma.Runtime.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Forma.Runtime.Bootstrap
{
    public class BootstrapFlow : IStartable
    {
        public void Start()
        {
            Debug.Log("BootstrapFlow.Start()");

            SceneManager.LoadScene(Constants.Scenes.Game);
        }
    }
}