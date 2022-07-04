namespace Questionnaire.Domain.Model;

public class Validation
{
    public int MaxValue { get; private set; }

    public int MinValue { get; private set; }

    public int MaxLength { get; private set; }

    public int MinLength { get; private set; }

    public bool IsRadioButtons { get; private set; }

    public Validation(int maxValue = int.MaxValue, int minValue = int.MinValue, int maxLength = 2000, int minLength = 0, bool isRadioButtons = false)
    {
        MaxValue = maxValue;
        MinValue = minValue;
        MaxLength = maxLength;
        MinLength = minLength;
        IsRadioButtons = isRadioButtons;
    }
}
