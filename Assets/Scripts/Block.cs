using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
  void Start()
  {

    InvokeRepeating("Fall", 2.0f, 0.5f);
      
  }

    // Update is called once per frame
  void Update()
  {
      
  }

  void Fall() {

    if(transform.localPosition.y < -17) {
      Debug.Log("PLACED");
      CancelInvoke();
      return;
    }

    transform.Translate(0, -1f, 0);
    Debug.Log(transform.localPosition.y);


  }

}
