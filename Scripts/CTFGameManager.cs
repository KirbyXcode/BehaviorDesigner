using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;

public class CTFGameManager : MonoBehaviour {

    private static CTFGameManager _instance;

    public static CTFGameManager Instance {
        get {
            return _instance;
        }
    }

    private List<BehaviorTree> flagNotTakenBehaviorTrees=new List<BehaviorTree>();
    private List<BehaviorTree> flagTakenBehaviorTrees=new List<BehaviorTree>();

    void Awake() {
        _instance = this;
    }

    // Use this for initialization
	void Start () {

	    BehaviorTree[] bts = FindObjectsOfType<BehaviorTree>();//得到场景中所有的行为树
	    foreach (var bt in bts) {
	        if (bt.Group == 1) {
	            flagNotTakenBehaviorTrees.Add(bt);
	        }
	        else {
	            flagTakenBehaviorTrees.Add(bt);
	        }
	    }

	}


    public void TakeFlag() {
        Debug.Log("takeFlag");
        foreach (var bt in flagNotTakenBehaviorTrees) {
            if (BehaviorManager.instance.IsBehaviorEnabled(bt)) {//判断行为树是否启用
                bt.DisableBehavior();//禁用自身
            }
        }

        foreach (var bt in flagTakenBehaviorTrees) {
            if (BehaviorManager.instance.IsBehaviorEnabled(bt) == false) {
                bt.EnableBehavior();//启用自身
            }
        }
    }

    public void DropFlag() {
        foreach (var bt in flagNotTakenBehaviorTrees)
        {
            if (BehaviorManager.instance.IsBehaviorEnabled(bt)==false)
            {//判断行为树是否启用
                bt.EnableBehavior();
            }
        }

        foreach (var bt in flagTakenBehaviorTrees)
        {
            if (BehaviorManager.instance.IsBehaviorEnabled(bt))
            {
                bt.DisableBehavior();
            }
        }
    }

}
