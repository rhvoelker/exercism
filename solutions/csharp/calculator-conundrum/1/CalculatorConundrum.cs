public static class SimpleCalculator
{
    private static string FormatResult(int a, int b, string operation, int result) => $"{a} {operation} {b} = {result}";
    
    private static readonly Dictionary<string, Func<int, int, string>> _operations = new() 
    {
        { "+", (a, b) => FormatResult(a, b, "+", a + b) },
        { "*", (a, b) => FormatResult(a, b, "*", a * b) },
        { "/", (a, b) => b != 0 ? FormatResult(a, b, "/", a / b) : "Division by zero is not allowed." },
    };
    
    public static string Calculate(int operand1, int operand2, string? operation)
    {
        if (operation == string.Empty) {
            throw new ArgumentException(operation, nameof(operation));
        }

        if (operation == null) {
            throw new ArgumentNullException(nameof(operation));
        }

        if (!_operations.TryGetValue(operation, out var func)) {
            throw new ArgumentOutOfRangeException(nameof(operation));
        }
        
        return func(operand1, operand2);
    }
}
