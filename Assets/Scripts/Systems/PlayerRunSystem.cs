using UnityEngine;
using Unity.Entities;

public class PlayerRunSystem : ComponentSystem
{
    private EntityQuery _runQuery;

    //bool startRuning = true;
    //bool stopRuning = false;
    //private bool _runBool;
    //private bool isBigSpeed = false;

    protected override void OnCreate()
    {
        _runQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<UserInputData>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_runQuery).ForEach((Entity entity, UserInputData userInputData, ref MoveData moveData, ref InputData inputData, ref RunData runData) =>
        {
            //float speedChange = _runBool ? -runData._runSpeed : runData._runSpeed;

            //moveData.Speed = runData._speed;

            //if (stopRuning == false && inputData.Run == 1 || userInputData.isTestRun)
            //{
            //    _runBool = true;
            //    runData._speed += speedChange;
            //    startRuning = false;
            //    stopRuning = true;

            //    if (userInputData.isTestRun)
            //    {
            //        isBigSpeed = true;
            //        userInputData.isTestRun = false;
            //    }
            //}

            //if (stopRuning == true && inputData.Run == 0 && startRuning == false || isBigSpeed)
            //{
            //    _runBool = false;
            //    runData._speed += speedChange;
            //    startRuning = true;
            //    stopRuning = false;

            //    isBigSpeed = false;
            //}

        });
    }


}