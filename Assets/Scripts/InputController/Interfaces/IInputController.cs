using General.Interfaces;
using InputController.Enums;
using System;
using UnityEngine;

namespace InputController.Interfaces
{
    public interface IInputController : IUpdatable, IDisposable
    {
        event Action<GameObject> OnClickOnObject;

        void AddButtonListener(EInputButtonType buttonType, Action onAction);
        void RemoveButtonListener(EInputButtonType buttonType, Action onAction);

        void SetButtonInteractable(EInputButtonType buttonType, bool state);
    }
}
