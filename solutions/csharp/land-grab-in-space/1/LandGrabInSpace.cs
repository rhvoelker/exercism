using System.Linq;

public struct Coord
{
    public Coord(ushort x, ushort y)
    {
        X = x;
        Y = y;
    }

    public ushort X { get; }
    public ushort Y { get; }

    public override bool Equals(object o) => o is Coord c && Equals(c);

    private bool Equals(Coord c) => X == c.X && Y == c.Y;

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public struct Plot
{
    public Plot(Coord a, Coord b, Coord c, Coord d)
    {
        var xs = new[] { a.X, b.X, c.X, d.X };
        var ys = new[] { a.Y, b.Y, c.Y, d.Y };

        var minX = xs.Min();
        var maxX = xs.Max();

        var minY = ys.Min();
        var maxY = ys.Max();

        TopLeft = new Coord(minX, maxY);
        TopRight = new Coord(maxX, maxY);
        BottomLeft = new Coord(minX, minY);
        BottomRight = new Coord(maxX, minY);
    }
    
    public Coord TopLeft { get; }
    public Coord TopRight { get; }
    public Coord BottomLeft { get; }
    public Coord BottomRight { get; }

    public ushort Width => (ushort)(TopRight.X - TopLeft.X);
    public ushort Height => (ushort)(TopRight.Y - BottomRight.Y);
    public ushort LongestSide => Math.Max(Width, Height);

    public override bool Equals(object o) => o is Plot p && Equals(p);

    private bool Equals(Plot p) => TopLeft.Equals(p.TopLeft)
        && TopRight.Equals(p.TopRight)
        && BottomLeft.Equals(p.BottomLeft)
        && BottomRight.Equals(p.BottomRight);

    public override int GetHashCode() => HashCode.Combine(
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight);
}


public class ClaimsHandler
{
    private readonly List<Plot> _claimedPlots = [];
    private Plot _lastClaim;
    private Plot _plotWithLongestSide;
    
    public void StakeClaim(Plot plot)
    {
        _lastClaim = plot;
        _claimedPlots.Add(plot);

        if (plot.LongestSide > _plotWithLongestSide.LongestSide)
        {
            _plotWithLongestSide = plot;
        }
    }

    public bool IsClaimStaked(Plot plot) => _claimedPlots.Contains(plot);

    public bool IsLastClaim(Plot plot) => plot.Equals(_lastClaim);

    public Plot GetClaimWithLongestSide() => _plotWithLongestSide;
}
