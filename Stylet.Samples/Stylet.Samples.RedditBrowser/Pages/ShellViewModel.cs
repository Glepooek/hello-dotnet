using Stylet.Samples.RedditBrowser.Events;

namespace Stylet.Samples.RedditBrowser.Pages
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<OpenSubredditEvent>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly ISubredditViewModelFactory subredditViewModelFactory;
        public TaskbarViewModel TaskbarViewModel { get; set; }

        public ShellViewModel(IEventAggregator eventAggregator, TaskbarViewModel taskbarViewModel, ISubredditViewModelFactory subredditViewModelFactory)
        {
            this.DisplayName = "Reddit Browser";
            this.eventAggregator = eventAggregator;
            this.TaskbarViewModel = taskbarViewModel;
            this.subredditViewModelFactory = subredditViewModelFactory;

            this.eventAggregator.Subscribe(this);
        }


        public void Handle(OpenSubredditEvent message)
        {
            var vm = subredditViewModelFactory.CreateSubredditViewModel();
            vm.Subreddit = message.Subreddit;
            vm.SortMode = message.SortMode;
            this.ActivateItem(vm);
        }

    }

    public interface ISubredditViewModelFactory
    {
        SubredditViewModel CreateSubredditViewModel();
    }

}
