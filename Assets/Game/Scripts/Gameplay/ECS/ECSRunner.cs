using System;
using Core.Scripts.Loggers;
using Game.Scripts.Gameplay.Data;
using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Damage;
using Game.Scripts.Gameplay.ECS.Damage.Components;
using Game.Scripts.Gameplay.ECS.Dead;
using Game.Scripts.Gameplay.ECS.Destroy;
using Game.Scripts.Gameplay.ECS.Input;
using Game.Scripts.Gameplay.ECS.Move;
using Game.Scripts.Gameplay.ECS.Portal;
using Game.Scripts.Gameplay.ECS.Rigidbody;
using Game.Scripts.Gameplay.ECS.Rotate;
using Game.Scripts.Gameplay.ECS.SendMessage;
using Game.Scripts.Gameplay.ECS.Slap;
using Game.Scripts.Gameplay.ECS.Spawn;
using Leopotam.Ecs;
#if UNITY_EDITOR
using Leopotam.Ecs.UnityIntegration;
#endif
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS
{
  public class ECSRunner : MonoBehaviour
  {
    [SerializeField] private CameraView mainCamera;
    
    private EcsWorld _world;
    private EcsSystems _systems;
    private EcsSystems _physicSystems;
    
    private bool _hasException;

    private void Awake()
    {
      _world = new EcsWorld();

#if UNITY_EDITOR
      EcsWorldObserver.Create(_world);
#endif

      _systems = new EcsSystems(_world);
      _systems
        .Add(new InputFeature())
        .Add(new SpawnFeature())
        .Add(new MoveFeature())
        .Add(new RotateFeature())
        //.Add(new JumpFeature())
        .Add(new SlapFeature())
        .Add(new DamageFeature())
        .Add(new PortalFeature())
        .Add(new SendMessageFeature())
        .Add(new DestroyFeature())
        .Add(new DeadFeature())
        .OneFrame<PlayerChangeEvent>()
        .OneFrame<DamageEvent>()
        .OneFrame<RestartEvent>()
        .OneFrame<OnCollisionEnterEvent>()
        .OneFrame<OnCollisionStayEvent>()
        .OneFrame<OnCollisionExitEvent>()
        .Inject(mainCamera)
        .ConvertScene()
        .Init();

      _physicSystems = new EcsSystems(_world);
      _physicSystems
        .Add(new RigidbodyFeature())
        .Init();

#if UNITY_EDITOR
      EcsSystemsObserver.Create(_systems);
#endif
    }

    private void Update()
    {
      if (_hasException)
        return;

      try
      {
        _systems?.Run();
      }
      catch (Exception e)
      {
        _hasException = true;
        ECSLogger.ShowLogs();
        throw new UnityException(e.ToString());
      }
    }

    private void FixedUpdate()
    {
      if (_hasException)
        return;

      try
      {
        _physicSystems?.Run();
      }
      catch (Exception e)
      {
        _hasException = true;
        ECSLogger.ShowLogs();
        throw new UnityException(e.ToString());
      }
    }

    private void OnDestroy()
    {
      _systems?.Destroy();
      _world?.Destroy();
    }
  }
}