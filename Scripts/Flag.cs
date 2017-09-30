using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {

    public Offense owner;
    
    public void OnTriggerEnter(Collider other) {

        if (other.tag == "Offense") {
            if(owner==null)
                CTFGameManager.Instance.TakeFlag();
            if (owner != null) {
                owner.hasFlag = false;
            }

            other.GetComponent<Offense>().hasFlag = true;
            transform.parent = other.transform;
            owner = other.GetComponent<Offense>();
        }

    }

}
