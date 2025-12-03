using Game.Scripts.Gameplay.Data;
using Game.Scripts.Gameplay.Data.Units;
using Game.Scripts.Gameplay.ECS.Spawn.Components;
using Game.Scripts.Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Spawn.Systems
{
  public class SpawnPotatoSystem : IEcsRunSystem
  {
    private EcsFilter<SpawnPotatoEvent> _eventFilter;
    private CameraView _mainCamera;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        var data = new UnitData()
        {
          Id = _eventFilter.Get1(i).Id,
          Speed = _eventFilter.Get1(i).Player.speed
        };

        UnitView potatoView;
        if (_eventFilter.Get1(i).IsPlayer)
        {
          var unitView = AssetProvider.GetPlayerView(_eventFilter.Get1(i).PrefabPath);
          unitView.SetCameraPoint(_mainCamera);
          potatoView = unitView;
        }
        else
        {
          var enemyView = AssetProvider.GetEnemyView(_eventFilter.Get1(i).PrefabPath);
          potatoView = enemyView;
        }
        
        potatoView.Setup(data);
        potatoView.transform.position = _eventFilter.Get1(i).Position;
        potatoView.transform.rotation = _eventFilter.Get1(i).Rotation;
      }
    }
  }
}