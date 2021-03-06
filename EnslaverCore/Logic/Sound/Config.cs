﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Configuration;

namespace EnslaverCore.Logic.Sound
{
    [Serializable]
    public class UserStatePhrases
    {
        public UserStates state;
        public Phrase[] phrases;
    }

    public static class PhrasesConfig
    {
        private static Dictionary<UserStates, Phrase[]> cache = new Dictionary<UserStates, Phrase[]>();

        public static void Load(string fileName)
        {
            var all = new List<Phrase>();

            var data = new XmlSerializer(typeof(UserStatePhrases[])).Deserialize(File.OpenRead(fileName)) as UserStatePhrases[];
            foreach (var datum in data)
            {
                foreach (var phrase in datum.phrases)
                {
                    phrase.Text = string.Format(phrase.Text, ConfigurationManager.AppSettings["owner"]);
                }

                cache.Add(datum.state, datum.phrases);

                all.AddRange(datum.phrases);
            }

            AllPhrases = all.ToArray();
        }

        public static Phrase[] GetPhrases(UserStates userState)
        {
            if (cache.ContainsKey(userState))
            {
                return cache[userState];
            }
            else
            {
                return new Phrase[] { };
            }
        }

        public static Phrase[] AllPhrases
        {
            get;
            private set;
        }

        public static void Save(string fileName, UserStatePhrases[] data)
        {
            new XmlSerializer(typeof(UserStatePhrases[])).Serialize(File.OpenWrite(fileName), data);
        }
    }
}
