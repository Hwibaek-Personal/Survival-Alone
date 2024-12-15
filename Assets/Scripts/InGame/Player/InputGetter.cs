using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InGame.Player
{
    public partial class InputGetter : MonoBehaviour
    {
        public static InputGetter Instance;
        
        [SerializeField] private PlayerInput playerInput;

        [field: SerializeField] public Vector2 MoveDirection { get; private set; } = Vector2.zero;
        
        private void OnEnable()
        {
            Instance = this;
            Debug.Log("OnEnable");
            playerInput.actions["Move"].performed += OnMove;
            playerInput.actions["Move"].canceled += OnMove;
        }

        private void OnDisable()
        {
            if (Instance == this)
            {
                Instance = null;
            }
            playerInput.actions["Move"].performed -= OnMove;
            playerInput.actions["Move"].canceled -= OnMove;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            MoveDirection = context.ReadValue<Vector2>();
            
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var query = new EntityQueryBuilder(Allocator.Temp).WithAll<PlayerMoveData>().Build(entityManager);

            var array = query.ToComponentDataArray<PlayerMoveData>(Allocator.Temp);

            for (var i = 0; i < array.Length; i++)
            {
                var data = array[i];
                data.MoveDir.xz = MoveDirection;
                array[i] = data;
            }
            query.CopyFromComponentDataArray(array);
        }
    }
}