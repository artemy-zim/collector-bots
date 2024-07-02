using System;

public interface IResource
{
    public void SetAssignedStatus(bool status);
    public bool IsAssigned();
}
