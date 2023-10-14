using System;
using Cinemachine;
using Player;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;
using Utilities.Console;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerManager playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform parentObject;
    [SerializeField] private CinemachineFreeLook playerFollowCamera;
    [SerializeField] private PlayerInput playerInput;

    private PlayerManager _player;
    
    public static Action<Transform> OnPlayerCreate;

    public PlayerManager GetPlayer() => _player;
    public CinemachineFreeLook GetPlayerCam() => playerFollowCamera;

    protected override void Awake()
    {
        base.Awake();
        GameSettings.SetUpSettingsDictionaries();
        Application.targetFrameRate = 120;
    }

    private void Start()
    {
        _player = CreatePlayer();
    }

    private PlayerManager CreatePlayer()
    {
        var go = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity, parentObject);
        playerFollowCamera.Follow = go.GetCameraFollow;
        playerFollowCamera.LookAt = go.GetCameraLookAt;

        go.SetPlayerInput(playerInput);

        OnPlayerCreate?.Invoke(go.transform);

        return go.GetComponent<PlayerManager>();
    }
}