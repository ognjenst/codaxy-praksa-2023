using Newtonsoft.Json;

namespace SOC.IoT.Base.Infrastructure.Zigbee.Properties;

public class BinaryValue
{
    private object _value { get; set; }
    private BinaryProperty _meta { get; set; }

    public BinaryValue(string definition)
    {
        _meta = JsonConvert.DeserializeObject<BinaryProperty>(definition);
    }

    public BinaryValue(Expose expose)
    {
        _meta = new BinaryProperty
        {
            ValueOn = expose.ValueOn,
            ValueOff = expose.ValueOff,
            ValueToggle = expose.ValueToggle
        };
    }

    public BinaryValue() { }

    public bool? Value => _value?.Equals(_meta.ValueOn);

    public object SetValue(object value)
    {
        if (value is null)
        {
            return _value;
        }

        if (value is true)
        {
            _value = _meta.ValueOn;
        }

        if (value is false)
        {
            _value = _meta.ValueOff;
        }

        if (value.Equals(_meta.ValueOn))
        {
            _value = _meta.ValueOn;
        }

        if (value.Equals(_meta.ValueOff))
        {
            _value = _meta.ValueOff;
        }

        // Toggle should never be recieved as a value, but can be set as a value
        if (value.Equals(_meta.ValueToggle))
        {
            _value = _meta.ValueToggle;
        }

        return _value;
    }

    public bool? Toggle()
    {
        if (_meta.ValueToggle is not null)
        {
            bool? oldValue = Value;
            _value = _meta.ValueToggle;
            return !oldValue;
        }

        _value = _value.Equals(_meta.ValueOn) ? _meta.ValueOff : _meta.ValueOn;
        return Value;
    }
}
