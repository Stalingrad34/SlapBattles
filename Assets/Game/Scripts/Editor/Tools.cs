using UnityEditor;
using UnityEditor.SceneManagement;

namespace Game.Scripts.Editor
{
  public class Tools
  {
    [MenuItem("Game/Play", false, 1)]
    static void Play()
    {
      EditorSceneManager.OpenScene("Assets/Game/Scenes/Bootstrap.unity");
      EditorApplication.isPlaying = true;
    }

    /*[MenuItem("Game/Clear and Play", false, 2)]
    static void ClearAndPlay()
    {
      DatabaseProvider.Clear();
      EditorSceneManager.OpenScene("Assets/AutoBattler/Scenes/Bootstrap.unity");
      EditorApplication.isPlaying = true;
    }
    
    [MenuItem("Game/OpenLocalization", false, 3)]
    static void OpenLocalization()
    {
      LocalizationEditor.OpenWindow();
    }*/
  }
}