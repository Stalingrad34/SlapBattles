using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Services
{
  public class LocalizationProvider : IInitializedService
  {
    private static Dictionary<string, Dictionary<string, string>> _localizations;

    public async UniTask Init(CancellationToken token)
    {
      var json = await Resources.LoadAsync<TextAsset>("Localization") as TextAsset;
      _localizations = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json.text);
    }

    public static Dictionary<string, string> GetLocalization(string locale)
    {
      var language = !string.IsNullOrEmpty(locale) ? locale : ServiceProvider.Get<DatabaseProvider>().Language.Value;
      
      return _localizations.TryGetValue(language, out var localization) 
        ? localization 
        : _localizations.First().Value;
    }
  }
}