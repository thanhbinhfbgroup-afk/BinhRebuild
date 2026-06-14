using Binh.Modudes.Input.Context;
using Binh.SharedPorts.Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BillGameCore.Modules.Input.Infrastructure
{
    public sealed class InputActionGateway : IDisposable
    {
        private readonly InputActionAsset _runtimeActions;
        private readonly InputActionMap _playerActionMap;
        private readonly InputAction _moveAction;
        private readonly InputAction _interactAction;
        private readonly InputAction _attackAction;
        public InputContext CurrentContext { get; private set; }

        public InputActionGateway(InputActionAsset actions)
        {
            if (actions == null)
            {
                throw new InvalidOperationException("InputActionGateway requires an InputActionAsset.");
            }

            _runtimeActions = UnityEngine.Object.Instantiate(actions);
            _playerActionMap = _runtimeActions.FindActionMap(InputContextNames.Player, throwIfNotFound: true);
            _moveAction = _playerActionMap.FindAction("Move", throwIfNotFound: true);
            _interactAction = _playerActionMap.FindAction("Interact", throwIfNotFound: true);
            _attackAction = _playerActionMap.FindAction("Attack", throwIfNotFound: true);
        }

        public void SwitchContext(InputContext context)
        {

            // CHẶN: Nếu trùng khớp với ngữ cảnh hiện tại -> Thoát luôn cho nhẹ máy
            if (context == CurrentContext)
            {
                return;
            }

            _playerActionMap.Disable();


            switch (context)
            {

                case InputContext.Player:
                    _playerActionMap.Enable();
                    CurrentContext = InputContext.Player;
                    return;


                case InputContext.UI:
                case InputContext.Vehicle:
                    throw new InvalidOperationException($"Input context '{context}' is not supported yet.");


                default:
                    throw new InvalidOperationException($"Unknown input context '{context}'.");
            }
        }

        public void EnablePlayerMap()
        {
            SwitchContext(InputContext.Player);
        }

        public void DisablePlayerMap()
        {
            _playerActionMap.Disable();
        }

        public Vector2 ReadMoveInput()
        {
            if (CurrentContext != InputContext.Player)
            {
                throw new InvalidOperationException(
                    $"Cannot read move input when current context is '{CurrentContext}'.");
            }

            return _moveAction.ReadValue<Vector2>();
        }
        public bool WasInteractPerformedThisFrame()
        {
            if (CurrentContext != InputContext.Player)
            {
                throw new InvalidOperationException(
                    $"Cannot read interact input when current context is '{CurrentContext}'.");
            }

            return _interactAction.WasPerformedThisFrame();
        }
        public bool WasAttackPerformedThisFrame()
        {
            if (CurrentContext != InputContext.Player)
            {
                throw new InvalidOperationException(
                    $"Cannot read attack input when current context is '{CurrentContext}'.");
            }

            return _attackAction.WasPerformedThisFrame();
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
