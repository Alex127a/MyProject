namespace MyProjectService.Core;

#pragma warning disable CA1032, RCS1194
public class LocationNameAlreadyExistsException : Exception
{
    public LocationNameAlreadyExistsException(string name) 
        : base($"Локация с именем '{name}' уже существует в системе.")
    {
    }
}
#pragma warning restore CA1032, RCS1194