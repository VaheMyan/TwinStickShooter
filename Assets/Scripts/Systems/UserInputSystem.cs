using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using Unity.Entities;

public class UserInputSystem : ComponentSystem
{
    public EntityQuery _inputQuery;

    public InputAction _moveAcion; // sa sharjvelu gortsoghutyuny
    public InputAction _shootAction; // sa krakelu gortsoghutyuny
    public InputAction _runAction;

    private float2 _moveInput;
    private float _shootInput;
    public float _runInput;

    protected override void OnCreate()
    {
        _inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<UserInputData>());
    }

    protected override void OnStartRunning() // stex avelacnum es knopkeqy vory inch petq a ani
    {
        // OnStartRunning() - es ashxatum a erb hamakargy sksum a ashxatel

        // Joystik
        _moveAcion = new InputAction(name: "move", binding: "<Gamepad>/rightStick");
        _moveAcion.AddCompositeBinding("Dpad")
            .With(name: "Up", binding: "<Keyboard>/w")
            .With(name: "Down", binding: "<Keyboard>/s")
            .With(name: "Left", binding: "<Keyboard>/a")
            .With(name: "Right", binding: "<Keyboard>/d");

        _moveAcion.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAcion.started += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAcion.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAcion.Enable(); // miacvum a _moveAcion-i ashxatanqy

        _shootAction = new InputAction(name: "shoot", binding: "<Mouse>/leftButton"); // stex asum es vor _shootAction = new InputAction
        // binding: "<Keyboard>/Space" krakelu knopkai vra script ka ytex yntrum es vor knpokeqy isk stex kanchum es dranq "<Keyboard>/Space"
        _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.canceled += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.Enable(); // miacvum a _shootAction-i ashxatanqy

        _runAction = new InputAction(name: "run", binding: "<Keyboard>/LeftShift");
        _runAction.performed += context => { _runInput = context.ReadValue<float>(); };
        _runAction.started += context => { _runInput = context.ReadValue<float>(); };
        _runAction.canceled += context => { _runInput = context.ReadValue<float>(); };
        _runAction.Enable();
    }

    protected override void OnStopRunning() // OnStopRunning() - es ashxatum a erb hamakargy dadarum a ashxatel
    {
        _moveAcion.Disable(); // kangnecvum a _moveAcion-i ashxatanqy
        _shootAction.Disable(); // kangnecvum a _shootAction-i ashxatanqy
    }

    protected override void OnUpdate()
    {
        Entities.With(_inputQuery).ForEach((Entity entity, ref InputData inputData, PlayerHealth playerHealth) => // kap a hastatvum InputData script-i het
        {
            inputData.Move = _moveInput;
            inputData.Shoot = _shootInput;
        });
    }
}
