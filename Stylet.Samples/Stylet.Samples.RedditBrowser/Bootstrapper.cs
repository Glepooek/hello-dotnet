using Stylet.Samples.RedditBrowser.Pages;
using Stylet.Samples.RedditBrowser.RedditApi;
using StyletIoC;

namespace Stylet.Samples.RedditBrowser
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            base.ConfigureIoC(builder);

            builder.Bind<IRedditClient>().To<RedditClient>().InSingletonScope();
            builder.Bind<ISubredditViewModelFactory>().ToAbstractFactory();
            builder.Bind<IPostCommentsViewModelFactory>().ToAbstractFactory();
        }
    }
}
