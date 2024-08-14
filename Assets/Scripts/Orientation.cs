using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Orientation : MonoBehaviour
{
    public GameObject _pauseMenu;

    public void BackMainMenu()
    {
        var entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
        entityManager.DestroyEntity(entityManager.UniversalQuery);
        _pauseMenu.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
