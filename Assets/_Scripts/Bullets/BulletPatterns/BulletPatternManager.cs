using UnityEngine;
using System.Collections.Generic;

public class BulletPatternManager : MonoBehaviour
{
    [SerializeField] PatternConfig _config;
    [SerializeField] Transform[] _firePoints;

    private Dictionary<PatternType, IBulletPattern> _patterns;
    private IBulletPattern _currentPattern;
    private int _currentIndex;
    
    private void Start()
    {
        // this is a dictionary of all the bullet patterns available (currently only 2)
        _patterns = new Dictionary<PatternType, IBulletPattern>
        {
            { PatternType.Spiral, new SpiralPattern("EnemyBullet") },
            { PatternType.Circle, new CirclePattern("EnemyBullet") },
            { PatternType.Sine, new SinePattern("SineBullet") },
            { PatternType.Whip, new WhipPattern("EnemyBullet")}
        };

        _currentPattern = _patterns[_config.patternSequence[0]];
    }

    private void Update()
    {
        // handles time so the patterns are spaced out accordingly
        if (_currentPattern.IsComplete)
        {
            NextPattern();
        }

        _currentPattern?.Execute(transform, _firePoints);
    }

    // because the patterns are in a dictionary, I just need to iterate through to go to the next one
    private void NextPattern()
    {
        _currentIndex = (_currentIndex + 1) % _config.patternSequence.Length;
        _currentPattern?.Reset();
        _currentPattern = _patterns[_config.patternSequence[_currentIndex]];
    }
}