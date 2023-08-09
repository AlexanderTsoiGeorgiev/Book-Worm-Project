namespace BookWorm.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BookWorm.Data.Models;
    using static BookWorm.Data.Common.AuthorIds;
    using static BookWorm.Data.Common.StaffIds;
    using static BookWorm.Common.GeneralApplicationConstants;

    public class ApplicationUserTableConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .Property(au => au.FirstName)
                .HasDefaultValue("Test");

            builder
                .Property(au => au.LastName)
                .HasDefaultValue("Test");

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
                AccessFailedCount = 0,
                FirstName = "Edgar",
                LastName = "Poe"
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
                AccessFailedCount = 0,
                FirstName = "William",
                LastName = "Shakespeare"
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
                AccessFailedCount = 0,
                FirstName = "Emily",
                LastName = "Dickinson"
            };
            users.Add(user);


            //Admin User
            user = new ApplicationUser()
            {
                Id = Guid.Parse(AdminId),
                UserName = AdminUserEmail,
                NormalizedUserName = AdminUserEmail.ToUpper(),
                Email = AdminUserEmail,
                NormalizedEmail = AdminUserEmail.ToUpper(),
                EmailConfirmed = false,
                PasswordHash = "AQAAAAEAACcQAAAAEFAdC3im6xmdFIsf+MsIcopBuSNFz2HU15zaS5SjNu1VMUdSHfYa/iF0NxxiqNA9uw==",
                SecurityStamp = "T4DMIR6DSDAR5R7QTFMX6MR6QSOXGGHH",
                ConcurrencyStamp = "f13caeaf-6cbe-4608-8b5b-4434d2d282fd",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                FirstName = "Admin",
                LastName = "User"
            };
            users.Add(user);


            //Moderator1 User
            user = new ApplicationUser()
            {
                Id = Guid.Parse(Moderator1Id),
                UserName = Moderator1UserEmail,
                NormalizedUserName = Moderator1UserEmail.ToUpper(),
                Email = Moderator1UserEmail,
                NormalizedEmail = Moderator1UserEmail.ToUpper(),
                EmailConfirmed = false,
                PasswordHash = "AQAAAAEAACcQAAAAEMajAP3maoVrRPWh5JTafBJE4KzjiKWZoF9LQd6/+xmPFuATvKwZbxIg0+i5mi15mg==",
                SecurityStamp = "AVBJCLXQD6IX5FDGTE37SHAZGVJWUJVT",
                ConcurrencyStamp = "cfb269b1-7833-439e-aa6e-f1f9a3fc484d",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                FirstName = "Moderator",
                LastName = "User"
            };
            users.Add(user);


            //Moderator2 User
            user = new ApplicationUser()
            {
                Id = Guid.Parse(Moderator2Id),
                UserName = Moderator2UserEmail,
                NormalizedUserName = Moderator2UserEmail.ToUpper(),
                Email = Moderator2UserEmail,
                NormalizedEmail = Moderator2UserEmail.ToUpper(),
                EmailConfirmed = false,
                PasswordHash = "AQAAAAEAACcQAAAAEIvwkhYsdf6k7M0Sv27DYiRxk/kWHv0gNM3dKOEi92hpvt8/YPZZINufnQeDPzzOng==",
                SecurityStamp = "GBGKNZCL6MTHX3QHMAV4TISDHFRDBAF7",
                ConcurrencyStamp = "99567f57-06ea-41bf-9e53-6c165534fe5c",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                FirstName = "Moderator",
                LastName = "User"
            };
            users.Add(user);

            return users.ToArray();
        }
    }
}
