using Movierama.Server.Models;

namespace Movierama.Server.Views.Home
{
    public static class ReviewActionHiddenAttributeExtension
    {
        public static string HiddenAttributeFor(string element, ReviewOpinion opinion)
        {
            bool shouldBeHidden = false;

            switch (element)
            {
                case "Hate":
                case "Like":
                    shouldBeHidden = opinion != ReviewOpinion.Neutral;
                    break;

                case "Unhate":
                case "Hate_Description":
                    shouldBeHidden = opinion != ReviewOpinion.Hate;
                    break;

                case "Unlike":
                case "Like_Description":
                    shouldBeHidden = opinion != ReviewOpinion.Like;
                    break;
            }

            if (shouldBeHidden)
                return "hidden";

            return string.Empty;
        }
    }
}
