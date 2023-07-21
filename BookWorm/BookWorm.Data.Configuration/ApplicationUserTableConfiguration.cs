namespace BookWorm.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BookWorm.Data.Models;
    using static BookWorm.Data.Common.AuthorIds;

    public class ApplicationUserTableConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(SeedUsers());
        }

        private static ApplicationUser[] SeedUsers()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            ApplicationUser user;

            user = new ApplicationUser()
            {
                Id = Guid.Parse(EdgarAllanPoeId),
                UserName = "edgar.allan.poe@bookworm.com",
                NormalizedUserName = "edgar.allan.poe@bookworm.com".ToUpper(),
                Email = "edgar.allan.poe@bookworm.com",
                NormalizedEmail = "edgar.allan.poe@bookworm.com".ToUpper(),
                EmailConfirmed = false,
                PasswordHash = "AQAAAAEAACcQAAAAEDTAR8wcodXIl9pQ8QGZeDMNYS4otxanV+OPfZivLuOFrMwvUi32SrUHYAQuiPIVSw==",
                SecurityStamp = "MNGCJM2MITII262L7SZTKNPY7PRPJOH2",
                ConcurrencyStamp = "a4276e99-7e24-486f-b71c-7ce187505e19",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
            users.Add(user);

            user = new ApplicationUser()
            {
                Id = Guid.Parse(WilliamShakespeareId),
                UserName = "william.shakespeare@bookworm.com",
                NormalizedUserName = "william.shakespeare@bookworm.com".ToUpper(),
                Email = "william.shakespeare@bookworm.com",
                NormalizedEmail = "william.shakespeare@bookworm.com".ToUpper(),
                EmailConfirmed = false,
                PasswordHash = "AQAAAAEAACcQAAAAEBGdXj3rAQCeGfRHE3d4NrOwN1TSBHbOD803OIq3iPnMFTGt5+YhvTiwW+0EVzvHdQ==",
                SecurityStamp = "3BZWTPUWLSQAUOZXXWD6IXNCXDZM5R7O",
                ConcurrencyStamp = "bdc117b5-2aa2-46a1-b7f0-3d577cf0d11c",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
            users.Add(user);

            user = new ApplicationUser()
            {
                Id = Guid.Parse(EmilyDickinsonId),
                UserName = "emily.dickinson@bookworm.com",
                NormalizedUserName = "emily.dickinson@bookworm.com".ToUpper(),
                Email = "emily.dickinson@bookworm.com",
                NormalizedEmail = "emily.dickinson@bookworm.com".ToUpper(),
                EmailConfirmed = false,
                PasswordHash = "AQAAAAEAACcQAAAAEG/paiLvb+6WVLvIc7m9Vf+zmEqmDkm1x6MDLq9/Zlf6uQbI7uoEIRUwOTlJd1vj8w==",
                SecurityStamp = "QPUH2L4RXOISAQTVHN5GQAAIUTQA24BT",
                ConcurrencyStamp = "e7bf6169-29b1-474f-9506-370fdd6a91fd",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
            users.Add(user);


            return users.ToArray();
        }
    }
}
