using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Scripts.Infrastructure.Services
{
  public interface IInitializedService : IService
  {
    UniTask Init(CancellationToken token);
  }
}