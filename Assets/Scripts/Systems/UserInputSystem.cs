using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using Unity.Entities;

public class UserInputSystem : ComponentSystem
{
    public EntityQuery _inputQuery;

    public InputAction _moveAcion;
    public InputAction _shootAction;
    public InputAction _pauseAction;
    public InputAction _reloadAction;

    private float2 _moveInput;
    private float _shootInput;
    private float _pouseInput;
    public float _reloadInput;

    protected override void OnCreate()
    {
        _inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<UserInputData>());
    }

    protected override void OnStartRunning()
    {
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
        _moveAcion.Enable();

        _shootAction = new InputAction(name: "shoot", binding: "<Mouse>/leftButton");
        _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.canceled += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.Enable();

        _pauseAction = new InputAction(name: "pouse", binding: "<Keyboard>/escape");
        _pauseAction.performed += context => { _pouseInput = context.ReadValue<float>(); };
        _pauseAction.started += context => { _pouseInput = context.ReadValue<float>(); };
        _pauseAction.canceled += context => { _pouseInput = context.ReadValue<float>(); };
        _pauseAction.Enable();

        _reloadAction = new InputAction(name: "reload", binding: "<Mouse>/rightButton");
        _reloadAction.performed += context => { _reloadInput = context.ReadValue<float>(); };
        _reloadAction.started += context => { _reloadInput = context.ReadValue<float>(); };
        _reloadAction.canceled += context => { _reloadInput = context.ReadValue<float>(); };
        _reloadAction.Enable();
    }

    protected override void OnStopRunning()
    {
        _moveAcion.Disable();
        _shootAction.Disable();
        _pauseAction.Disable();
        _reloadAction.Disable();
    }

    protected override void OnUpdate()
    {
        Entities.With(_inputQuery).ForEach((Entity entity, ref InputData inputData) =>
        {
            inputData.Move = _moveInput;
            inputData.Shoot = _shootInput;
            inputData.Pause = _pouseInput;
            inputData.Reload = _reloadInput;
        });
    }
}
