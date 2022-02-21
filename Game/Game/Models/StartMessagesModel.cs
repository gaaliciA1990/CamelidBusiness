using System;
using System.Collections.Generic;
using System.Linq;


namespace Game.Models
{
    public static class StartMessagesModelHelper
    {
        /// <summary>
        /// Start Messages List
        /// </summary>
        static List<string> StartMessages = new List<string>
        {
            "Lets Get Ready To Rumble!!!!!!",
            "Show These Monsters Who's Boss!!!",
            "Camelids Unite For Battle!!",
            "You Can Do It!!!!!",
            "You Sure You Wanna Do This?!",
            "Ready When You Are",
            "Hope For The Best\nGet Ready For The Worst",
            "Everyone Is\nGetting Ready\nFor A Big Battle.",
            "Lets Get This Camelid Business Going!!"
        };

        /// <summary>
        /// Gets random start message.
        /// </summary>
        public static string GetRandomMessage
        {
            get
            {
                Random rnd = new Random();
                int index = rnd.Next(StartMessages.Count);
                return StartMessages[index];
            }
        }
    }
}
