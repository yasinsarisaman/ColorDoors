using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events;
using ColorDoors.Scripts.Events.Doors;
using ColorDoors.Scripts.Events.Player;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private VariableJoystick _joystick;

    private Animator characterAnimator;

    private Vector3 _playerInput;
    private const float GROUND_ELEVATION = 0.2f;
    private bool _isFirstInputReceived = false;

    private void Start()
    {
         characterAnimator = this.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventBus<TeleportPlayer>.AddListener(OnTeleportation);
        EventBus<BoostPlayerSpeed>.AddListener(OnBoostPlayer);
    }

    private void OnDisable()
    {
        EventBus<TeleportPlayer>.RemoveListener(OnTeleportation);
        EventBus<BoostPlayerSpeed>.RemoveListener(OnBoostPlayer);
    }

    private void Update()
    {
        GatherInputsFromJoyStick();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision other)
    {
        /* Collision with a red door */
        if (other.gameObject.TryGetComponent(out RedDoor redDoor)) 
        {
            EventBus<IDoorStatusChangedEvent>.Emit(this, new RedDoorStatusChangedEvent(redDoor.doorId,redDoor.isOpened));
        }
        
        /* Collision with a blue door */
        if (other.gameObject.TryGetComponent(out BlueDoor blueDoor))
        {
            EventBus<IDoorStatusChangedEvent>.Emit(this, new BlueDoorStatusChangedEvent(blueDoor.doorId));
        }
        
        /* Collision with a green door */
        if (other.gameObject.TryGetComponent(out GreenDoor greenDoor))
        {
            EventBus<IDoorStatusChangedEvent>.Emit(this, new GreenDoorStatusChangedEvent(greenDoor.doorId, greenDoor.doorAdditionalTime, !greenDoor.isOpen));
        }
        
        /* Collision with a purple door */
        if (other.gameObject.TryGetComponent(out PurpleDoor purpleDoor))
        {
            EventBus<IDoorStatusChangedEvent>.Emit(this, new PurpleDoorStatusChangedEvent(purpleDoor.doorId, purpleDoor.doorFreezeTime, !purpleDoor.isOpen));
        }
        
        /* Collision with a orange door */
        if (other.gameObject.TryGetComponent(out OrangeDoor orangeDoor))
        {
            EventBus<IDoorStatusChangedEvent>.Emit(this, new OrangeDoorStatusChangedEvent(orangeDoor.doorId));
        }
        
        /* Collision with a white door */
        if (other.gameObject.TryGetComponent(out WhiteDoor whiteDoor))
        {
            EventBus<IDoorStatusChangedEvent>.Emit(this, new WhiteDoorStatusChangedEvent(whiteDoor.doorId));
        }
        
        /* Collision with a speed door */
        if (other.gameObject.TryGetComponent(out SpeedDoor speedDoor))
        {
            EventBus<IDoorStatusChangedEvent>.Emit(this, new SpeedDoorStatusChangedEvent(speedDoor.doorId, speedDoor.boostFactor));
        }
        
        /* Collision with finish door */
        if (other.gameObject.TryGetComponent(out FinishDoor finishDoor))
        {
            EventBus<FinishDoorStatusChangedEvent>.Emit(this, new FinishDoorStatusChangedEvent());
        }
    }
    
    void GatherInputsFromJoyStick()
    {
        _playerInput = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        if (!_isFirstInputReceived && _playerInput != Vector3.zero)
        {
            _isFirstInputReceived = true;
            EventBus<FirstInputReceivedEvent>.Emit(this, new FirstInputReceivedEvent());
        }
    }

    void Look()
    {
        if (_playerInput != Vector3.zero)
        {
            var relative = (transform.position + _playerInput.ToIsometric()) - transform.position;
            var rotation = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 360);
        }
    }

    void Move()
    {
        
        characterAnimator.SetFloat("CharacterSpeed", math.abs(_playerInput.magnitude)/2);
        _playerRb.MovePosition(transform.position + transform.forward * (_playerInput.magnitude * _playerSpeed * Time.deltaTime));
    }
    
    /* Callbacks */
    private void OnTeleportation(object sender, TeleportPlayer teleportPlayerEvent)
    {
        //Vector3 teleportPosition = teleportPlayerEvent.TransformToTeleport.position;
        //Vector3 newPositionWithOffset = new Vector3(teleportPosition.x , transform.position.y, teleportPosition.z) + (new Vector3(transform.forward.x, GROUND_ELEVATION, transform.forward.z) * teleportPlayerEvent.DoorOffset);
        
        Vector3 teleportPosition = teleportPlayerEvent.TransformToTeleport.position;
        Vector3 newPositionWithOffset = teleportPosition +
                                        teleportPlayerEvent.TransformToTeleport.forward *
                                        teleportPlayerEvent.DoorOffset;
        transform.SetPositionAndRotation(new Vector3(newPositionWithOffset.x, GROUND_ELEVATION, newPositionWithOffset.z), teleportPlayerEvent.TransformToTeleport.rotation);
    }

    private void OnBoostPlayer(object sender, BoostPlayerSpeed boostPlayerEvent)
    {
        _playerSpeed *= boostPlayerEvent.BoostFactor;
    }
}
