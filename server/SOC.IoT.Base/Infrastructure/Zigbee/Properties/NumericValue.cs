using Newtonsoft.Json;

namespace SOC.IoT.Base.Infrastructure.Zigbee.Properties;

public class NumericValue
{
    private decimal _value { get; set; }
    private NumericProperty _meta { get; set; }

    public NumericValue(string jsonDefinition)
    {
        _meta = JsonConvert.DeserializeObject<NumericProperty>(jsonDefinition);
    }

    public NumericValue(Expose expose)
    {
        _meta = new NumericProperty
        {
            Name = expose.Name,
            ValueMin = expose.ValueMin,
            ValueMax = expose.ValueMax,
        };
    }

    public decimal Value => _value;

    public decimal PercentageOrValue => CanSetPercentage ? Value / _meta.ValueMax.Value : Value;

    public bool CanSetPercentage => _meta?.ValueMax is not null;

    public decimal SetValue(decimal value)
    {
        if (_meta.ValueStep is not null)
        {
            int stepsInValue = Convert.ToInt32(value / _meta.ValueStep.Value);
            value = stepsInValue * _meta.ValueStep.Value;
        }

        if (_meta.ValueMin is not null && value < _meta.ValueMin.Value)
        {
            value = _meta.ValueMin.Value;
        }

        if (_meta.ValueMax is not null && value > _meta.ValueMax.Value)
        {
            value = _meta.ValueMax.Value;
        }

        _value = value;
        return Value;
    }

    public decimal SetPercentage(decimal percentage)
    {
        if (!CanSetPercentage)
        {
            throw new InvalidOperationException(
                "This numeric value does not support percentage setting."
            );
        }

        decimal validPercentage = Math.Clamp(percentage, 0, 1);
        decimal minValue = _meta.ValueMin ?? 0m,
            maxValue = _meta.ValueMax ?? 0m;
        decimal calculatedValue = minValue + validPercentage * (maxValue - minValue);

        return SetValue(calculatedValue);
    }

    public bool TrySetPercentage(decimal percentage)
    {
        if (CanSetPercentage)
        {
            SetPercentage(percentage);
            return true;
        }

        return false;
    }
}
