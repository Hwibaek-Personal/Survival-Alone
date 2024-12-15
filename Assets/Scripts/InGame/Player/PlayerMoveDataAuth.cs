using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace InGame.Player
{
    public partial struct PlayerMoveData : IComponentData
    {
        public float Speed;
        public float3 MoveDir;
    }
    
    public partial class PlayerMoveDataAuth : MonoBehaviour
    {
        public float speed;

        private class PlayerMoveDataAuthBaker : Baker<PlayerMoveDataAuth>
        {
            public override void Bake(PlayerMoveDataAuth authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerMoveData
                {
                    Speed = authoring.speed
                });
            }
        }
    }
}