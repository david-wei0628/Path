using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayType.Move
{
    public class RayT
    {
        Ray ray;
        RaycastHit hit;
        public Vector3 SelectRay(Camera CamearTrans)
        {
            ray = CamearTrans.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, 3500);
            return hit.point;
        }

    }
}

