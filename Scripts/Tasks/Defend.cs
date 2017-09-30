using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

//用来追敌人，直到敌人跑出视野范围外边
public class Defend : Action {

    public SharedFloat viewDistance;
    public SharedFloat fieldOfViewAngle;

    public SharedFloat speed;
    public SharedFloat angularSpeed;

    public SharedTransform target;//要防御的目标 

    private float sqrViewDistance;
    private NavMeshAgent navMeshAgent;
    
    public override void OnAwake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public override void OnStart() {
        sqrViewDistance = viewDistance.Value*viewDistance.Value;
        //启用导航组件
        navMeshAgent.enabled = true;
        navMeshAgent.speed = speed.Value;
        navMeshAgent.angularSpeed = angularSpeed.Value;
        navMeshAgent.destination = target.Value.position;
    }

    public override void OnEnd() {
        navMeshAgent.enabled = false;
    }

    //如果抢夺者在视野内，就追， 否则就认为防御成功
    public override TaskStatus OnUpdate() {
        //做一个安全的校验
        if (target == null && target.Value == null) {
            return TaskStatus.Failure;
        }
        float sqrDistance = (target.Value.position - transform.position).sqrMagnitude;
        float angle = Vector3.Angle(transform.forward, target.Value.position - transform.position);
        if (sqrDistance < sqrViewDistance && angle < fieldOfViewAngle.Value*0.5f) {
            if (navMeshAgent.destination != target.Value.position) {
                navMeshAgent.destination = target.Value.position;
            }
            return TaskStatus.Running;
        }
        else {
            return TaskStatus.Success;
        }
    }
}
