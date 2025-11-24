using Game.Scripts.Infrastructure.Custom;
using Game.Scripts.Infrastructure.Extensions;
using UniRx;
using UnityEngine;

namespace Game.Scripts.UI
{
  public class BattleGUIView : GUIView<BattleGUIModel>
  {
    [SerializeField] private CustomButton restartBtn;
    [SerializeField] private VariableJoystick joystick;
    
    protected override void SetModel(BattleGUIModel model)
    {
      model.ShowRestartBtn.Subscribe(restartBtn.gameObject.SetActive).AddTo(gameObject);
      restartBtn.OnClick(model.RestartClicked).AddTo(gameObject);
      
      
    }
  }
}