namespace Questionnaire.Domain.Model;

public class Validation
{
    public int MaxValue { get; private set; }

    public int MinValue { get; private set; }

    public int MaxLength { get; private set; }

    public int MinLength { get; private set; }

    public bool IsRadioButtons { get; private set; }
}
