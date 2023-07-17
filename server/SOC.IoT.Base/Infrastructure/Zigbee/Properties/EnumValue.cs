namespace SOC.IoT.Base.Infrastructure.Zigbee.Properties;

public class EnumValue
{
    private EnumProperty _meta;
    private object _value;

    public IEnumerable<object> Values => _meta.Values;

    public string? Value => _value?.ToString();

    public string? SetValue(object value)
    {
        if (!Values.Contains(value))
        {
            throw new InvalidOperationException(
                $"Enum value {value} is not registered with the enum {_meta.Name}."
            );
        }

        _value = value;

        return Value;
    }

    public EnumValue(Expose expose)
    {
        _meta = new EnumProperty { Values = expose.Values, Name = expose.Name };
    }

    public object this[object key]
    {
        get => Values.First(v => v.Equals(key));
    }
}
