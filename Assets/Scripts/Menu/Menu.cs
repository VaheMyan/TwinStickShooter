using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //Menu
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private GameObject _musicCross;
    [SerializeField] private GameObject _sfxCross;

    //Game
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private bool isPause = false;
    [SerializeField] private NetworkManager networkManager;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            networkManager = GameObject.FindObjectOfType<NetworkManager>();
        }

        MusicVolume();
        SFXVolume();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuiteGame()
    {
        Application.Quit();
    }
    public void SelectPlayer()
    {

    }
    public void Settings()
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }
    public void BackToMainMenu()
    {
        _settingsMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }
    public void Pause()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (isPause)
            {
                Continue();
            }
            else
            {
                _pauseMenu.SetActive(true);
                isPause = true;
                Time.timeScale = 0f;
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (isPause)
            {
                Continue();
            }
            else
            {
                _pauseMenu.SetActive(true);
                isPause = true;
            }
        }
    }
    public void Continue()
    {
        _pauseMenu.SetActive(false);
        isPause = false;
        Time.timeScale = 1f;
    }
    public async void Reset()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            Time.timeScale = 1f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            networkManager.StartDisconnected();

            while (true)
            {
                await Task.Delay(100);
                if (networkManager.endDisconnected)
                {
                    _pauseMenu.SetActive(false);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                    Time.timeScale = 1f;
                    return;
                }
            }
        }
    }
    public async void BackMainMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            var entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
            entityManager.DestroyEntity(entityManager.UniversalQuery);
            _pauseMenu.SetActive(false);
            SceneManager.LoadScene(0);

            Time.timeScale = 1f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            networkManager.StartDisconnected();

            while (true)
            {
                await Task.Delay(100);
                if (networkManager.endDisconnected)
                {
                    _pauseMenu.SetActive(false);
                    SceneManager.LoadScene(0);

                    Time.timeScale = 1f;
                    return;
                }
            }
        }
    }

    //Music
    public void ToggleMusic() // Off On
    {
        AudioManager.Instance?.ToggleMusic(_musicCross);
    }
    public void ToggleSFX() // Off On
    {
        AudioManager.Instance?.ToggleSFX(_sfxCross);
    }
    public void MusicVolume() // Volume Music
    {
        AudioManager.Instance?.MusiceVolume(_musicSlider.value);
    }
    public void SFXVolume() // Volume Music
    {
        AudioManager.Instance?.SFXVolume(_sfxSlider.value);
    }
}
