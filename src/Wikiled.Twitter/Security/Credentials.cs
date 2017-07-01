﻿using Tweetinvi;
using Tweetinvi.Models;

namespace Wikiled.Twitter.Security
{
    public class Credentials
    {
        private Credentials()
        {
            IphoneTwitterCredentials = Auth.SetApplicationOnlyCredentials("IQKbtAYlXLripLGPWd0HUA", "GgDYlkSvaPxGxC4X8liwpUoqKwwr3lCADbz8A7ADU");
        }

        public ITwitterCredentials IphoneTwitterCredentials { get; }

        public static Credentials Instance { get; } = new Credentials();
    }
}