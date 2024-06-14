namespace Shared.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string name, string keyName, object key) : base($"Entity \"{name}\" with key {keyName} : ({key}) was not found.")
    {
    }
    
    public NotFoundException(string name, object key) : base($"Entity \"{name}\" with key ({key}) was not found.")
    {
    }
}
