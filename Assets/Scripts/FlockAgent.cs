using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    [SerializeField] private List<GameObject> trailblazes;
    private Collider2D _agentCollider;
    private Flock _agentFlock;
    public Color agentColor;

    private bool _isTrailEnabled = true;
    private bool _isWhiteMode = true;

    private SpriteRenderer _spriteRenderer;
    
    public Flock AgentFlock
    {
        get { return _agentFlock; }
    }
    public Collider2D AgentCollider
    {
        get { return _agentCollider; }
    }
    
    void Start()
    {
        _agentCollider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.color = new Color(agentColor.r, agentColor.g, agentColor.b, 1f);
        GetComponentInChildren<Light>().color = new Color(agentColor.r, agentColor.g, agentColor.b, 1f);
        trailblazes[1].SetActive(false);
        TrailRenderer[] trails = trailblazes[0].GetComponentsInChildren<TrailRenderer>();
        foreach (var trail in trails)
        {
            Gradient gradient = new Gradient
            {
                colorKeys = new[]
                    { new GradientColorKey(new Color(agentColor.r, agentColor.g, agentColor.b), 0f) },
                alphaKeys = new[]
                {
                    new GradientAlphaKey(1f, 0f),
                    new GradientAlphaKey(0f, 1f)
                }
            };
            trail.colorGradient = gradient;
        }
    }

    private void Update()
    {
        switch (HotkeyController.IsTrailEnabled)
        {
            case true when !_isTrailEnabled:
            {
                TrailRenderer[] trails = GetComponentsInChildren<TrailRenderer>();
                foreach (var trail in trails)
                {
                    trail.enabled = true;
                }

                _isTrailEnabled = true;
                break;
            }
            case false when _isTrailEnabled:
            {
                TrailRenderer[] trails = GetComponentsInChildren<TrailRenderer>();
                foreach (var trail in trails)
                {
                    trail.enabled = false;
                }

                _isTrailEnabled = false;
                break;
            }
        }

        switch (HotkeyController.IsWhiteMode)
        {
            case true when !_isWhiteMode:
                trailblazes[1].SetActive(true);
                trailblazes[0].SetActive(false);
                _spriteRenderer.color = Color.white;
                _isWhiteMode = true;
                break;
            case false when _isWhiteMode:
                trailblazes[1].SetActive(false);
                trailblazes[0].SetActive(true);
                _spriteRenderer.color = new Color(agentColor.r, agentColor.g, agentColor.b, 1f);
                _isWhiteMode = false;
                break;
        }
    }

    public void Initialize(Flock flock)
    {
        _agentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
