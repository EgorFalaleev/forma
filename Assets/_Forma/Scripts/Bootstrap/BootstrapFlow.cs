using Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Bootstrap
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