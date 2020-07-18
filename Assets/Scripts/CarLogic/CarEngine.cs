using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

namespace CarGame.CarLogic
{
    public class CarEngine : MonoBehaviour
    {
        public Transform path;
        public float maxSteerAngle = 45f;
        public WheelCollider wheelFL;
        public WheelCollider wheelFR;

        private List<Transform> nodes;
        private int currentNode = 0;

        void Start()
        {
            Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
            nodes = new List<Transform>();

            EngineGetNodes(pathTransforms, nodes);
        }

        // Get all nodes from current path
        private void EngineGetNodes(Transform[] pathTransforms, List<Transform> nodes)
        {            
            for (int i = 0; i < pathTransforms.Length; i++)
            {
                if (pathTransforms[i] != path.transform)
                {
                    nodes.Add(pathTransforms[i]);
                }
            }
        }

        private void FixedUpdate()
        {
            ApplySteer();
        }

        private void ApplySteer()
        {
            // Position or Direction of the current node car is driving towards
            Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);

            // TO determine if we are steering LEFT or RIGHT (relativeVector.x / relativeVector.magnitude)
            // - negative (we are steering left)
            // + positive (we are steeting right)

            float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;

            // Set the steer direction onto the Wheel Colliders
            wheelFL.steerAngle = newSteer;
            wheelFR.steerAngle = newSteer;

            print(relativeVector);
        }
    }
}
