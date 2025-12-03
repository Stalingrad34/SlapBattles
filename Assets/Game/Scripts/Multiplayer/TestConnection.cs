using Colyseus;
using Cysharp.Threading.Tasks;
using Game.Scripts.Infrastructure;
using UnityEngine;

namespace Game.Scripts.Multiplayer
{
  public class TestConnection : MonoBehaviour
  {
    private MultiplayerManager _multiplayer;
    private ColyseusRoom<State> _room;

    private void Awake()
    {
      _multiplayer = AssetProvider.GetMultiplayerManager();
      _multiplayer.Init();
    }

    public void Connect()
    {
      ConnectAsync().Forget();
    }

    private async UniTaskVoid ConnectAsync()
    {
      _room = await _multiplayer.ConnectTest();
    }

    public void Disconnect()
    {
      _room.Leave();
    }
  }
}