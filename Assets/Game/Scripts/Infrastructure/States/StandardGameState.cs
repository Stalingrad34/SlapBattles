using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Infrastructure.States
{
  public class StandardGameState : AbstractGameState
  {
    protected override async UniTask LoadScene()
    {
      await SceneManager.LoadSceneAsync("Game/Scenes/StandardGame");
    }
  }
}