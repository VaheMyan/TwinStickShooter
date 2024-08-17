using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;

    public int Coins;
    public Text coinText;

    //Menu
    [SerializeField] private GameObject _playerSelectMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private GameObject _musicCross;
    [SerializeField] private GameObject _sfxCross;

    private void Start()
    {
        UpdateCoinText(PlayerPrefs.GetInt("PlayerCoins"));

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
        _mainMenu.SetActive(false);
        _playerSelectMenu.SetActive(true);
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
    public void BackToMainMenuFromPlayerSelection()
    {
        _playerSelectMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }
    public void Reset()
    {
        var entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
        entityManager.DestroyEntity(entityManager.UniversalQuery);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1f;
    }
    public void UpdateCoinText(int _coin)
    {
        coinText.text = _coin.ToString();
        Coins = _coin;
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
