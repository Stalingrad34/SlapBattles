using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Game.Scripts.Infrastructure.Services
{
  public class DatabaseProvider : IInitializedService
  {
    public ReactiveProperty<string> Language = new();
    public ReactiveProperty<bool> SoundOff;
    public ReactiveProperty<bool> MusicOff;
    private bool _canSave;
    private CancellationTokenSource _saveTokenSource;

    public async UniTask Init(CancellationToken token)
    {
      
    }
  }
}