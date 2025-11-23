using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.Data.Units
{
  public abstract class UnitView : MonoBehaviour
  {
    [SerializeField] private ConvertToEntity entityConverter;
    
    public void Setup(UnitData data)
    {
      var setupComponents = gameObject.GetComponents<IUnitSetup>();
      foreach (var setupComponent in setupComponents)
      {
        setupComponent.Setup(data);
      }
    }
  }
}