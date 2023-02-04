using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion

    private Camera mainCamera;

    private TouchAction touchAction;

    private void Awake() {
        touchAction = new TouchAction();
        mainCamera = Camera.main;
    }

    private void OnEnable() {
        touchAction.Enable();
    }

    private void OnDisable() {
        touchAction.Disable();
    }

    void Start()
    {
        touchAction.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        touchAction.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context) {
        if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, touchAction.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context) {
        if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, touchAction.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }

    public Vector2 PrimaryPosition() {
        return Utils.ScreenToWorld(mainCamera, touchAction.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
