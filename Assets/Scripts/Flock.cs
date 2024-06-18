using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    private List<FlockAgent> _agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Header("Entity Spawning Variables")] 
    public Color agentColor;
    [Range(10, 500)] 
    public int startingCount = 250;
    private const float AgentDensity = 0.08f;

    [Header("Speed and Radius Factors")]
    [Range(1f, 100f)] 
    public float driveFactor = 10f;

    [Range(1f, 100f)] 
    public float maxSpeed = 5f;

    [Range(1f, 10f)] 
    public float neighborRadius = 1.5f;

    [Range(0f, 1f)] 
    public float avoidanceRadiusMultiplier = 0.5f;

    private float _squareMaxSpeed;
    private float _squareNeighborRadius;
    private float _squareAvoidanceRadius;
    public float SquareAvoidanceRadius
    {
        get { return _squareAvoidanceRadius; }
    }
    
    void Start()
    {
        _squareMaxSpeed = maxSpeed * maxSpeed;
        _squareNeighborRadius = neighborRadius * neighborRadius;
        _squareAvoidanceRadius = _squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0, 360)),
                transform
            );
            newAgent.name = "Agent #" + i;
            newAgent.agentColor = agentColor;
            newAgent.Initialize(this);
            _agents.Add(newAgent);
        }
    }

    void Update()
    {
        foreach (FlockAgent agent in _agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > _squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        
        return context;
    }
}
