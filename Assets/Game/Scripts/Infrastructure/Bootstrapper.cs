using Cysharp.Threading.Tasks;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Infrastructure.States;
using UnityEngine;

namespace Game.Scripts.Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = 60;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            
            var multiplayerManager = AssetProvider.GetMultiplayerManager();
            multiplayerManager.Init();
            DontDestroyOnLoad(multiplayerManager);
            
            ServiceProvider.Register(multiplayerManager);
            StateMachine.Init();
            StateMachine.EnterAsync<LobbyState>().Forget();
        }
    }
}