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
            Debug.Log($"[InputReader] {gameObject.name} had set controlled entity to {entityId}", this);
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

        private void Awake()
        {
            if (_actions == null)
            {
                Debug.LogError(
                    $"[InputReader] Missing InputActionAsset on '{gameObject.name}'. Assign an asset in the Inspector.",
                    this);
                enabled = false;
                return;
            }
            if (_commandBuffer == null)
            {
                Debug.LogError(
                    $"[InputReader] CommandBuffer dependency not injected on '{gameObject.name}'. Ensure it is registered in the DI container.",
                    this);
                enabled = false;
                return;
            }

            _inputActionGateway = EnsureGateway();
            _inputActionGateway.SetContext(InputContext.Player);
            Debug.Log($"[InputReader] {gameObject.name} had set up done and WAITING to pass entityId", this);
        }
        private void OnEnable()
        {
            if (_inputActionGateway != null)
            {
                _inputActionGateway.EnableCurrentContext();
            }
        }
        private void OnDisable()
        {
            if (_inputActionGateway != null)
            {
                _inputActionGateway.DisableCurrentContext();
            }
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
            if (!_controlledEntityId.IsValid)
            {
                return;
            }
            var moveInput = _inputActionGateway.ReadMove();
            var dirX = moveInput.x;
            var dirY = moveInput.y;
            _commandBuffer.Enqueue(new MoveCommand(_controlledEntityId, dirX, dirY));
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
            if (gateway.CurrentContext != targetContext)
            {
                gateway.DisableCurrentContext();
                gateway.SetContext(targetContext);
                gateway.EnableCurrentContext();
                _commandBuffer.Clear();
            }

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
            if (_inputActionGateway != null)
            {
                _inputActionGateway.Dispose();
                _inputActionGateway = null;
            }
        }
    }
}
