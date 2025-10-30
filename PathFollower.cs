using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Transform _pathContainer;
    [SerializeField] private float _minDistanceToWaypoint = 0.1f;

    private Transform[] _pathPoints;
    private int _currentPointIndex;
    private float _sqrMinDistanceToWaypoint;

    private void Awake()
    {
        _sqrMinDistanceToWaypoint = _minDistanceToWaypoint * _minDistanceToWaypoint;
    }

    private void Start()
    {
        RefreshChildArray();
    }

    private void Update()
    {
        if (_pathPoints.Length == 0)
        {
            return;
        }

        var targetPoint = _pathPoints[_currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, _speed * Time.deltaTime);

        if ((transform.position - targetPoint.position).sqrMagnitude < _sqrMinDistanceToWaypoint)
        {
            UpdateToNextPoint();
        }
    }

    private void UpdateToNextPoint()
    {
        _currentPointIndex = (_currentPointIndex + 1) % _pathPoints.Length;

        var nextPointPosition = _pathPoints[_currentPointIndex].transform.position;
        transform.forward = nextPointPosition - transform.position;
    }
    
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int childCount = _pathContainer.childCount;
        _pathPoints = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            _pathPoints[i] = _pathContainer.GetChild(i);
        }
    }
}
