class WeighingMachine
{
    public int Precision { get; }

    public double Weight
    {
        get;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Weight must be non-negative.");
            }

            field = value;
        }
    }

    public double TareAdjustment { get; set; } = 5;

    public string DisplayWeight => (Weight - TareAdjustment).ToString($"0.{new string('0', Precision)} kg");

    public WeighingMachine(int precision)
    {
        Precision = precision;
    }
}
