using Game.Scripts.Infrastructure.States;
using UniRx;

namespace Game.Scripts.UI
{
  public class BattleGUIModel : GUIModel
  {
    public readonly ReactiveProperty<bool> ShowRestartBtn = new();
    
    private readonly MainState _mainState;

    public BattleGUIModel(MainState mainState)
    {
      _mainState = mainState;
    }

    public void SetRestartBtnActive(bool isActive)
    {
      ShowRestartBtn.Value = isActive;
    }
    
    public void RestartClicked()
    {
      _mainState.Restart();
    }
  }
}