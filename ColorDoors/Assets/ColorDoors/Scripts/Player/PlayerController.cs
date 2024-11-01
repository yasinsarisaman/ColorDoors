using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events;
using ColorDoors.Scripts.Events.Player;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private DynamicJoystick _joystick;

    private Vector3 _playerInput;
    private const float GROUND_ELEVATION = 0.2f;

    private void OnEnable()
    {
        EventBus<TeleportPlayer>.AddListener(OnTeleportation);
    }

    private void OnDisable()
    {
        EventBus<TeleportPlayer>.RemoveListener(OnTeleportation);
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
            EventBus<RedDoorStatusChangedEvent>.Emit(this, new RedDoorStatusChangedEvent(redDoor.doorId,redDoor.isOpened));
        }
        
        /* Collision with a blue door */
        if (other.gameObject.TryGetComponent(out BlueDoor blueDoor))
        {
            EventBus<BlueDoorStatusChangedEvent>.Emit(this, new BlueDoorStatusChangedEvent(blueDoor.doorId));
        }
    }

    void GatherInputsFromJoyStick()
    {
        _playerInput = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
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
        _playerRb.MovePosition(transform.position + transform.forward * (_playerInput.magnitude * _playerSpeed * Time.deltaTime));
    }
    
    /* Callbacks */
    private void OnTeleportation(object sender, TeleportPlayer teleportPlayerEvent)
    {
        Vector3 teleportPosition = teleportPlayerEvent.TransformToTeleport.position;
        Vector3 newPositionWithOffset = new Vector3(teleportPosition.x , GROUND_ELEVATION, teleportPosition.z) + (new Vector3(transform.forward.x, GROUND_ELEVATION, transform.forward.z) * teleportPlayerEvent.DoorOffset);
        transform.SetPositionAndRotation(newPositionWithOffset, teleportPlayerEvent.TransformToTeleport.rotation);
    }

}
