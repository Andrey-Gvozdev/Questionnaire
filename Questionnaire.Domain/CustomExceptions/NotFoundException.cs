namespace Questionnaire.Domain.CustomExceptions;

// TODO: better to generate all three constructor,
// cause what if someone wants to place and inner exception inside
public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message)
    {
    }
}