using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public ApplyPlayerAnimDirection playerAnimDirection;
    public ShootAbility shootAbility;

    private PlayerSelectoer playerSelector;

    private void Awake()
    {
        playerSelector = GetComponent<PlayerSelectoer>();

        UpdateStateValues();
    }
    public void UpdateStateValues()
    {
        for (int i = 0; i < playerSelector.playerStates.Length; i++)
        {
            UpdateScripts(i);
            playerSelector.playerStates[i].featuresValues[0] = playerHealth.Value();
            playerSelector.playerStates[i].featuresValues[1] = (int)playerAnimDirection.Value();
            playerSelector.playerStates[i].featuresValues[2] = (int)shootAbility.Value();
        }
    }
    private void UpdateScripts(int index)
    {
        playerHealth = playerSelector.players[index].GetComponent<PlayerHealth>();
        playerAnimDirection = playerSelector.players[index].GetComponent<ApplyPlayerAnimDirection>();
        shootAbility = playerSelector.players[index].GetComponent<ShootAbility>();
    }
}
