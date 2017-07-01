﻿using System;
using System.Collections.Generic;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using Wikiled.Core.Utility.Arguments;

namespace Wikiled.Twitter.Discovery
{
    public class MessageDiscovery
    {
        private readonly string[] topics;

        private readonly string[] enrichment;

        private readonly HashSet<long> processed = new HashSet<long>();

        public MessageDiscovery(string[] topics, string[] enrichment)
        {
            Guard.NotNull(() => topics, topics);
            Guard.NotNull(() => enrichment, enrichment);
            this.topics = topics;
            this.enrichment = enrichment;
        }

        public IEnumerable<ITweet> Process()
        {
            foreach (var enrichmentItem in enrichment)
            {
                foreach (var topic in topics)
                {
                    int total = 0;
                    DateTime lastSearch = DateTime.Now;
                    do
                    {
                        total = 0;
                        var searchParameter = GetParameter(topic, enrichmentItem, lastSearch);
                        var tweets = Search.SearchTweets(searchParameter);

                        foreach (var tweet in tweets)
                        {
                            total++;
                            if (tweet.CreatedAt < lastSearch)
                            {
                                lastSearch = tweet.CreatedAt;
                            }

                            if (!processed.Contains(tweet.Id))
                            {
                                processed.Add(tweet.Id);
                                yield return tweet;
                            }
                        }
                    }
                    while (total > 0);
                }
            }
        }

        private SearchTweetsParameters GetParameter(string topic, string enrichmentItem, DateTime until)
        {
            var searchParameter = new SearchTweetsParameters($"\"{topic}\" {enrichmentItem} -filter:retweets");
            searchParameter.Lang = LanguageFilter.English;
            searchParameter.Until = until;
            return searchParameter;

        }
    }
}