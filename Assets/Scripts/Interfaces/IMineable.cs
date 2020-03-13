using UnityEngine;

public enum ResourceType
{
    Metal,
    RareMetal,
    Ice,
}

interface IMineable
{
    uint OnMine(uint amount);
    ResourceType GetResourceType();
}
