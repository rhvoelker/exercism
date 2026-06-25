using System.Text;

public static class Identifier
{
    public static string Clean(string identifier) {
        var builder = new StringBuilder();
        var capitalizeAndIncludeCharacter = false;

        foreach (var c in identifier) {
            if (capitalizeAndIncludeCharacter) {
                builder.Append(char.ToUpper(c));
                capitalizeAndIncludeCharacter = false;
            }
            else if (c == ' ') {
                builder.Append('_');
            }
            else if (char.IsControl(c)) {
                builder.Append("CTRL");
            }
            else if (c == '-') {
                capitalizeAndIncludeCharacter = true;
            }
            else if (char.IsLetter(c) && (c < 'α' || c > 'ω')) {
                builder.Append(c);
            }
        }

        return builder.ToString();
    }
}
