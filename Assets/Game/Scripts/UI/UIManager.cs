using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Scripts.Infrastructure;
using Game.Scripts.Widgets;
using Sirenix.Utilities;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        public static Camera GameCamera => _instance.gameCamera; 
        private static UIManager _instance;
        
        [SerializeField] private Canvas guiCanvas;
        [SerializeField] private CanvasHolder canvasHolder;
        [SerializeField] private Transform popupRoot;
        [SerializeField] private Camera uiCamera;
        [SerializeField] private Camera gameCamera;
        [SerializeField] private CanvasGroup blackScreenFade;
        [SerializeField] private NotificationView infoMessageView;
        [SerializeField] private NotificationView warningMessageView;

        private readonly Dictionary<Type, PopupViewBase> _popups = new ();
        private readonly Dictionary<GUIModel, GUIViewBase> _gui = new ();
        private Sequence _sequence;

        public float duration = 1;
        public float strength = 3;
        public int vibrato = 10;
        public float randomness = 90;

        [ContextMenu("TestShakeCamera")]
        public static void ShakeCamera(float delay)
        {
            GameCamera
                .DOShakePosition(_instance.duration, _instance.strength, _instance.vibrato, _instance.randomness)
                .SetDelay(delay)
                .SetLink(_instance.gameObject);
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }

        public static void SetCameraPosition(Vector3 position)
        {
            _instance.gameCamera.transform.position = position;
        }
        
        public static void SetCameraPositionTween(Vector3 position, Action onComplete = null)
        {
            _instance.gameCamera.transform
                .DOMove(position, 1)
                .OnComplete(() => onComplete?.Invoke())
                .SetEase(Ease.OutSine)
                .SetLink(_instance.gameObject);
        }
        
        public static void SetCameraRotation(Vector3 rotation)
        {
            _instance.gameCamera.transform.rotation = Quaternion.Euler(rotation);
        }

        public static TView ShowGUI<TView, TModel>(TModel model) where TView : GUIView<TModel> where TModel : GUIModel
        {
            var resource = AssetProvider.GetGUI<TView, TModel>();
            var view = Instantiate(resource, _instance.guiCanvas.transform);
            view.Init(model);
            _instance._gui[model] = view;

            return view;
        }

        public static void ShowPopup<TView, TModel>(TModel model)
            where TView : PopupView<TModel> where TModel : PopupModel
        {
            var res = AssetProvider.GetPopup<TView, TModel>();
            var canvas = GetPopupCanvas();
            var popupView = Instantiate(res, canvas.transform, false);

            popupView.Init(model, canvas);
            _instance._popups[typeof(TModel)] = popupView;

            var showTween = popupView.GetShowTween();
            showTween.Play();
        }
        
        public static void HidePopup<T>(T model) where T: PopupModel
        {
            var view = _instance._popups[model.GetType()];
            view.SetInputActive(false);

            var hideTween = view.GetHideTween();
            hideTween.OnComplete(() => DestroyPopup(model));
            hideTween.Play();
        }
        
        public static async UniTask ShowPopupAsync<TView, TModel>(TModel model)
            where TView : PopupView<TModel> where TModel : PopupModel
        {
            var res = AssetProvider.GetPopup<TView, TModel>();
            var canvas = GetPopupCanvas();
            var popupView = Instantiate(res, canvas.transform, false);

            popupView.Init(model, canvas);
            _instance._popups[typeof(TModel)] = popupView;
            await popupView.GetShowTween().AsyncWaitForCompletion();
        }
        
        public static async UniTask ShowBlackScreenFade()
        {
            _instance._sequence?.Complete();
            _instance._sequence = DOTween.Sequence();
            
            await _instance._sequence
                .OnStart(() => _instance.blackScreenFade.gameObject.SetActive(true))
                .Append(_instance.blackScreenFade.DOFade(1, 0.5f))
                .SetEase(Ease.Linear)
                .SetLink(_instance.gameObject)
                .AsyncWaitForCompletion();
        }
        
        public static async UniTask HideBlackScreenFade()
        {
            _instance._sequence?.Complete();
            _instance._sequence = DOTween.Sequence();
            
            await _instance._sequence
                .Append(_instance.blackScreenFade.DOFade(0, 0.5f))
                .OnComplete(() => _instance.blackScreenFade.gameObject.SetActive(false))
                .SetEase(Ease.Linear)
                .SetLink(_instance.gameObject)
                .AsyncWaitForCompletion();
        }

        public static void ShowInfoMessage(string message)
        {
            _instance.warningMessageView.Hide();
            _instance.infoMessageView.Show(message);
        }
        
        public static void ShowWarnMessage(string message)
        {
            _instance.infoMessageView.Hide();
            _instance.warningMessageView.Show(message);
        }
        
        public static void Clear()
        {
            _instance._popups.Values.ForEach(v => v.Destroy());
            _instance._gui.Values.ForEach(v => Destroy(v.gameObject));
            
            _instance._popups.Clear();
            _instance._gui.Clear();
        }
        
        private static CanvasHolder GetPopupCanvas()
        {
            var canvas = Instantiate(_instance.canvasHolder, _instance.popupRoot);
            canvas.name = $"Canvas {_instance._popups.Count}";
            canvas.SetCamera(_instance.uiCamera);

            return canvas;
        }

        private static void DestroyPopup<T>(T model) where T: PopupModel
        {
            var view = _instance._popups[model.GetType()];
            view.Destroy();
            _instance._popups.Remove(model.GetType());
        }
    }
}