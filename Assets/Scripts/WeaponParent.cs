using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    private Transform aimTransform;
    [SerializeField] private Transform playerTransform;
    private void Awake()
    {
        aimTransform = GetComponent<Transform>();
    }
    private void Update()
    {
        Vector3 mousePos = GetMouseWorldPos();
        Vector3 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        if (playerTransform.localScale.x < 0)
        {   
            angle += 180;
        }
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        Debug.Log(transform.localScale);
    }
    public Vector3 GetMouseWorldPos()
    {
        Vector3 vec = GetMouseWorldPosWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public Vector3 GetMouseWorldPosWithZ(Vector3 screenPos, Camera worldCamera)
    {
        Vector3 worldPos = worldCamera.ScreenToWorldPoint(screenPos);
        return worldPos;
    }
}
