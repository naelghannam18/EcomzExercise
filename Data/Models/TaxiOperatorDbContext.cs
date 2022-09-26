using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class TaxiOperatorDbContext : DbContext
    {
        public TaxiOperatorDbContext()
        {
        }

        public TaxiOperatorDbContext(DbContextOptions<TaxiOperatorDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Cab> Cabs { get; set; }
        public virtual DbSet<CarModel> CarModels { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Cupon> Cupons { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Pricing> Pricings { get; set; }
        public virtual DbSet<Ride> Rides { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=TaxiOperatorDb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.HasIndex(e => e.AddressTypeId, "IX_Address_AddressTypeId");

                entity.HasIndex(e => e.CityId, "IX_Address_CityId");

                entity.HasIndex(e => e.CustomerId, "IX_Address_CustomerId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressStreetName)
                    .IsRequired()
                    .HasColumnName("address_street_name");

                entity.Property(e => e.AddressStreetNumber).HasColumnName("address_street_number");

                entity.Property(e => e.AddressZipPostal).HasColumnName("address_zip_postal");

                entity.HasOne(d => d.AddressType)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.AddressTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.ToTable("Address_Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressTypeDescription)
                    .HasMaxLength(10)
                    .HasColumnName("address_type_description");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdminEmail)
                    .IsRequired()
                    .HasColumnName("admin_email");

                entity.Property(e => e.AdminFirstName)
                    .IsRequired()
                    .HasColumnName("admin_first_name");

                entity.Property(e => e.AdminIsLocked).HasColumnName("admin_is_locked");

                entity.Property(e => e.AdminLastName)
                    .IsRequired()
                    .HasColumnName("admin_last_name");

                entity.Property(e => e.AdminLoginToken).HasColumnName("admin_login_token");

                entity.Property(e => e.AdminLoginTokenExpiry).HasColumnName("admin_login_token_expiry");

                entity.Property(e => e.AdminPassword)
                    .IsRequired()
                    .HasColumnName("admin_password");

                entity.Property(e => e.AdminRoleName)
                    .IsRequired()
                    .HasColumnName("admin_role_name");
            });

            modelBuilder.Entity<Cab>(entity =>
            {
                entity.ToTable("Cab");

                entity.HasIndex(e => e.CarModelId, "IX_Cab_CarModelId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CabIsActive).HasColumnName("cab_is_active");

                entity.Property(e => e.LicensePlate)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("license_plate");

                entity.HasOne(d => d.CarModel)
                    .WithMany(p => p.Cabs)
                    .HasForeignKey(d => d.CarModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CarModel>(entity =>
            {
                entity.ToTable("Car_Model");

                entity.Property(e => e.ModelDescription)
                    .IsRequired()
                    .HasColumnName("model_description");

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("model_name");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.HasIndex(e => e.CountryId, "IX_City_CountryId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("city_name");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryInitials)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("country_initials");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasColumnName("country_name");
            });

            modelBuilder.Entity<Cupon>(entity =>
            {
                entity.ToTable("Cupon");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CuponCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("cupon_code");

                entity.Property(e => e.CuponCustomerId).HasColumnName("cupon_customer_id");

                entity.Property(e => e.CuponDateExpiry).HasColumnName("cupon_date_expiry");

                entity.Property(e => e.CuponDateIssued).HasColumnName("cupon_date_issued");

                entity.Property(e => e.CuponDiscount).HasColumnName("cupon_discount");

                entity.HasOne(d => d.CuponCustomer)
                    .WithMany(p => p.Cupons)
                    .HasForeignKey(d => d.CuponCustomerId)
                    .HasConstraintName("FK_cupon_Customer");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.CustomerEmail, "IX_Customer_customer_email")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CustomerAccountDisabled).HasColumnName("customer_account_disabled");

                entity.Property(e => e.CustomerDob).HasColumnName("customer_dob");

                entity.Property(e => e.CustomerEmail)
                    .IsRequired()
                    .HasColumnName("customer_email");

                entity.Property(e => e.CustomerFailedLogins).HasColumnName("customer_failed_logins");

                entity.Property(e => e.CustomerFirstName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("customer_first_name");

                entity.Property(e => e.CustomerGender)
                    .IsRequired()
                    .HasColumnName("customer_gender");

                entity.Property(e => e.CustomerLastLogin).HasColumnName("customer_last_login");

                entity.Property(e => e.CustomerLastName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("customer_last_name");

                entity.Property(e => e.CustomerLoginToken).HasColumnName("customer_login_token");

                entity.Property(e => e.CustomerLoginTokenExpiry).HasColumnName("customer_login_token_expiry");

                entity.Property(e => e.CustomerPassword)
                    .IsRequired()
                    .HasColumnName("customer_password");

                entity.Property(e => e.CustomerPoints)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("customer_points");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.ToTable("Driver");

                entity.HasIndex(e => e.DriverEmail, "IX_Driver_driver_email")
                    .IsUnique();

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.DriverAccountDisabled).HasColumnName("driver_account_disabled");

                entity.Property(e => e.DriverDob).HasColumnName("driver_dob");

                entity.Property(e => e.DriverEmail)
                    .IsRequired()
                    .HasColumnName("driver_email");

                entity.Property(e => e.DriverFailedLogins).HasColumnName("driver_failed_logins");

                entity.Property(e => e.DriverFirstName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("driver_first_name");

                entity.Property(e => e.DriverIsActive).HasColumnName("driver_is_active");

                entity.Property(e => e.DriverLastLogin).HasColumnName("driver_last_login");

                entity.Property(e => e.DriverLastName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("driver_last_name");

                entity.Property(e => e.DriverLoginToken).HasColumnName("driver_login_token");

                entity.Property(e => e.DriverLoginTokenExpiry).HasColumnName("driver_login_token_expiry");

                entity.Property(e => e.DriverPassword)
                    .IsRequired()
                    .HasColumnName("driver_password");

                entity.Property(e => e.DriverUsername)
                    .IsRequired()
                    .HasColumnName("driver_username");

                entity.Property(e => e.DrivingLicenseExpiry).HasColumnName("driving_license_expiry");

                entity.Property(e => e.DrivingLicenseNumber)
                    .IsRequired()
                    .HasColumnName("driving_license_number");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phone_number");
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("Payment_Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PaymentTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("payment_type_name");
            });

            modelBuilder.Entity<Pricing>(entity =>
            {
                entity.ToTable("Pricing");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PricingName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("pricing_name");

                entity.Property(e => e.PricingPerKm)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("pricing_per_km");
            });

            modelBuilder.Entity<Ride>(entity =>
            {
                entity.ToTable("ride");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RideCanceled).HasColumnName("ride_canceled");

                entity.Property(e => e.RideCuponId).HasColumnName("ride_cupon_id");

                entity.Property(e => e.RideCustomerId).HasColumnName("ride_customer_id");

                entity.Property(e => e.RideDestinationAddress).HasColumnName("ride_destination_address");

                entity.Property(e => e.RideDistance)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ride_distance");

                entity.Property(e => e.RideDone).HasColumnName("ride_done");

                entity.Property(e => e.RideEndTime).HasColumnName("ride_end_time");

                entity.Property(e => e.RideEndingLatitude)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("ride_ending_latitude");

                entity.Property(e => e.RideEndingLongitude)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("ride_ending_longitude");

                entity.Property(e => e.RidePaymentType).HasColumnName("ride_payment_type");

                entity.Property(e => e.RidePrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ride_price");

                entity.Property(e => e.RidePricingId).HasColumnName("ride_pricing_id");

                entity.Property(e => e.RideRewardPoints).HasColumnName("ride_reward_points");

                entity.Property(e => e.RideShiftId).HasColumnName("ride_shift_id");

                entity.Property(e => e.RideStartTime).HasColumnName("ride_start_time");

                entity.Property(e => e.RideStartingAddress).HasColumnName("ride_starting_address");

                entity.Property(e => e.RideStartingLatitude)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("ride_starting_latitude");

                entity.Property(e => e.RideStartingLongitude)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("ride_starting_longitude");

                entity.HasOne(d => d.RideCupon)
                    .WithMany(p => p.Rides)
                    .HasForeignKey(d => d.RideCuponId)
                    .HasConstraintName("FK_ride_cupon_cuponId");

                entity.HasOne(d => d.RideCustomer)
                    .WithMany(p => p.Rides)
                    .HasForeignKey(d => d.RideCustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ride_customer_customerId");

                entity.HasOne(d => d.RideDestinationAddressNavigation)
                    .WithMany(p => p.RideRideDestinationAddressNavigations)
                    .HasForeignKey(d => d.RideDestinationAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ride_address_addressid_destination");

                entity.HasOne(d => d.RidePaymentTypeNavigation)
                    .WithMany(p => p.Rides)
                    .HasForeignKey(d => d.RidePaymentType)
                    .HasConstraintName("FK_ride_paymentType_paymentTypeid");

                entity.HasOne(d => d.RidePricing)
                    .WithMany(p => p.Rides)
                    .HasForeignKey(d => d.RidePricingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ride_pricing_pricingId");

                entity.HasOne(d => d.RideShift)
                    .WithMany(p => p.Rides)
                    .HasForeignKey(d => d.RideShiftId)
                    .HasConstraintName("FK_ride_shift_shiftid");

                entity.HasOne(d => d.RideStartingAddressNavigation)
                    .WithMany(p => p.RideRideStartingAddressNavigations)
                    .HasForeignKey(d => d.RideStartingAddress)
                    .HasConstraintName("FK_ride_address_addressId_starting");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.ToTable("Shift");

                entity.HasIndex(e => e.ShiftCabId, "IX_Shift_CabId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.ShiftCabId).HasColumnName("shift_cab_id");

                entity.Property(e => e.ShiftEnd).HasColumnName("shift_end");

                entity.Property(e => e.ShiftIsActive).HasColumnName("shift_is_active");

                entity.Property(e => e.ShiftIsAvailable).HasColumnName("shift_is_available");

                entity.Property(e => e.ShiftLatitude)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("shift_latitude");

                entity.Property(e => e.ShiftLoginTime).HasColumnName("shift_login_time");

                entity.Property(e => e.ShiftLogoutTime).HasColumnName("shift_logout_time");

                entity.Property(e => e.ShiftLongitude)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("shift_longitude");

                entity.Property(e => e.ShiftStart).HasColumnName("shift_start");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Shifts)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_Shift_Driver_Driver_id");

                entity.HasOne(d => d.ShiftCab)
                    .WithMany(p => p.Shifts)
                    .HasForeignKey(d => d.ShiftCabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shift_Cab_CabId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
