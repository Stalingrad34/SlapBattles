using Game.Scripts.Infrastructure.States;
using UniRx;

namespace Game.Scripts.UI
{
  public class BattleGUIModel : GUIModel
  {
    public readonly ReactiveProperty<bool> ShowRestartBtn = new();
    
    private readonly AbstractGameState _standardGameState;

    public BattleGUIModel(AbstractGameState standardGameState)
    {
      _standardGameState = standardGameState;
    }

    public void SetRestartBtnActive(bool isActive)
    {
      ShowRestartBtn.Value = isActive;
    }
    
    public void RestartClicked()
    {
      _standardGameState.Restart();
    }
  }
}