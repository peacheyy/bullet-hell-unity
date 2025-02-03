using System.Collections.Generic;
using UnityEngine;

public class BulletPatternManager : MonoBehaviour
{
    [SerializeField] PatternConfig _config;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform[] _firePoints;

    private Dictionary<PatternType, IBulletPattern> _patterns;
    private IBulletPattern _currentPattern;
    private int _currentIndex;
    private float _timer;

    private void Start()
    {
        _patterns = new Dictionary<PatternType, IBulletPattern>
        {
            { PatternType.Spiral, new SpiralPattern() },
            { PatternType.Circle, new CirclePattern() }
        };

        _currentPattern = _patterns[_config.patternSequence[0]];
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _config.switchInterval)
        {
            NextPattern();
            _timer = 0f;
        }

        _currentPattern?.Execute(transform, _firePoints, _bulletPrefab);
    }

    private void NextPattern()
    {
        _currentIndex = (_currentIndex + 1) % _config.patternSequence.Length;
        _currentPattern?.Reset();
        _currentPattern = _patterns[_config.patternSequence[_currentIndex]];
    }
}