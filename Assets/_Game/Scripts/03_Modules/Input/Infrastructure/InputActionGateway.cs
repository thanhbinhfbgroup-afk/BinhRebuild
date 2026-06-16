using Binh.Modudes.Input.Context;
using Binh.SharedPorts.Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Binh.Modules.Input.Infrastructure
{
    public sealed class InputActionGateway : IDisposable
    {
        private readonly InputActionAsset _runtimeActions;

        private readonly InputActionMap _playerActionMap;
        private readonly InputAction _moveAction;
        private readonly InputAction _attackAction;
        private readonly InputAction _interactAction;

        private readonly InputActionMap _uiActionMap;
        private readonly InputAction _submitAction;

        public InputContext? CurrentContext { get; private set; }

        public InputActionGateway(InputActionAsset actions)
        {
            if (actions == null)
            {
                throw new InvalidOperationException("InputActionGateway requires an InputActionAsset.");
            }

            _runtimeActions = UnityEngine.Object.Instantiate(actions);

            _playerActionMap = _runtimeActions.FindActionMap(InputContextNames.Maps.Player, throwIfNotFound: true);
            _moveAction = _playerActionMap.FindAction(InputContextNames.PlayerActions.Move, throwIfNotFound: true);
            _attackAction = _playerActionMap.FindAction(InputContextNames.PlayerActions.Attack, throwIfNotFound: true);
            _interactAction = _playerActionMap.FindAction(InputContextNames.PlayerActions.Interact, throwIfNotFound: true);

            _uiActionMap = _runtimeActions.FindActionMap(InputContextNames.Maps.UI, throwIfNotFound: true);
            _submitAction = _uiActionMap.FindAction(InputContextNames.UIActions.Submit, throwIfNotFound: true);
        }

        public void SetContext(InputContext context)
        {
            switch (context)
            {
                case InputContext.Player:
                    CurrentContext = InputContext.Player;
                    return;
                case InputContext.UI:
                    CurrentContext = InputContext.UI;
                    return;
                case InputContext.Vehicle:
                default:
                    throw new InvalidOperationException(
                        $"InputActionGateway does not support context '{context}'.");
            }
        }

        public void EnableCurrentContext()
        {
            GetCurrentActionMap().Enable();
        }

        public void DisableCurrentContext()
        {
            GetCurrentActionMap().Disable();
        }

        private InputActionMap GetCurrentActionMap()
        {
            if (CurrentContext == null)
            {
                throw new InvalidOperationException("InputActionGateway requires an active input context.");
            }

            return CurrentContext switch
            {
                InputContext.Player => _playerActionMap,
                InputContext.UI => _uiActionMap,
                InputContext.Vehicle => throw new InvalidOperationException(
                    $"InputActionGateway does not support context '{CurrentContext}'."),
                _ => throw new InvalidOperationException(
                    $"InputActionGateway does not support context '{CurrentContext}'.")
            };
        }

        public Vector2 ReadMove()
        {
            EnsurePlayerContext();
            return _moveAction.ReadValue<Vector2>();
        }

        public bool WasInteractPressedThisFrame()
        {
            EnsurePlayerContext();
            return _interactAction.WasPressedThisFrame();
        }

        public bool WasAttackPressedThisFrame()
        {
            EnsurePlayerContext();
            return _attackAction.WasPressedThisFrame();
        }

        private void EnsurePlayerContext()
        {
            if (CurrentContext != InputContext.Player)
            {
                throw new InvalidOperationException(
                    $"InputActionGateway does not player context, current context is '{CurrentContext}'.");
            }
        }

        public bool WasSubmitPressedThisFrame()
        {
            EnsureUiContext();
            return _submitAction.WasPressedThisFrame();
        }

        private void EnsureUiContext()
        {
            if (CurrentContext != InputContext.UI)
            {
                throw new InvalidOperationException(
                    $"InputActionGateway does not ui context,current context is '{CurrentContext}'.");
            }
        }

        public void Dispose()
        {
            if (_runtimeActions != null)
            {
                UnityEngine.Object.Destroy(_runtimeActions);
            }
        }
    }
}