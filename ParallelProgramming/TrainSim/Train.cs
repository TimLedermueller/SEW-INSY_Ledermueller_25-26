namespace TrainSim;

public class Train
{
        public int Position { get; private set; }
        public int Length { get; }

        public Train(int length)
        {
            Length = length;
            Position = -length; // startet links außerhalb
        }

        public void Step()
        {
            Position++;
        }
}