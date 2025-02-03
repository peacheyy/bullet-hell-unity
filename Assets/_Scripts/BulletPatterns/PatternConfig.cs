using UnityEngine;

[CreateAssetMenu(fileName = "PatternConfig", menuName = "Bullets/Pattern Config")]
public class PatternConfig : ScriptableObject
{
    public PatternType[] patternSequence;
    public float switchInterval;
}

public enum PatternType
{
    Spiral,
    Circle
}