using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Submission
    {
        public static string xmlURL = "https://raw.githubusercontent.com/KyliesCreations/KyliesCreations.github.io/refs/heads/main/NationalParks.xml";
        public static string xmlErrorURL = "https://raw.githubusercontent.com/KyliesCreations/KyliesCreations.github.io/refs/heads/main/NationalParksErrors.xml";
        public static string xsdURL = "https://raw.githubusercontent.com/KyliesCreations/KyliesCreations.github.io/refs/heads/main/NationalParks.xsd";

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            string xmlContent = DownloadContent(xmlUrl);
            string xsdContent = DownloadContent(xsdUrl);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            StringReader xsdTextReader = new StringReader(xsdContent);
            XmlReader xsdReader = XmlReader.Create(xsdTextReader);
            settings.Schemas.Add(null, xsdReader);
            System.Text.StringBuilder errors = new System.Text.StringBuilder(); 
            settings.ValidationEventHandler += (sender, e) =>
            {
                errors.AppendLine(e.Message);
            };
            StringReader xmlTextReader = new StringReader(xmlContent);
            XmlReader xmlReader = XmlReader.Create(xmlTextReader, settings);
            try{
            while(xmlReader.Read())
            {
            }
            }
            catch (XmlException ex)
            {
                errors.AppendLine(ex.Message);
            }

            if(errors.Length == 0)
            {
                return "No errors are found";
            }
            else
            {
                return errors.ToString();
            }
        }


        public static string Xml2Json(string xmlUrl)
        {
            string xmlContent = DownloadContent(xmlUrl);
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlContent);
            string jsonText = JsonConvert.SerializeXmlNode(document);
            return jsonText;
            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
        }

        // Helper method to download content from URL
        private static string DownloadContent(string url)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }

}
