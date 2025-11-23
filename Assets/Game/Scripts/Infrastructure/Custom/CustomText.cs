using System.Collections.Generic;
using Game.Scripts.Infrastructure.Services;
using TMPro;
using UniRx;

namespace Game.Scripts.Infrastructure.Custom
{
    public class CustomText : TextMeshProUGUI
    {
        private TextData _textData;
        private string[] _params;
        private bool _isSubscribed;

        public void SetText(TextData textData)
        {
            _textData = textData;
            
            if (!_isSubscribed)
            {
                _isSubscribed = true;
                ServiceProvider.Get<DatabaseProvider>().Language.Subscribe(OnLanguageChanged).AddTo(gameObject);
            }
            else
            {
                LocalizeText(ServiceProvider.Get<DatabaseProvider>().Language.Value);
            }
        }

        private void OnLanguageChanged(string locale)
        {
            LocalizeText(locale);
        }

        private void LocalizeText(string locale)
        {
            var localization = LocalizationProvider.GetLocalization(locale);
            if (!string.IsNullOrEmpty(_textData.Key))
            {
                text = localization.GetValueOrDefault(_textData.Key, _textData.Key);
            }
            else if (!string.IsNullOrEmpty(_textData.Format))
            {
                var format = localization.GetValueOrDefault(_textData.Format, _textData.Format);

                var values = new List<string>(_textData.Values.Length);
                foreach (var value in _textData.Values)
                {
                    var localizeValues = localization.GetValueOrDefault(value, value);
                    values.Add(localizeValues);
                }

                text = string.Format(format, values.ToArray());
            }
            else
                text = string.Empty;
        }
    }
}