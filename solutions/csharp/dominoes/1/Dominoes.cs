using System.Runtime.CompilerServices;

public static class Dominoes
{    
    private class DominoReferenceEqualityComparer : IEqualityComparer<Domino>
    {
        public static DominoReferenceEqualityComparer Instance { get; } = new();
        
        private DominoReferenceEqualityComparer() {}
        
        public bool Equals(Domino a, Domino b) => object.ReferenceEquals(a, b);

        public int GetHashCode(Domino d) => RuntimeHelpers.GetHashCode(d);
    }
    
    private record Domino(int Left, int Right);

    private record DominoChain(int Start, int End)
    {
        public bool CanPlaceOnChain(Domino newDomino) => End == newDomino.Left || End == newDomino.Right;

        public DominoChain AddDominoToChain(Domino newDomino)
        {
            return new DominoChain(
                Start,
                End == newDomino.Left
                    ? newDomino.Right
                    : newDomino.Left);
        }
    }
    
    public static bool CanChain(IEnumerable<(int, int)> dominoes)
    {
        if (!dominoes.Any())
        {
            return true;
        }
        
        var dominoSet = new HashSet<Domino>(
            dominoes.Select(d => new Domino(d.Item1, d.Item2)),
            DominoReferenceEqualityComparer.Instance);

        var firstDomino = dominoSet.First();
        dominoSet.Remove(firstDomino);

        return CanChain(new DominoChain(firstDomino.Left, firstDomino.Right), dominoSet);
    }

    private static bool CanChain(DominoChain chain, HashSet<Domino> rest)
    {
        if (rest.Count == 0)
        {
            return chain.Start == chain.End;
        }

        foreach (var nextDomino in rest.Where(d => chain.CanPlaceOnChain(d)))
        {
            var newRest = new HashSet<Domino>(rest, DominoReferenceEqualityComparer.Instance);
            newRest.Remove(nextDomino);
            
            if (CanChain(chain.AddDominoToChain(nextDomino), newRest))
            {
                return true;
            }
        }

        return false;
    }
}