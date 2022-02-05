using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TravelAgencyApp.Models
{
    public partial class Travel_AgencyContext : DbContext
    {
        public Travel_AgencyContext()
        {
        }

        public Travel_AgencyContext(DbContextOptions<Travel_AgencyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Destinations> Destinations { get; set; }
        public virtual DbSet<PlacesEnroute> PlacesEnroute { get; set; }
        public virtual DbSet<PlananddestinationDetails> PlananddestinationDetails { get; set; }
        public virtual DbSet<Transportation> Transportation { get; set; }
        public virtual DbSet<TravelPeriod> TravelPeriod { get; set; }
        public virtual DbSet<TravelPlans> TravelPlans { get; set; }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source= RISVANASHERIN\\SQLEXPRESS; Initial Catalog= Travel_Agency; Integrated security=True");
            }
        }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Destinations>(entity =>
            {
                entity.HasKey(e => e.DestinationId)
                    .HasName("PK__destinat__55015391E54FAD97");

                entity.ToTable("destinations");

                entity.Property(e => e.DestinationId).HasColumnName("destination_id");

                entity.Property(e => e.DestinationName)
                    .IsRequired()
                    .HasColumnName("destination_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PlacesEnroute>(entity =>
            {
                entity.HasKey(e => e.PlaceId)
                    .HasName("PK__places_e__BF2B684A0D8D6FD3");

                entity.ToTable("places_enroute");

                entity.Property(e => e.PlaceId).HasColumnName("place_id");

                entity.Property(e => e.DestinationId).HasColumnName("destination_id");

                entity.Property(e => e.PlaceName)
                    .IsRequired()
                    .HasColumnName("place_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.PlacesEnroute)
                    .HasForeignKey(d => d.DestinationId)
                    .HasConstraintName("FK__places_en__desti__403A8C7D");
            });

            modelBuilder.Entity<PlananddestinationDetails>(entity =>
            {
                entity.HasKey(e => e.PId)
                    .HasName("PK__planandd__82E06B91CC18A5D3");

                entity.ToTable("plananddestination_details");

                entity.Property(e => e.PId)
                    .HasColumnName("p_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DestinationId).HasColumnName("destination_id");

                entity.Property(e => e.PlanId).HasColumnName("plan_id");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.PlananddestinationDetails)
                    .HasForeignKey(d => d.DestinationId)
                    .HasConstraintName("FK__planandde__desti__4E88ABD4");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.PlananddestinationDetails)
                    .HasForeignKey(d => d.PlanId)
                    .HasConstraintName("FK__planandde__plan___4D94879B");
            });

            modelBuilder.Entity<Transportation>(entity =>
            {
                entity.ToTable("transportation");

                entity.Property(e => e.TransportationId).HasColumnName("transportation_id");

                entity.Property(e => e.TransportationName)
                    .IsRequired()
                    .HasColumnName("transportation_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VehicleChargePerDay).HasColumnName("Vehicle_charge_per_day");
            });

            modelBuilder.Entity<TravelPeriod>(entity =>
            {
                entity.HasKey(e => e.PeriodId)
                    .HasName("PK__travel_p__2323EE447EFD5131");

                entity.ToTable("travel_period");

                entity.Property(e => e.PeriodId).HasColumnName("period_id");

                entity.Property(e => e.PeriodOfDays).HasColumnName("period_of_days");

                entity.Property(e => e.PlanId).HasColumnName("plan_id");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.TravelPeriod)
                    .HasForeignKey(d => d.PlanId)
                    .HasConstraintName("FK__travel_pe__plan___4316F928");
            });

            modelBuilder.Entity<TravelPlans>(entity =>
            {
                entity.HasKey(e => e.PlanId)
                    .HasName("PK__travel_p__BE9F8F1DBFFB9850");

                entity.ToTable("travel_plans");

                entity.Property(e => e.PlanId).HasColumnName("plan_id");

                entity.Property(e => e.PlanName)
                    .IsRequired()
                    .HasColumnName("plan_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PricePerDay).HasColumnName("price_per_day");

                entity.Property(e => e.TransportationId).HasColumnName("transportation_id");

                entity.HasOne(d => d.Transportation)
                    .WithMany(p => p.TravelPlans)
                    .HasForeignKey(d => d.TransportationId)
                    .HasConstraintName("FK__travel_pl__trans__3D5E1FD2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
