using System.Collections.Generic;
using Scripts.Core;
using UnityEngine;

namespace Scripts.Hole
{
    public class HoleMovement : MonoBehaviour
    {
        [Header("Hole mesh")]
        [SerializeField] private MeshFilter _holeMeshFilter;
        [SerializeField] private MeshCollider _holeMeshCollider;

        [Header("Hole vertices")]
        [SerializeField] private Vector2 _moveLimits;
        [SerializeField] private float _holeRadius;
        [SerializeField] private Transform _holeCenter;
        
        [Space]
        [SerializeField] private float _moveSpeed;

        private Mesh _mesh;
        private List<int> _holeVertices = new ();
        private List<Vector3> _offsets = new ();
        private int _holeVerticesCount;

        private float x;
        private float y;
        private Vector3 _touch;
        private Vector3 _targetPosition;
        
        private void Start()
        {
            GameStates.isGameOver = false;
            GameStates.isMoving = false;
            
            _mesh = _holeMeshFilter.mesh;
            
            FindHoleVertices();
        }

        private void Update()
        {
            GameStates.isMoving = Input.GetMouseButton(0);

            if (!GameStates.isGameOver && GameStates.isMoving)
            {
                Move();
                UpdateHoleVertices();
            }
        }

        private void Move()
        {
            x = Input.GetAxis("Mouse X");
            y = Input.GetAxis("Mouse Y");

            _touch = Vector3.Lerp(
                _holeCenter.position, 
                _holeCenter.position + new Vector3(x, 0f, y), 
                _moveSpeed * Time.deltaTime);

            _targetPosition = new Vector3(
                Mathf.Clamp(_touch.x, -_moveLimits.x, _moveLimits.x), 
                _touch.y,
                Mathf.Clamp(_touch.z, -_moveLimits.y, _moveLimits.y));
            
            _holeCenter.position = _targetPosition;
        }

        private void UpdateHoleVertices()
        {
            Vector3[] vertices = _mesh.vertices;
            
            for (int i = 0; i < _holeVerticesCount; i++)
            {
                vertices[_holeVertices[i]] = _holeCenter.position + _offsets[i];
            }

            _mesh.vertices = vertices;
            _holeMeshFilter.mesh = _mesh;
            _holeMeshCollider.sharedMesh = _mesh;
        }

        private void FindHoleVertices()
        {
            for (int i = 0; i < _mesh.vertices.Length; i++)
            {
                float distance = Vector3.Distance(_holeCenter.position, _mesh.vertices[i]);

                if (distance < _holeRadius)
                {
                    _holeVertices.Add(i);
                    _offsets.Add(_mesh.vertices[i] - _holeCenter.position);
                }
            }

            _holeVerticesCount = _holeVertices.Count;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_holeCenter.position, _holeRadius);
        }
    }
}