//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.3
//     from Assets/Action Inputs/ControllAction.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @ControllAction : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControllAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControllAction"",
    ""maps"": [
        {
            ""name"": ""CustomPlayer"",
            ""id"": ""041c7be2-efe8-4ada-85c8-8b046fa6b711"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""ae81672b-7e24-42be-a04e-835c62ce8b02"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""6195cc81-fe2e-4f64-9123-cf7c5eb8b773"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""52532eae-a1cf-4d58-b010-766150ff4c35"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""58fa8ec0-e0de-484a-ac12-b5140cddf848"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""74073dc2-aa25-45e1-993e-d9f230c18a3b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""DebugState"",
                    ""type"": ""Button"",
                    ""id"": ""4594f350-cd0b-489c-b587-82b36bd15bd0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""47a7c3d9-5358-4055-92ef-fddc2770dd4d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48431c20-055d-4e50-a9dd-f85b84e0cd4e"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""01527b8a-45e9-478c-a483-2c6675cbc098"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e196688c-0cd4-4dbe-8d6a-acc29806d48e"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83a82200-f700-41a4-ab7e-1c8762c95179"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12e5ab54-52af-41cf-b7b9-348b35069968"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""968829e0-5ead-4985-a406-24e9fc77b8ca"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=1000,y=50)"",
                    ""groups"": ""XboxController"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e3ccd64-a2a5-4959-999f-42f48432b343"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D KeyMove"",
                    ""id"": ""08875426-c687-478f-ae42-1ae63a4e1c4b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6a056938-e527-4544-87b1-8a22ca97f848"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1c423428-c2e6-4f7e-a9d4-07b565879248"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""180f23d0-e392-4dc1-9602-bae58fc98c8d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""90611f24-133e-4e1e-847c-e1abaeea2b05"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ce005f36-2823-42c8-9127-cda4ba64a860"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b9b296c-3dce-4895-b7e7-5ab6822ce96f"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DebugState"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CustomUI"",
            ""id"": ""4a74f7fd-b8ec-47cb-94a8-8dbd30e1f25a"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""015a06b5-70ce-49bf-9fcb-2778fcb08c99"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""37046476-b612-4f87-8e3d-1ca5c82e9e60"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XboxController"",
            ""bindingGroup"": ""XboxController"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // CustomPlayer
        m_CustomPlayer = asset.FindActionMap("CustomPlayer", throwIfNotFound: true);
        m_CustomPlayer_Movement = m_CustomPlayer.FindAction("Movement", throwIfNotFound: true);
        m_CustomPlayer_Jump = m_CustomPlayer.FindAction("Jump", throwIfNotFound: true);
        m_CustomPlayer_Grab = m_CustomPlayer.FindAction("Grab", throwIfNotFound: true);
        m_CustomPlayer_Crouch = m_CustomPlayer.FindAction("Crouch", throwIfNotFound: true);
        m_CustomPlayer_Look = m_CustomPlayer.FindAction("Look", throwIfNotFound: true);
        m_CustomPlayer_DebugState = m_CustomPlayer.FindAction("DebugState", throwIfNotFound: true);
        // CustomUI
        m_CustomUI = asset.FindActionMap("CustomUI", throwIfNotFound: true);
        m_CustomUI_Pause = m_CustomUI.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // CustomPlayer
    private readonly InputActionMap m_CustomPlayer;
    private ICustomPlayerActions m_CustomPlayerActionsCallbackInterface;
    private readonly InputAction m_CustomPlayer_Movement;
    private readonly InputAction m_CustomPlayer_Jump;
    private readonly InputAction m_CustomPlayer_Grab;
    private readonly InputAction m_CustomPlayer_Crouch;
    private readonly InputAction m_CustomPlayer_Look;
    private readonly InputAction m_CustomPlayer_DebugState;
    public struct CustomPlayerActions
    {
        private @ControllAction m_Wrapper;
        public CustomPlayerActions(@ControllAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_CustomPlayer_Movement;
        public InputAction @Jump => m_Wrapper.m_CustomPlayer_Jump;
        public InputAction @Grab => m_Wrapper.m_CustomPlayer_Grab;
        public InputAction @Crouch => m_Wrapper.m_CustomPlayer_Crouch;
        public InputAction @Look => m_Wrapper.m_CustomPlayer_Look;
        public InputAction @DebugState => m_Wrapper.m_CustomPlayer_DebugState;
        public InputActionMap Get() { return m_Wrapper.m_CustomPlayer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CustomPlayerActions set) { return set.Get(); }
        public void SetCallbacks(ICustomPlayerActions instance)
        {
            if (m_Wrapper.m_CustomPlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnJump;
                @Grab.started -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnGrab;
                @Grab.performed -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnGrab;
                @Grab.canceled -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnGrab;
                @Crouch.started -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnCrouch;
                @Look.started -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnLook;
                @DebugState.started -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnDebugState;
                @DebugState.performed -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnDebugState;
                @DebugState.canceled -= m_Wrapper.m_CustomPlayerActionsCallbackInterface.OnDebugState;
            }
            m_Wrapper.m_CustomPlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Grab.started += instance.OnGrab;
                @Grab.performed += instance.OnGrab;
                @Grab.canceled += instance.OnGrab;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @DebugState.started += instance.OnDebugState;
                @DebugState.performed += instance.OnDebugState;
                @DebugState.canceled += instance.OnDebugState;
            }
        }
    }
    public CustomPlayerActions @CustomPlayer => new CustomPlayerActions(this);

    // CustomUI
    private readonly InputActionMap m_CustomUI;
    private ICustomUIActions m_CustomUIActionsCallbackInterface;
    private readonly InputAction m_CustomUI_Pause;
    public struct CustomUIActions
    {
        private @ControllAction m_Wrapper;
        public CustomUIActions(@ControllAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_CustomUI_Pause;
        public InputActionMap Get() { return m_Wrapper.m_CustomUI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CustomUIActions set) { return set.Get(); }
        public void SetCallbacks(ICustomUIActions instance)
        {
            if (m_Wrapper.m_CustomUIActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_CustomUIActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_CustomUIActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_CustomUIActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_CustomUIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public CustomUIActions @CustomUI => new CustomUIActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_XboxControllerSchemeIndex = -1;
    public InputControlScheme XboxControllerScheme
    {
        get
        {
            if (m_XboxControllerSchemeIndex == -1) m_XboxControllerSchemeIndex = asset.FindControlSchemeIndex("XboxController");
            return asset.controlSchemes[m_XboxControllerSchemeIndex];
        }
    }
    public interface ICustomPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnDebugState(InputAction.CallbackContext context);
    }
    public interface ICustomUIActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
}
