using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour
{
    public List<MonoBehaviour> ApplyShootActions = new List<MonoBehaviour>();
    public MonoBehaviour ShootAction;
    public MonoBehaviour ReloadAction;
    public MonoBehaviour AmmoAction;
    public MonoBehaviour AimAction;

    public List<MonoBehaviour> BillboardActions = new List<MonoBehaviour>();
    private Billboard[] billboards;

    public float speed;
    public float runSpeed;
    public string moveAnimHash;
    public string moveAnimSpeedHash;
    public string shootAnimHash;
    public string reloadAnimHash;
    public string dieAnimHash;

    [HideInInspector] public InputData inputData = new InputData();
    [HideInInspector] public MoveData moveData = new MoveData();
    [HideInInspector] public ShootData shootData = new ShootData();
    [HideInInspector] public AimData aimData = new AimData();
    [HideInInspector] public RunData runData = new RunData();
    [HideInInspector] public bool isTestRun;

    private void Start()
    {
        Application.targetFrameRate = 60;

        billboards = GameObject.FindObjectsOfType<Billboard>();
        foreach (var billboard in billboards)
        {
            BillboardActions.Add(billboard);
        }

        runData = new RunData { _runSpeed = runSpeed, _speed = speed, _isTestRun = isTestRun };
    }
}

public struct InputData
{
    public float2 Move;
    public float Shoot;
    public float Pause;
    public float Reload;
    public float ChangeMat;
    public int CollideInput;
}

public struct MoveData
{
    public float Speed;
}

public struct ShootData
{

}
public struct AimData
{

}

public struct RunData
{
    public float _runSpeed;
    public float _speed;
    public bool _isTestRun;
}