using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScoreTracker.Program
{
    public class FileManager
    {
        public static void Save<T>(string fileName, T objectToWrite, bool append = false)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + fileName +".txt";
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public static T Load<T>(string fileName)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + fileName + ".txt";
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
