namespace BookWorm.Data.Common
{
    //TODO: Change data types to ushort/byte where possible
    public static class DataModelsValidationConstants
    {
        public static class PoemValidationConstants
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 100;

            public const int DescriptionMinLength = 30;
            public const int DescriptionMaxLength = 150;

            public const int ContentMinLenght = 75;
            public const int ContentMaxLenght = 6_500;
        }

        public static class BookValidationConstants
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 200;

            public const int DescriptionMinLength = 50;
            public const int DescriptionMaxLength = 500;

            public const int ImageUrlMinLenght = 5;
            public const int ImageUrlMaxLenght = 2_048;

            public const int MinQuantity = 1;
            public const int MaxQuantity = 1_000_000;

            public const string MinPriceAsString = "10";
            public const string MaxPriceAsString = "1000";
        }

        public static class ReviewValidationConstants
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 200;

            public const int ContentMinLength = 100;
            public const int ContentMaxLength = 100_000;

            public const float MinRating = 0.00f;
            public const float MaxRating = 10.00f;
        }


        //Might be unnecessary as only admins should be able to create new categories
        public static class CategoryValidationConstants
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class ArticleValidationConstants
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 200;

            public const int ContentMinLength = 100;
            public const int ContentMaxLength = 100_000;
        }

        public static class ForumPostValidationConstants
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 100;

            public const int ContentMinLength = 30;
            public const int ContentMaxLength = 1_000;
        }

        public static class TagValidationConstants
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class ReplyValidationConstants
        {
            public const int ContentMinLength = 1;
            public const int ContentMaxLength = 500;
        }

        public static class UserValidationConstant
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 150;

            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 150;
        }
    }
}
