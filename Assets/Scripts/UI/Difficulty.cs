public class Difficulty
{
    public Difficulty(float complexity, float instabilty, int maxDistance, int limitDistnace, int safePathDistance, string name)
    {
        Complexity = complexity;
        Instability = instabilty;
        MaxDistance = maxDistance;
        LimitDistance = limitDistnace;
        SafePathDistance = safePathDistance;
        Name = name;
    }
    public float Complexity { get; private set; }
    public float Instability { get; private set; }
    public int MaxDistance { get; internal set; }
    public int LimitDistance { get; internal set; }
    public int SafePathDistance { get; internal set; }
    public string Name { get; set; }
}
