using Game.Scripts.Infrastructure.States;
using UniRx;

namespace Game.Scripts.UI
{
  public class BattleGUIModel : GUIModel
  {
    public readonly ReactiveProperty<bool> ShowRestartBtn = new();
    
    private readonly ArenaGameState _arenaGameState;

    public BattleGUIModel(ArenaGameState arenaGameState)
    {
      _arenaGameState = arenaGameState;
    }

    public void SetRestartBtnActive(bool isActive)
    {
      ShowRestartBtn.Value = isActive;
    }
    
    public void RestartClicked()
    {
      
    }
  }
}