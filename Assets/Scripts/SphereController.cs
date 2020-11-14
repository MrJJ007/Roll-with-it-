using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
   void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
