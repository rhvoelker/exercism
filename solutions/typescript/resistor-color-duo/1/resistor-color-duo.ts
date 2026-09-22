export function decodedValue(colors: string[]): number {
  if (colors.length == 1) {
    return colorToDigit(colors[0]);
  }

  var result = 0;

  result += colorToDigit(colors[0]) * 10;
  result += colorToDigit(colors[1]);

  return result;
}

function colorToDigit(color: string): number {
  switch (color) {
    case 'brown':
      return 1;
    case 'red':
      return 2;
    case 'orange':
      return 3;
    case 'yellow':
      return 4;
    case 'green':
      return 5;
    case 'blue':
      return 6;
    case 'violet':
      return 7;
    case 'grey':
      return 8;
    case 'white':
      return 9;
    default:
      return 0;
  }
}
