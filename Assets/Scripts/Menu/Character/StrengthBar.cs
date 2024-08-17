using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrengthBar : MonoBehaviour
{
    public GameObject strengthBlockPrefab;
    public Transform blocksContainer;
    public int maxBlocks = 13;
    private int currentStrength;

    void Start()
    {
        currentStrength = maxBlocks;
        GenerateBlocks();
    }

    void GenerateBlocks()
    {
        foreach (Transform child in blocksContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentStrength; i++)
        {
            Instantiate(strengthBlockPrefab, blocksContainer);
        }
    }

    public void UpdateStrength(int newStrength)
    {
        currentStrength = Mathf.Clamp(newStrength, 0, maxBlocks);
        GenerateBlocks();
    }
}
