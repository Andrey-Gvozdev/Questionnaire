namespace Questionnaire.Domain.Model;

public class Validation
{
    public int MaxValue { get; private set; }

    public int MinValue { get; private set; }

    public int MaxLength { get; private set; }

    public int MinLength { get; private set; }

    public bool IsPersent { get; private set; }

    public bool IsRadioButtons { get; private set; }

    // TODO: remove this constructor, the is no sense in constructor
    // that allow to leave its parameters empty
    public Validation(int maxValue = 0, int minValue = 0, int maxLength = 0, int minLength = 0, bool isPersent = false, bool isRadioButtons = false)
    {
        MaxValue = maxValue;
        MinValue = minValue;
        MaxLength = maxLength;
        MinLength = minLength;
        IsPersent = isPersent;
        IsRadioButtons = isRadioButtons;
    }
}
