﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot.Types;


namespace TelegramBotFastStart
{

    public class ConfigApp
    {
        public IConfigurationRoot config { get; }
        public IConfigurationRoot dbConfig { get; }
        private static ConfigApp Instance;

        public static ConfigApp GetInstance()
        {
            if (Instance == null)
                Instance = new ConfigApp();
            return Instance;
        }

        private ConfigApp()
        {
            config = new ConfigurationBuilder()
                     .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                     .AddJsonFile("Configs/appconfig.json").Build();

            dbConfig = new ConfigurationBuilder()
                     .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                     .AddJsonFile("Configs/dbconfig.json").Build();
        }

        public static T GetSettings<T>()
        {
            var config      = GetInstance().config;
            var section     = config.GetSection(typeof(T).Name);
            return section.Get<T>();
        }

        public static T GetSettingsDB<T>()
        {
            var config = GetInstance().dbConfig;
            var section = config.GetSection(typeof(T).Name);
            return section.Get<T>();
        }
    }

    public class TelegramConfig
    {
        public string Token { get; set; }
    }

    public class DatabaseConfig
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    class SettingsConfig
    {
        public bool ShowNotifyRegisterUserForAdmin { get; set; }
        public List<long> Admins { get; set; }
    }

    class CustomSettings
    {
        public Dictionary<string, string> Variables { get; set; }
        public Dictionary<string, string> Messages { get; set; }
        public Dictionary<string, string> Buttons { get; set; }

        public string GetVariable(string command, [CallerMemberName] string methodName = "")
        {
            //FixVariables(Variables);
            string value;
            Variables.TryGetValue(command, out value);
            var result = value ?? $"NOT_FOUND_{command}";
            if(result.Contains("NOT_FOUND"))
            {
                Console.WriteLine($"Обнаружена проблема в переменной {result} | вызов {methodName}");
            }
            return result;
        }

        public string GetMessage(string message, [CallerMemberName] string methodName = "")
        {
            string value;
            Messages.TryGetValue(message, out value);
            var result = value != null ? GetFormatedMessage(value) : $"NOT_FOUND_{message}";
            if (result.Contains("NOT_FOUND"))
            {
                Console.WriteLine($"Обнаружена проблема в сообщение {result} | вызов {methodName}");
            }
            return result;
        }

        public string GetButton(string button, [CallerMemberName] string methodName = "")
        {
            string value;
            Buttons.TryGetValue(button, out value);
            var result = value ?? $"NOT_FOUND_{button}";
            if (result.Contains("NOT_FOUND"))
            {
                Console.WriteLine($"Обнаружена проблема в кнопке {result} | вызов {methodName}");
            }
            return result;
        }

        public string GetFormatedMessage(string text)
        {
            var pattern = @"{[A-za-z0-9]*}";
            var match = Regex.Matches(text, pattern);

            if(match.Count > 0)
            {
                foreach (var varible in match)
                {
                    var result = varible.ToString();
                    text = text.Replace(result, GetVariable(result.Replace("{","").Replace("}","")));
                }
            }

            return text;
        }
    }
}
