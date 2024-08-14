using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
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

    private void Start()
    {
        MusicVolume();
        SFXVolume();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log(SceneManager.GetActiveScene());
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
        Time.timeScale = AudioManager.Instance.ToFloat(isPause);
        _pauseMenu.SetActive(!isPause);
        isPause = !isPause;
    }
    public void Continue()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Reset()
    {
        var entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
        entityManager.DestroyEntity(entityManager.UniversalQuery);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1f;
    }
    public void BackMainMenu()
    {
        var entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
        entityManager.DestroyEntity(entityManager.UniversalQuery);
        _pauseMenu.SetActive(false);
        SceneManager.LoadScene(0);

        Time.timeScale = 1f;
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
