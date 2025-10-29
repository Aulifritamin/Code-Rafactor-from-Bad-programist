public class PathFollower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _pathContainer;
    
    private Transform[] _pathPoints;
    private int _currentPointIndex;

    private void Start()
    {
        _pathPoints = new Transform[_pathContainer.childCount];

        for (int i = 0; i < _pathContainer.childCount; i++)
        {
            _pathPoints[i] = _pathContainer.GetChild(i);
        }
    }

    private void Update()
    {
        if (_pathPoints.Length == 0)
        {
            return;
        }

        var targetPoint = _pathPoints[_currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, _speed * Time.deltaTime);

        if (transform.position == targetPoint.position)
        {
            UpdateToNextPoint();
        }
    }

    private void UpdateToNextPoint()
    {
        _currentPointIndex++;

        if (_currentPointIndex == _pathPoints.Length)
        {
            _currentPointIndex = 0;
        }
        
        var nextPointPosition = _pathPoints[_currentPointIndex].transform.position;
        transform.forward = nextPointPosition - transform.position;
    }
}