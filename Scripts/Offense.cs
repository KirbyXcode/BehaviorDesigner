using UnityEngine;
using System.Collections;

public class Offense : MonoBehaviour {

    public bool hasFlag = false;

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start() {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }
     
    public void OnCollisionEnter(Collision other) {
        Debug.Log("OnCollisionEnter");
        if (other.collider.tag == "Defense") {
            if (hasFlag) {
                hasFlag = false;
                CTFGameManager.Instance.DropFlag();
                if (transform.childCount > 0) {
                    Transform flagTrans = transform.GetChild(0);//得到0号儿子
                    flagTrans.GetComponent<Flag>().owner = null;
                    flagTrans.parent = null;
                }
            }
            transform.position = startPosition;
            transform.rotation = startRotation;
            
        }
    }

}
