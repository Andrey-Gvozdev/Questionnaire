using System.ComponentModel.DataAnnotations;

namespace Questionnaire.Domain.Model;

public class QuestionDefinition
{
    public Guid Id { get; private set; }
    
    public string Name { get; private set; }

    public QuestionDefinitionType Type { get; private set; }

    public QuestionDefinitionUIType UIType { get; private set; }

    public Validation Validation { get; set; }

    public QuestionDefinition(string name, Validation validation)
    {
        Name = name;
        Validation = validation;
        UIType = ValidetionDefinitionValidation(Validation);
    }

    private QuestionDefinitionUIType ValidetionDefinitionValidation(Validation validation)
    {
        if ((validation.MaxValue != 0 || validation.MinValue != 0))
        {
            if (validation.MaxLength == 0 &&
                validation.MinLength == 0 &&
                validation.IsRadioButtons == false &&
                validation.IsPersent == false)
            {
                return QuestionDefinitionUIType.Number;
            }
            else
            {
                throw new ValidationException("Too many options selected");
            }
        }
        if ((validation.MaxLength != 0 || validation.MaxLength != 0))
        {
            if (validation.MaxValue == 0 &&
                validation.MaxValue == 0 &&
                validation.IsRadioButtons == false &&
                validation.IsPersent == false)
            {
                return QuestionDefinitionUIType.Text;
            }
            else
            {
                throw new ValidationException("Too many options selected");
            }
        }
        if ((validation.IsRadioButtons == true))
        {
            if (validation.MaxValue == 0 &&
                validation.MaxLength == 0 &&
                validation.MaxLength == 0 &&
                validation.MaxValue == 0 &&
                validation.IsPersent == false)
            {
                return QuestionDefinitionUIType.RadioButton;
            }
            else
            {
                throw new ValidationException("Too many options selected");
            }
        }
        if ((validation.IsPersent == true))
        {
            if (validation.MaxValue == 0 &&
                validation.MaxLength == 0 &&
                validation.MaxLength == 0 &&
                validation.MaxValue == 0 &&
                validation.IsRadioButtons == false)
            {
                return QuestionDefinitionUIType.Percent;
            }
            else
            {
                throw new ValidationException("Too many options selected");
            }
        }
        throw new ValidationException("Validation parameters are empty");
    }
}