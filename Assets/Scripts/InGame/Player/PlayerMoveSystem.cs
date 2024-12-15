using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace InGame.Player
{
    public partial struct PlayerMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new PlayerMoveSystemJob
            {
                DeltaTime = SystemAPI.Time.DeltaTime,
            };
            job.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct PlayerMoveSystemJob : IJobEntity
    {
        public float DeltaTime;
        public void Execute(ref LocalTransform localTrans, in PlayerMoveData moveData, ref PhysicsVelocity velocity)
        {
            velocity.Linear = moveData.MoveDir * moveData.Speed;
            velocity.Angular = float3.zero;
        }
    }
}