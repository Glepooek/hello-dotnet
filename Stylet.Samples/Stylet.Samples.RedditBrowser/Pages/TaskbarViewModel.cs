using Stylet.Samples.RedditBrowser.Events;
using Stylet.Samples.RedditBrowser.RedditApi;
using System.Collections.Generic;

namespace Stylet.Samples.RedditBrowser.Pages
{
    public class TaskbarViewModel : Screen
    {
        private readonly IEventAggregator eventAggregator;
        public string Subreddit { get; set; }
        public IEnumerable<SortMode> SortModes { get; set; }
        public SortMode SelectedSortMode { get; set; }

        public TaskbarViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.SortModes = SortMode.AllModes;
            this.SelectedSortMode = SortMode.Hot;
        }

        public bool CanOpen
        {
            get { return !string.IsNullOrWhiteSpace(this.Subreddit); }
        }

        public void Open()
        {
            eventAggregator.Publish(new OpenSubredditEvent() { Subreddit = this.Subreddit, SortMode = this.SelectedSortMode });
        }

    }
}
