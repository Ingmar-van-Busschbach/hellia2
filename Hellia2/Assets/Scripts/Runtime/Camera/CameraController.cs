using System;
using Runtime.Blocks;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float movementSpeedMultiplier;
    [SerializeField] private float horizontalLookAtSpeedMultiplier;
    [SerializeField] private float verticalLookAtSpeedMultiplier;

    private Vector3 _previousOffset;
    private PlayerBlock _playerBlock;
    private Transform _cachedTransform;

    private void Awake()
    {
        _cachedTransform = transform;

        _playerBlock = FindObjectOfType<PlayerBlock>();
        Vector3 playerPos = _playerBlock.transform.position;

        _cachedTransform.position = playerPos + offset;
        _cachedTransform.LookAt(playerPos);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _previousOffset = offset;
            offset.x = _previousOffset.z;
            offset.z = -_previousOffset.x;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _previousOffset = offset;
            offset.x = -_previousOffset.z;
            offset.z = _previousOffset.x;
        }
    }

    private void LateUpdate()
    {
        var playerPosition = _playerBlock.transform.position;
        var myPosition = _cachedTransform.position;

        _cachedTransform.position = Vector3.Slerp(myPosition, playerPosition + offset, Time.deltaTime * movementSpeedMultiplier);

        var rotation = _cachedTransform.rotation;
        
        Quaternion verticalLookOn = Quaternion.LookRotation(playerPosition - myPosition);
        verticalLookOn.x = rotation.x;
        verticalLookOn.z = rotation.z;
        Quaternion horizontalLookOn = Quaternion.LookRotation(playerPosition - myPosition);
        verticalLookOn.y = rotation.y;
        
        rotation = Quaternion.Lerp(rotation, horizontalLookOn, Time.deltaTime * horizontalLookAtSpeedMultiplier);
        rotation = Quaternion.Lerp(rotation, verticalLookOn, Time.deltaTime * verticalLookAtSpeedMultiplier);
        _cachedTransform.rotation = rotation;
    }
}