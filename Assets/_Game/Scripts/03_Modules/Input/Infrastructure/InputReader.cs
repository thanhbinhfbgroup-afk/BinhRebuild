using System;
using Binh.Core.ValueObjects;
using Binh.Modules.Input.Commands;
using Binh.SharedPorts.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Binh.Modules.Input.Infrastructure
{
    public sealed class InputReader : MonoBehaviour, IInputContextService
    {
        [SerializeField] private InputActionAsset _actions;
        private CommandBuffer _commandBuffer;
        private BinhEntityId _controlledEntityId;
        private InputActionGateway _inputActionGateway;

        public void SetControlledEntity(BinhEntityId entityId)
        {
            if (!entityId.IsValid)
            {
                throw new InvalidOperationException("InputReader requires an active entityid");
            }
            _controlledEntityId = entityId;
        }
        private void Awake()
        {
            _inputActionGateway = EnsureGateway();
            _inputActionGateway.SetContext(InputContext.Player);
        }
        private InputActionGateway EnsureGateway()
        {
            if (_actions == null)
            {
                throw new InvalidOperationException("InputReader requires an active InputActionAsset");
            }
            _inputActionGateway ??= new InputActionGateway(_actions);
            return _inputActionGateway;
        }
        private void OnEnable()
        {
            _inputActionGateway.EnableCurrentContext();
        }
        private void OnDisable()
        {
            if (_inputActionGateway == null)
            {
                return;
            }
            _inputActionGateway.DisableCurrentContext();
        }
        public void ValidateConfiguration()
        {
            _ = EnsureGateway();
        }
        [Inject]
        public void Inject(CommandBuffer commandBuffer)
        {
            _commandBuffer = commandBuffer ?? throw new ArgumentNullException(nameof(commandBuffer));
        }
        private void Update()
        {
            switch (_inputActionGateway.CurrentContext)
            {
                case InputContext.Player: ReadPlayerMap(); return;
                case InputContext.UI: return;
                case InputContext.Vehicle: throw new InvalidOperationException("InputReader does not support Vehicle context");
                case null: throw new InvalidOperationException("InputReader requires input context ");
                default: throw new InvalidOperationException("InputReader does support");
            }
        }
        private void ReadPlayerMap()
        {
            if (_commandBuffer == null)
            {
                throw new InvalidOperationException("InputReader requires a command buffer");
            }
            if (!_controlledEntityId.IsValid)
            {
                throw new InvalidOperationException("InputReader requires an active entity id");
            }
            var moveInput = _inputActionGateway.ReadMove();
            var DirX = moveInput.x;
            var DirY = moveInput.y;
            _commandBuffer.Enqueue(new MoveCommand(_controlledEntityId, DirX, DirY));
            if (_inputActionGateway.WasAttackPressedThisFrame())
            {
                _commandBuffer.Enqueue(new AttackCommand(_controlledEntityId, _inputActionGateway.IsAttackHeld(), 0f));
            }
            if (_inputActionGateway.WasInteractPressedThisFrame())
            {
                _commandBuffer.Enqueue(new InteractCommand(_controlledEntityId));
            }
        }
        public void SwitchContext(InputContext targetContext)
        {
            if (_commandBuffer == null)
            {
                throw new InvalidOperationException("InputReader requires a command buffer");
            }
            var gateway = EnsureGateway();
            if (gateway.CurrentContext == targetContext)
            {
                return;
            }
            gateway.DisableCurrentContext();
            gateway.SetContext(targetContext);
            gateway.EnableCurrentContext();
            _commandBuffer.Clear();
        }
        public bool WasSubmitPressedThisFrame()
        {
            var gateway = EnsureGateway();
            if (gateway.CurrentContext != InputContext.UI)
            {
                return false;
            }
            return gateway.WasSubmitPressedThisFrame();
        }
        private void OnDestroy()
        {
            if (_inputActionGateway == null)
            {
                return;
            }

            _inputActionGateway.Dispose();
            _inputActionGateway = null;
        }
    }
}