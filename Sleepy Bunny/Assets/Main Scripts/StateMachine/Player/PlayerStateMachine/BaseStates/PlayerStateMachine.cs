using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using PlayerStM.BaseStates;
using PlayerStM.SubStates;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerStM.BaseStates
{
    /// <summary>
    /// This is the Main class of the player state machine where all the magic happens.
    /// <br></br>
    /// If you want more info refer to
    /// <see href="https://docs.google.com/document/d/1aFpvd6ApL2zObopLuXXM9AEezrWgWXJt30wbKSJnSlc/edit?usp=sharing">
    /// Programming Design Doc
    /// </see>.
    /// <br></br>
    /// StateFactory.cs and BasePlayerState.cs are the other two classes that's important
    /// </summary>
    public class PlayerStateMachine : MonoBehaviour
    {
        // Script that contains all the variables.
        private PlayerVariables _v;

        public PlayerVariables Variables => _v;

        private void Awake()
        {
            _v = GetComponent<PlayerVariables>();

            _v._gameMaster = FindObjectOfType<GameMaster>();

            _v._theInput = FindObjectOfType<InputScript>();

            _v.thePlayerInput = new ControllAction();

            _v._playerAnimator = GetComponentInChildren<Animator>();
            _v._rb = GetComponentInParent<Rigidbody>();

            _v._stateFactory = new StateFactory(this, _v);
            _v._currentSuper = _v._stateFactory.SuperGrounded();
            _v._currentSuper.EnterState();

            _v._mainCamera = Camera.main;
        }

        private void Start()
        {
            SetRaycastVectors();

            Physics.gravity = new Vector3(0, _v.Gravity + _v._gravityModifier, 0);

            if (_v._useDefaultGroundLayer)
            {
                _v._groundLayer = LayerMask.GetMask("Ground") + LayerMask.GetMask("Default");
            }

            if (_v._useDefaultInteractionsLayers)
            {
                _v._climbLayer = LayerMask.GetMask("Climbable");
                _v._grabLayer = LayerMask.GetMask("Grabable");
                _v._interactLayer = LayerMask.GetMask("Interactable");
            }
        }

        private void OnEnable()
        {
            InputScript.DebugState += GetCurrentState;

            InputScript.Interact += ClimbGrabInteract;

            Grounded.IsGroundedEvent += GroundedRaycast;
        }

        private void OnDisable()
        {
            InputScript.DebugState -= GetCurrentState;

            InputScript.Interact -= ClimbGrabInteract;

            Grounded.IsGroundedEvent -= GroundedRaycast;
        }

        private void FixedUpdate()
        {
            _v._currentSuper.FixedUpdateStates();
        }

        private void Update()
        {
            _v._currentSuper.UpdateStates();
        }

        /// <summary>
        /// Not in use. Old method for checking if something is grounded.
        /// <br></br>
        /// Does not work
        /// </summary>
        private void CheckGrounded(bool grounded)
        {
            _v._isGrounded = grounded;
        }

        /// <summary>
        /// Press the Select(View on xbox controller) button on the gamepad
        /// <br></br>
        /// to see the current super- and substate in the console
        /// </summary>
        private void GetCurrentState()
        {
            Debug.Log("SuperState " + _v.CurrentSuper);
            Debug.Log("SubState: " + _v.CurrentSub);
        }

        private void SetRaycastVectors()
        {
            // Cardinal directions are relative to the players rotation

            //0
            _v._downVectors.Add(Vector3.down);

            //1 south
            _v._downVectors.Add(Vector3.Lerp
                (Vector3.down, Vector3.back, _v._vectorAngle));

            //2 north
            _v._downVectors.Add(Vector3.Lerp
                (Vector3.down, Vector3.forward, _v._vectorAngle));

            //3 east
            _v._downVectors.Add(Vector3.Lerp
                (Vector3.down, Vector3.right, _v._vectorAngle));

            //4 west
            _v._downVectors.Add(Vector3.Lerp
                (Vector3.down, Vector3.left, _v._vectorAngle));

            //5 north east
            _v._downVectors.Add(Vector3.Lerp
                (_v._downVectors[2], _v._downVectors[3], _v._vectorAngle));

            //6 north west
            _v._downVectors.Add(Vector3.Lerp
                (_v._downVectors[2], _v._downVectors[4], _v._vectorAngle));

            //7 south east
            _v._downVectors.Add(Vector3.Lerp
                (_v._downVectors[1], _v._downVectors[3], _v._vectorAngle));

            //8 south west
            _v._downVectors.Add(Vector3.Lerp
                (_v._downVectors[1], _v._downVectors[4], _v._vectorAngle));

            //0 Forward
            _v._forwardVector.Add(Vector3.forward);

            //1 Forward up
            _v._forwardVector.Add(Vector3.Lerp
                (Vector3.forward, Vector3.up, _v._forwardVAngel));

            //2 Forward right
            _v._forwardVector.Add(Vector3.Lerp
                (Vector3.forward, Vector3.right, _v._forwardVAngel));

            //3 Forward left
            _v._forwardVector.Add(Vector3.Lerp
                (Vector3.forward, Vector3.left, _v._forwardVAngel));
        }

        /// <summary>
        /// Shoots out Rays and that hits specific Layers.
        /// <br></br>
        /// The layers can be specified in the editor under PlayerStateMachine
        /// </summary>
        public void ClimbGrabInteract()
        {
            if (_v._isGrabing)
            {
                _v._isGrabing = false;
                _v._isPulling = false;
                _v._isPushing = false;

                Debug.Log("Restet");
                BasePlayerState.AnimaitonAffected();
                return;
            }

            // The forloop shoots out rays in all direction unitll
            // one hits a object with the correct layer
            for (int i = 0; i < _v._forwardVector.Count; i++)
            {
                Vector3 tempVector =
                    Camera.main.transform.TransformDirection(_v._forwardVector[i]);

                Debug.DrawRay(transform.position, tempVector * 1, Color.green, 2);

                //Grab ray
                if (Physics.Raycast(transform.position, tempVector, out _v.hit,
                    _v._grabRayLength, _v._grabLayer))
                {
                    _v._transformHit = _v.hit.transform;
                    _v._rigidbodyGrabed = _v.hit.transform.GetComponent<Rigidbody>();

                    if (_v.hitPoint != null)
                    {
                        Destroy(_v.hitPoint);
                    }

                    _v.hitPoint = new GameObject();
                    _v.hitPoint.name = "PullPoint";
                    _v.hitPoint.tag = "Move_Object";
                    _v.hitPoint.transform.position = _v.hit.point;
                    _v.hitPoint.transform.parent = _v.hit.transform;
                    _v._pointHit = _v.hitPoint.transform;

                    _v._isPulling = true;
                    _v._isGrabing = true;
                    BasePlayerState.AnimaitonAffected();
                    break;
                }

                //Climb ray
                else if (Physics.Raycast(transform.position, tempVector, out _v.hit,
                _v._climbRayLength, _v._climbLayer))
                {
                    _v._transformHit = _v.hit.transform;
                    Vector3 hitDistance = (transform.position - _v.hit.transform.position).normalized;
                    transform.position = _v.hit.point + hitDistance * _v._hitDistanceModifier;
                    _v._isClimbing = true;
                    BasePlayerState.AnimaitonAffected();
                    break;
                }

                // Interaction Ray
                else if (Physics.Raycast(transform.position, tempVector, out _v.hit,
                    _v._interactRayLength, _v._interactLayer))
                {
                    Debug.Log("Happening");
                    try
                    {
                        _v.hit.transform.gameObject.GetComponent<InteractObject>()
                            .Interacted.Invoke();
                    }
                    catch (Exception)
                    {
                        _v.hit.transform.gameObject.GetComponentInChildren<InteractObject>()
                            .Interacted.Invoke();
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Shoots a multitude of rays down in a cone that
        /// <br></br>
        /// checks for a object with the grounded layer
        /// </summary>
        public void GroundedRaycast()
        {
            for (int i = 0; i < _v._downVectors.Count; i++)
            {
                Debug.DrawRay(transform.position, _v._downVectors[i] * _v._rayGroundDist,
                        Color.red, 1);
                if (Physics.Raycast(transform.position, _v._downVectors[i],
                    _v._rayGroundDist, _v._groundLayer, QueryTriggerInteraction.Collide))
                {
                    _v._isGrounded = true;
                    break;
                }
                else if (i == _v._downVectors.Count - 1)
                {
                    _v._isGrounded = false;
                }
            }
        }

        public void PlayerDied()
        {
            _v.PlayerAnimator.SetTrigger("Dead");
        }

        public void PlayerRespawn()
        {
            _v.PlayerAnimator.ResetTrigger("Dead");
            transform.position = _v._gameMaster.CurrentCheckpointPosition;
        }
    }
}