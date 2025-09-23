namespace TrainSim;

public class Train
{
    public int Position;
    public int Length;

    public Train(int length)
    {
        Length = length;
        Position = -length;
    }

    public void Step()
    {
        Position++;
    }

    public bool IsOutOfTrack(int trackLength)
    {
        return Position - Length > trackLength;
    }
}