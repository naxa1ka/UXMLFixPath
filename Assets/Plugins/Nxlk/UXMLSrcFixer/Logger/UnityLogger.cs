using UnityEngine;

namespace Nxlk.UXMLSrcFixer
{
    public class UnityLogger : ILogger
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void LogError(string message)
        {
            Debug.LogError(message);
        }
    }
}