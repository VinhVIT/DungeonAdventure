using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{   
    public static CameraShake instance;
    private void Awake() {
        instance = this;
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            float x = Random.Range(orignalPosition.x - 0.01f,orignalPosition.x + 0.01f) * magnitude;
            float y = Random.Range(orignalPosition.y - 0.01f,orignalPosition.y + 0.01f) * magnitude;

            transform.position = new Vector3(x, y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = new Vector3(0,0,-10);
    }

   
}
