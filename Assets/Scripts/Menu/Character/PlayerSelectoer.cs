using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class PlayerState
{
    public int[] featuresValues;
}

public class PlayerSelectoer : MonoBehaviour
{
    public Sprite[] playersSprites;
    public GameObject[] players;
    public Image playerImage;
    public int selectedPlayer = 0;

    public PlayerState[] playerStates;
    public StrengthBar[] strengthBars;

    private void Start()
    {
        selectedPlayer = PlayerPrefs.GetInt("selectedPlayer");
        playerImage.sprite = playersSprites[selectedPlayer];
        UpdatePlayerState(selectedPlayer);
    }
    public void NextCharacter()
    {
        selectedPlayer = (selectedPlayer + 1) % players.Length;
        playerImage.sprite = playersSprites[selectedPlayer];

        UpdatePlayerState(selectedPlayer);
    }
    public void PreviouseCharacter()
    {
        selectedPlayer--;
        if (selectedPlayer < 0)
        {
            selectedPlayer += players.Length;
        }
        playerImage.sprite = playersSprites[selectedPlayer];

        UpdatePlayerState(selectedPlayer);
    }

    //State
    private void UpdatePlayerState(int index)
    {
        for (int i = 0; i < strengthBars.Length; i++)
        {
            strengthBars[i].UpdateStrength(playerStates[index].featuresValues[i]);
        }
    }
    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedPlayer", selectedPlayer);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
