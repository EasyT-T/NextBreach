namespace NextBreach.Math;

public class Random
{
    private const int RndA = 48271;
    private const int RndM = 2147483647;
    private const int RndQ = 44488;
    private const int RndR = 3399;

    private int state;

    public Random()
    {
        SeedRnd(0x1234);
    }

    public float Rand()
    {
        state = RndA * (state % RndQ) - RndR * (state / RndQ);
        if (state < 0) state += RndM;
        return (state & 65535) / 65536.0f + 0.5f / 65536.0f;
    }

    public float Rand(float from, float to)
    {
        return Rand() * (to - from) + from;
    }

    public int Rand(int from, int to)
    {
        if (to < from) (from, to) = (to, from);
        return (int)(Rand() * (to - from + 1)) + from;
    }

    public void SeedRnd(int seed)
    {
        seed &= 0x7fffffff;
        state = seed != 0 ? seed : 1;
    }

    public int RndSeed()
    {
        return state;
    }
}