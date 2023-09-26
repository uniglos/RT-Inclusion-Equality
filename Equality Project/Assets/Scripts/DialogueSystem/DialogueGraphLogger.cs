using UnityEngine;

#if UNITY_EDITOR

public static class DialogueGraphLogger
{
    public enum ELogError {
        Trace,
        Warning,
        Error,
    }

    public static void Log(string text, ELogError error = ELogError.Trace, Object uObject = null) {
        switch (error) {
            case ELogError.Trace:
                Debug.Log(text, uObject);
                break;
            case ELogError.Warning:
                Debug.LogWarning(text, uObject);
                break;
            case ELogError.Error:
                Debug.LogError(text, uObject);
                Debug.Break();
                break;
            default:
                break;
        }
    }
}

#endif