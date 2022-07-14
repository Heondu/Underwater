using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Modifier
{
    public StatusType Type;
    public float Value;
}

public enum StatusType
{
    moveSpeed,
    rushSpeed,
    rushTime,
    rushCooltime
}

public class Status : MonoBehaviour
{
    [SerializeField]
    private Modifier[] modifiers;

    [Header("Event")]
    [HideInInspector] public UnityEvent<Status, StatusType, float, float> onModifierUpdate;

    public float MoveSpeed => GetModifier(StatusType.moveSpeed).Value;
    public float RushSpeed => GetModifier(StatusType.rushSpeed).Value;
    public float RushTime => GetModifier(StatusType.rushTime).Value;
    public float RushCooltime => GetModifier(StatusType.rushCooltime).Value;

    public float GetValue(StatusType type) => GetModifier(type).Value;
    public void SetValue(StatusType type, float value)
    {
        Modifier modifier = GetModifier(type);
        float prevValue = modifier.Value;
        modifier.Value = value;

        onModifierUpdate.Invoke(this, type, value, prevValue);
    }

    public Modifier GetModifier(StatusType type) => modifiers.First(x => x.Type == type);
}
