using InputController.Enums;
using InputController.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InputController.Implementations
{
    public class InputController : MonoBehaviour, IInputController
    {
        [SerializeField] private Button _generateButton;
        [SerializeField] private Button _increaseSizeButton;
        [SerializeField] private Button _decreaseSizeButton;

        public event Action<GameObject> OnClickOnObject;
        private IDictionary<EInputButtonType, List<Action>> _buttonListeners;

        private void Awake()
        {
            _buttonListeners = new Dictionary<EInputButtonType, List<Action>>();

            _generateButton.onClick.AddListener(() => InvokeButtonAction(EInputButtonType.GenerateButton));
            _increaseSizeButton.onClick.AddListener(() => InvokeButtonAction(EInputButtonType.IncreaseMapSizeButton));
            _decreaseSizeButton.onClick.AddListener(() => InvokeButtonAction(EInputButtonType.DecreaseMapSizeButton));
        }

        public void AddButtonListener(EInputButtonType buttonType, Action onAction)
        {
            if (_buttonListeners.ContainsKey(buttonType))
            {
                if (_buttonListeners[buttonType] != null && !_buttonListeners[buttonType].Contains(onAction))
                    _buttonListeners[buttonType].Add(onAction);
            }
            else
            {
                _buttonListeners[buttonType] = new List<Action>() { onAction };
            }
        }

        public void RemoveButtonListener(EInputButtonType buttonType, Action onAction)
        {
            if (_buttonListeners.ContainsKey(buttonType) &&
                _buttonListeners[buttonType] != null &&
                _buttonListeners[buttonType].Contains(onAction))
            {
                _buttonListeners[buttonType].Remove(onAction);
            }
        }

        private void InvokeButtonAction(EInputButtonType buttonType)
        {
            if (_buttonListeners.ContainsKey(buttonType) && _buttonListeners[buttonType] != null)
            {
                foreach (var listener in _buttonListeners[buttonType])
                {
                    listener?.Invoke();
                }
            }
        }

        public void SetButtonInteractable(EInputButtonType buttonType, bool state)
        {
            switch (buttonType)
            {
                case EInputButtonType.GenerateButton: _generateButton.interactable = state; break;
                case EInputButtonType.IncreaseMapSizeButton: _increaseSizeButton.interactable = state; break;
                case EInputButtonType.DecreaseMapSizeButton: _decreaseSizeButton.interactable = state; break;
            }
        }

        public void OnUpdate(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    OnClickOnObject?.Invoke(hit.collider.gameObject);
                }
            }
        }

        public void Dispose()
        {
            OnClickOnObject = null;
            _buttonListeners.Clear();
        }
    }
}
