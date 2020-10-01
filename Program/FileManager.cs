using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScoreTracker.Program
{/// <summary>
/// Saves and loads objects of any kind
/// </summary>
    public class FileManager
    {
        /// <summary>
        /// Saves an object
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="fileName">Name of the file that will be created by the function</param>
        /// <param name="objectToWrite">The object that will be saved</param>
        /// <param name="append">If the file is going to append an existing file or create a new one</param>
        public static void Save<T>(string fileName, T objectToWrite, bool append = false)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + fileName +".txt";
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Load an object back to the program
        /// </summary>
        /// <typeparam name="T">The type of the object being loaded</typeparam>
        /// <param name="fileName">The name of the file it will load from (It needs to be the same as the save filename)</param>
        /// <returns>The object Loaded</returns>
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
