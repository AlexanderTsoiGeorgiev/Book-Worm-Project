namespace BookWorm.Common
{
    public static class GeneralApplicationConstants
    {
        public const int ReleaseYear = 2023;

        public const int PoemsPerPageDefault = 9;
        public const int CurrentPageDefault = 1;

        public const string AdminRoleName = "Admin";
        public const string AdminPassword = "123456";
        public const string AdminUserEmail = "admin@bookworm.com";

        public const string ModeratorRoleName = "Moderator";
        public const string Moderator1UserEmail = "moderator1@bookworm.com";
        public const string Moderator2UserEmail = "moderator2@bookworm.com";

        public const string AdminAreaName = "Admin";
        public const string ForumAreaName = "Forum";

        public const string PoemUserCache = "PoemUserCache";
        public const string PoemMineCache = "PoemMineCache";
        public const string BookUserCache = "BookUserCache";
        public const string BookMineCache = "BookMineCache";
        public const string ReviewUserCache = "ReviewUserCache";
        public const string ReviewMineCache = "ReviewMineCache";
        public const string ArticleUserCache = "ArticleUserCache";
        public const string ArticleMineCache = "ArticleMineCache";
        public const int CacheExpirationMinutes = 5;
    }
}
