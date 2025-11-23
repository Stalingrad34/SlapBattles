using System.Collections.Generic;
using Game.Scripts.Infrastructure.Services;

namespace Game.Scripts.Infrastructure.Extensions
{
  public static class CommonExtensions
  {
    public static string Localize(this string text)
    {
      var language = ServiceProvider.Get<DatabaseProvider>().Language.Value;
      var localization = LocalizationProvider.GetLocalization(language);
      
      return localization.GetValueOrDefault(text, text);
    }
  }
}