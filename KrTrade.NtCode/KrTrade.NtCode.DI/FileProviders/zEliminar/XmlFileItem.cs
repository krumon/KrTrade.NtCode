using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace KrTrade.NtCode.Files
{
    public class XmlFileItem
    {

        #region Public properties

        /// <summary>
        /// The display name of the file.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The path of the file.
        /// </summary>
        public string Path { get; set; }

        #endregion

        #region Public methods
        
        /// <summary>
        /// Deserializs any xml file
        /// </summary>
        /// <typeparam name="T">The class to store the xml file.</typeparam>
        /// <returns>The object with the xml file.</returns>
        public T Deserialize<T>()
            where T : class
        {
            // Make sure i have a path
            if (string.IsNullOrEmpty(Path))
                return null;

            // Make sure file exists
            if (!File.Exists(Path))
                return null;

            // Create the xml serializer
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            serializer.UnknownNode += Serializer_UnknownNode;
            serializer.UnknownAttribute += new XmlAttributeEventHandler(Serializer_UnknownAttribute);

            // Declare an object variable of the type to be deserialized.
            T obj;

            using (Stream reader = new FileStream(Path, FileMode.Open))
            {
                // Call the Deserialize method to restore the object's state.
                obj = (T)serializer.Deserialize(reader);
            }

            return obj;
        }

        private void Serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            Debugger.Break();
            throw new Exception("Unknown node in the xml file.");
        }

        private void Serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            throw new Exception("Unknown attribute in the xml file.");
        }

        #endregion


    }
}
