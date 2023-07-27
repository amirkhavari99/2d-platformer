using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (LevelController.Instance != null)
        {
            Vector2 playerposition = LevelController.Instance.GetPlayerPosition();
            transform.position = new Vector3(playerposition.x, playerposition.y, transform.position.z);
        }
    }
}
