public static class TelemetryBuffer
{
    private const byte PrefixLong = 256 - 8,
        PrefixUInt = 4,
        PrefixInt = 256 - 4,
        PrefixUShort = 2,
        PrefixShort = 256 - 2;
    
    public static byte[] ToBuffer(long reading)
    {
        var buffer = new byte[9];
        byte[] encoded;

        switch (reading)
        {
            case (> uint.MaxValue) or (< int.MinValue):
                buffer[0] = PrefixLong;
                encoded = BitConverter.GetBytes(reading);
                break;
            case > int.MaxValue:
                buffer[0] = PrefixUInt;
                encoded = BitConverter.GetBytes((uint)reading);
                break;
            case (> ushort.MaxValue) or (< short.MinValue):
                buffer[0] = PrefixInt;
                encoded = BitConverter.GetBytes((int)reading);
                break;
            case >= 0:
                buffer[0] = PrefixUShort;
                encoded = BitConverter.GetBytes((ushort)reading);
                break;
            default:
                buffer[0] = PrefixShort;
                encoded = BitConverter.GetBytes((short)reading);
                break;
        }

        encoded.CopyTo(buffer, 1);

        return buffer;
    }

    public static long FromBuffer(byte[] buffer) => buffer[0] switch
    {
        PrefixLong => BitConverter.ToInt64(buffer, 1),
        PrefixUInt => BitConverter.ToUInt32(buffer, 1),
        PrefixInt => BitConverter.ToInt32(buffer, 1),
        PrefixUShort => BitConverter.ToUInt16(buffer, 1),
        PrefixShort => BitConverter.ToInt16(buffer, 1),
        _ => 0
    };
}
