using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.0125f;
    public Vector3 offset;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            FindPlayer();
            return;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    public void FixedUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
    private async void FindPlayer()
    {
        GameObject Player = null;
        while (Player == null)
        {
            await Task.Delay(500);
            Player = PlayerMultiplayerData.Instance.Player;
        }
        target = Player.transform;
        return;
    }
}
