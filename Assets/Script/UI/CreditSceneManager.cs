using Sounds;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Script
{
    public class CreditSceneManager : MonoBehaviour
    {
        private void Start()
        {
            SoundManager.Instance.PlayMusic(Sounds.Sounds.BackgroundMusic);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene(0);
        }
    }
}