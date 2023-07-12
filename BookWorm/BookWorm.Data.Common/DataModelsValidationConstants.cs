﻿namespace BookWorm.Data.Common
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

            public const int ConetntMinLength = 75;
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

            public const int MinQuantity = 0;
            public const int MaxQuantity = 1_000_000;

            public const decimal MinPrice = 10.00m;
            public const decimal MaxPrice = 1_000.00m;

        }
    }
}
