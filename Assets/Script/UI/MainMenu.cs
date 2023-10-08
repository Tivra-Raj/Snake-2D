using Sounds;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject HowToPlayPanel;

    [Header("Main Menu Buttons")]
    [SerializeField] private Button singlePlayer;
    [SerializeField] private Button multiPlayer;
    [SerializeField] private Button howToPlay;
    [SerializeField] private Button creditScene;
    [SerializeField] private Button quit;
    [SerializeField] private Slider musicVolumeSlider;

    [Header("HowToPlay Buttons")]
    [SerializeField] private Button closeHowToPlayPanel;

    private void Awake()
    {
        singlePlayer.onClick.AddListener(SinglePlayerLevel);
        multiPlayer.onClick.AddListener(MultiPlayerLevel);
        howToPlay.onClick.AddListener(HowToPlay);
        creditScene.onClick.AddListener(LoadCreditScene);
        quit.onClick.AddListener(Quit);

        closeHowToPlayPanel.onClick.AddListener(CloseHowToPlay);
        HowToPlayPanel.SetActive(false);
    }

    private void Start()
    {
        UnityAction<float> setMusicVolumeAction = new(SoundManager.Instance.SetMusicVolume);
        musicVolumeSlider.onValueChanged.AddListener(setMusicVolumeAction);

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            SoundManager.Instance.LoadVolume(musicVolumeSlider);
        }
        else
            SoundManager.Instance.LoadVolume(musicVolumeSlider);

        SoundManager.Instance.PlayMusic(Sounds.Sounds.BackgroundMusic); 
    }

    public void SinglePlayerLevel()
    {
        SoundManager.Instance.PlaySoundEffect(Sounds.Sounds.ButtonClick);
        SceneManager.LoadScene(1);
    }

    public void MultiPlayerLevel()
    {
        SoundManager.Instance.PlaySoundEffect(Sounds.Sounds.ButtonClick);
        SceneManager.LoadScene(2);
    }

    public void HowToPlay()
    {
        SoundManager.Instance.PlaySoundEffect(Sounds.Sounds.ButtonClick);
        HowToPlayPanel.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        SoundManager.Instance.PlaySoundEffect(Sounds.Sounds.ButtonClick);
        HowToPlayPanel.SetActive(false);
    }

    public void LoadCreditScene()
    {
        SoundManager.Instance.PlaySoundEffect(Sounds.Sounds.ButtonClick);
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        SoundManager.Instance.PlaySoundEffect(Sounds.Sounds.ButtonClick);
        Application.Quit();
    }


}
