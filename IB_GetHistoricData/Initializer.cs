using System.IO;

namespace IB_GetHistoricData
{
    internal class Initializer
    {
        internal void Initialize()
        {
            Directory.CreateDirectory(Properties.App.Default.LogsFolderPath);
        }
    }
}