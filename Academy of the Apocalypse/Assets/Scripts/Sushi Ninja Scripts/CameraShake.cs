using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour{

       public float durationTime = 0.15f;
       public float magnitude = 0.3f;
       public bool enable = true;

       //The Update Function is just for testing (hit [p]), and can be commented out:
       void Update(){

       }

       //use this to call from another script, like when the player gets hit
       public void ShakeCamera(float durationTime2, float magnitude2){
              if (enable == true) {
                     StartCoroutine(ShakeMe(durationTime2, magnitude2));
              }
       }

       public void disableShake() {
              enable = false;
       }

       public void enableShake() {
              enable = true;
       }

       //the screenshake!
       public IEnumerator ShakeMe(float durationTime, float magnitude){
              Vector3 origPos = transform.localPosition;
              float elapsedTime = 0.0f;

              while (elapsedTime < durationTime){
                     float sX = Random.Range(-1f, 1f) * magnitude;
                     float sY = Random.Range(-1f, 1f) * magnitude;

                     transform.localPosition = new Vector3((origPos.x+sX), (origPos.y+sY), origPos.z);
                     elapsedTime += Time.deltaTime;
                     yield return null;
              }
              transform.localPosition = origPos;
       }

}
