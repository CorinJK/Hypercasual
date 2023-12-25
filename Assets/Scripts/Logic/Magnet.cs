using System;
using System.Collections.Generic;
using Scripts.Core;
using UnityEngine;

namespace Scripts.Logic
{
    [RequireComponent(typeof(SphereCollider))]
    public class Magnet : MonoBehaviour
    {
        public static Magnet Instance;
        
        private readonly string _objectTag = "Object";
        private readonly string _obstacleTag = "Obstacle";
        [SerializeField] private float _magnetForce;

        private List<Rigidbody> _affectedRigidbodies = new ();
        private Transform magnet;
        
        private void Awake()
        {
            if (Instance == null) 
                Instance = this;
        }

        private void Start()
        {
            magnet = transform;
            _affectedRigidbodies.Clear();
        }

        private void FixedUpdate()
        {
            if (!GameStates.isGameOver && GameStates.isMoving)
            {
                foreach (Rigidbody rb in _affectedRigidbodies)
                {
                    rb.AddForce((magnet.position-rb.position) * _magnetForce * Time.fixedDeltaTime);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            string tag = other.tag;
            
            if (!GameStates.isGameOver && (tag.Equals(_obstacleTag) || tag.Equals(_objectTag)))
            {
                AddToMagnetField (other.attachedRigidbody);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            string tag = other.tag;
            
            if (!GameStates.isGameOver && (tag.Equals(_obstacleTag) || tag.Equals(_objectTag)))
            {
                RemoveFromMagnetField (other.attachedRigidbody);
            }
        }

        public void AddToMagnetField(Rigidbody rb)
        {
            _affectedRigidbodies.Add(rb);
        }
        
        public void RemoveFromMagnetField(Rigidbody rb)
        {
            _affectedRigidbodies.Remove(rb);
        }
    }
}