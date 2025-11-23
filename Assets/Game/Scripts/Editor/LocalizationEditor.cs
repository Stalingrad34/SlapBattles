using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Editor
{
  public class LocalizationEditor : OdinEditorWindow
  {
    [OnInspectorInit]
    public void Init()
    {
      if (LocaleList.Count > 0)
        return;
      
      var json = Resources.Load<TextAsset>("Localization");
      var localization = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json.text);
      foreach (var (key, value) in localization["ru"])
      {
        var locale = new KeyValueLocale() {Key = key, Russian = value, English = localization["en"][key]};
        LocaleList.Add(locale);
      }
    }
    
    [Button]
    public void Save()
    {
      var localization = new Dictionary<string, Dictionary<string, string>>();
      localization.Add("ru", GetRussianLocalization());
      localization.Add("en", GetEnglishLocalization());
      
      var json = JsonConvert.SerializeObject(localization);
      var path = "Assets/AutoBattler/Resources/Localization.json";
      File.WriteAllText(path, json);
      AssetDatabase.ImportAsset(path);
      AssetDatabase.SaveAssets();
      AssetDatabase.Refresh();
    }

    private Dictionary<string, string> GetRussianLocalization()
    {
      return LocaleList.ToDictionary(locale => locale.Key, locale => locale.Russian);
    }
    
    private Dictionary<string, string> GetEnglishLocalization()
    {
      return LocaleList.ToDictionary(locale => locale.Key, locale => locale.English);
    }
    
    [TableList]
    public List<KeyValueLocale> LocaleList = new ();

    [Serializable]
    public class KeyValueLocale
    {
      [TableColumnWidth(20)]
      public string Key;
      public string Russian;
      public string English;
    }

    public static void OpenWindow()
    {
      var window = GetWindow<LocalizationEditor>();
      window.titleContent = new GUIContent("Localization Editor");
      window.minSize = new Vector2(800, 600);
      window.Show();
    }

  }
}