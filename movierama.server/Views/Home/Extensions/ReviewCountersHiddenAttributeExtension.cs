using Movierama.Server.Models;

namespace Movierama.Server.Views.Home
{
    public static class ReviewCountersHiddenAttributeExtension
    {
        public static string HiddenAttributeFor(ReviewOpinion opinion, bool userIsAuthenticated, bool canReview)
        {
            // non authenticated users cannot vote, so show the counters
            if (!userIsAuthenticated || !canReview)
                return string.Empty;

            if (opinion == ReviewOpinion.Neutral)
                return "hidden";

            return string.Empty;
        }
    }
}
