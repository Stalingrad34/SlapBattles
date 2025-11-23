using Game.Scripts.Gameplay.Data.Units;
using Game.Scripts.Gameplay.ECS.Spawn.Components;
using Game.Scripts.Infrastructure;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Spawn.Systems
{
  public class SpawnEnemySystem : IEcsRunSystem
  {
    private EcsFilter<SpawnEnemyEvent> _eventFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        var data = new UnitData()
        {
          Id = _eventFilter.Get1(i).Id,
          Speed = _eventFilter.Get1(i).Player.speed
        };
        
        var unitView = AssetProvider.GetEnemyView();
        unitView.Setup(data);
        unitView.transform.position = _eventFilter.Get1(i).Position;
      }
    }
  }
}