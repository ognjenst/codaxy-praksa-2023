using SOC.IoT.Base.Infrastructure.Zigbee;

namespace SOC.IoT.Base.Infrastructure.Zigbee.Properties;

public class CompositeValue
{
    private CompositeProperty _meta;
    private Dictionary<string, NumericValue> _numericValues;
    private Dictionary<string, BinaryValue> _binaryValues;

    public Dictionary<string, NumericValue> NumericValues => _numericValues;
    public Dictionary<string, BinaryValue> BinaryValues => _binaryValues;

    public CompositeValue(Expose expose)
    {
        _meta = new CompositeProperty { Features = expose.Features.ToArray() };

        _numericValues =
            expose.Features
                .Where(f => f.Type == "numeric")
                ?.ToDictionary(keySelector: f => f.Name, elementSelector: f => new NumericValue(f))
            ?? new();
        _binaryValues =
            _meta.Features
                .Where(f => f.Type == "binary")
                ?.ToDictionary(keySelector: f => f.Name, elementSelector: f => new BinaryValue(f))
            ?? new();
    }

    public decimal SetNumericValue(string name, decimal value) =>
        _numericValues[name].SetValue(value);

    public object SetBinaryValue(string name, object value) => _binaryValues[name].SetValue(value);
}
