using System;
using System.IO;
using System.Net;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RpgBase
{
    public class WebInteractions
    {
        public static string Get()
        {
            // Create a request for the URL.
            WebRequest request = WebRequest.Create(
              "https://mediaology.com/testing/");
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            string responseFromServer;

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
            }

            // Close the response.
            response.Close();

            return responseFromServer;
        }
        public static void RenderFW(string fwFile, SpriteBatch _spriteBatch, SpriteFont defont)
        {
            Stack tagstack = new Stack();
            string[] tags = {"bg"};
            fwFile = fwFile.Replace("]", "/]");
            fwFile = fwFile.Replace("[", "[/");
            fwFile = fwFile.Replace("]", "[");
            
            foreach (string item in fwFile.Split("["))
            {
                if(item.EndsWith('/') && item.StartsWith('/') && !item.StartsWith("//"))
                {
                    tagstack.Push(item.Replace("/", ""));
                }
                else if(tagstack.Count > 0 && item.EndsWith('/') && item.StartsWith("//") && item.Replace("/", "") == (string)tagstack.Peek())
                {
                    tagstack.Pop();
                }else if(tagstack.Count > 0)
                {
                    //Console.WriteLine(item + " : " + tagstack.Peek());
                    //_spriteBatch.DrawString(defont, item, new Vector2(0, 20), Color.Black);
                    HandleTag(item, (string)tagstack.Peek(), _spriteBatch, defont);
                }
            }
        }
        public static void HandleTag(string text, string param, SpriteBatch _spriteBatch, SpriteFont defont)
        {
            string[] tagparmarr = param.Split(" ");
            switch (tagparmarr[0])
            {
                case "bg":
                    _spriteBatch.DrawString(defont, text, new Vector2(0, 0), Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);
                break;
                case "blue":
                    _spriteBatch.DrawString(defont, text, new Vector2(0, 20), Color.SkyBlue, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);
                break;
            }
        }
    }
}