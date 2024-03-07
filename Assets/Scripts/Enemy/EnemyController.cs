using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    enum EnemyState
    {
        Patrol = 0,
        Investigate = 1
    }
    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _threshold = 0.5f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private PatrolRoute _patrolRoute;
    [SerializeField] private FieldOfView _fov;
    [SerializeField] private EnemyState _state = EnemyState.Patrol;

    private bool _moving = false;
    private Transform _currentPoint;
    private int _routeIndex = 0;
    private bool _forwardsAlongPath = true;
    private Vector3 _investigationPoint;
    private float _waitTimer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentPoint = _patrolRoute.route[_routeIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (_fov.visibleObjects.Count > 0)
        {
            InvestigatePoint(_fov.visibleObjects[0].position);
        }
        
        if (_state == EnemyState.Patrol)
        {
            UpdatePatrol();
        }
        else if(_state == EnemyState.Investigate)
        {
            UpdateInvestigate();
        }
    }

    public void InvestigatePoint(Vector3 investigatePoint)
    {
        //Debug.Log("Investigate Point Triggered");
        _state = EnemyState.Investigate;
        _investigationPoint = investigatePoint;
        _agent.SetDestination(_investigationPoint);
    }

    private void UpdateInvestigate()
    {
        //Debug.Log("Investigating");
        if (Vector3.Distance(transform.position, _investigationPoint) < _threshold)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer > _waitTime)
            {
                ReturnToPatrol();
            }
        }
    }

    private void ReturnToPatrol()
    {
        Debug.Log("Enemy returning to patrol");
        _state = EnemyState.Patrol;
        _waitTimer = 0;
        _moving = false;
    }

    private void UpdatePatrol()
    {
        if (!_moving)
        {
            NextPatrolPoint();

            _agent.SetDestination(_currentPoint.position);
            _moving = true;
        }

        if (_moving && Vector3.Distance(transform.position, _currentPoint.position) < _threshold)
        {
            _moving = false;
        }
    }

    private void NextPatrolPoint()
    {
        if (_forwardsAlongPath)
        {
            _routeIndex++;
        }
        else
        {
            _routeIndex--;
        }
            
        if (_routeIndex == _patrolRoute.route.Count)
        {
            if (_patrolRoute.patrolType == PatrolRoute.PatrolType.Loop)
            {
                _routeIndex = 0;
            }
            else
            {
                _forwardsAlongPath = false;
                _routeIndex-=2;
            }
        }

        if (_routeIndex == 0)
        {
            _forwardsAlongPath = true;
        }
        
        //Debug.Log(_routeIndex);
            
        _currentPoint = _patrolRoute.route[_routeIndex];
    }
}
