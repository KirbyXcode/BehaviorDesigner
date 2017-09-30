using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;

public class IsHasFlag : Conditional {

    private Offense offense;

    public override void OnAwake() {
        offense = this.GetComponent<Offense>();
    }

    public override TaskStatus OnUpdate() {
        if (offense.hasFlag) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
