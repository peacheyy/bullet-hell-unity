using System.Collections.Generic;
using UnityEngine;

public class BulletPatternManager : MonoBehaviour
{
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform[] _firePoints;
    private Dictionary<string, IBulletPattern> _patterns;
    private IBulletPattern _currentPattern;

    // getters and setters for current patter and bullet prefab
    public IBulletPattern CurrentPattern
    {
        get { return _currentPattern; }
        private set { _currentPattern = value; }
    }

    public GameObject BulletPrefab
    {
        get { return _bulletPrefab; }
        private set { _bulletPrefab = value; }
    }

    private void Start()
    {
        // possible implimentation would be to randomly select a bullet pattern from the _patterns dictionary
        _patterns = new Dictionary<string, IBulletPattern>
        {
            { "spiral", new SpiralPattern() }
        };
        _currentPattern = _patterns["spiral"];
    }

    private void Update()
    {
        // null propagation used which is basically an if statement checking if _currentPattern is null
        _currentPattern?.Execute(transform, _firePoints, _bulletPrefab);
    }

    public void SwitchPattern(string patternName)
    {
        if (_patterns.ContainsKey(patternName))
        {
            _currentPattern?.Reset();
            _currentPattern = _patterns[patternName];
        }
    }
}