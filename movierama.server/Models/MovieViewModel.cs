namespace Movierama.Server.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool HasFullDescription
        {
            get
            {
                return this.Description.Length == 303 && this.Description.EndsWith("...");
            }
        }

        public string OwnerId { get; set; }

        public string OwnerFullName { get; set; }

        public int PostDuration { get; set; }

        public string UnitOfPostDuration { get; set; }

        public string PublicationDate { get; set; }

        public int LikeCount { get; set; }

        public int HateCount { get; set; }

        public bool CanReview { get; set; }

        public ReviewOpinion ReviewOpinion { get; set; }

    }
}
