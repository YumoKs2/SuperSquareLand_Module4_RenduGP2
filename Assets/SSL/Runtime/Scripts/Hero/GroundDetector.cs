using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private Transform[] _detectionPoints;
    [SerializeField] private float _detectionLength = 0.05f;
    [SerializeField] private LayerMask _groundLayerMask;

    public bool DetectorGroundNearBy()
    {
        foreach (Transform detectionPoint in _detectionPoints) {
            RaycastHit2D hitResult = Physics2D.Raycast(
                detectionPoint.position,
                Vector2.down,
                _detectionLength,
                _groundLayerMask
                );

            if (hitResult.collider != null ) { 
            return true;
            }
        }

        return false;
    }
}