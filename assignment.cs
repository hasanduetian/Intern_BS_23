using System;
using System.Collections.Generic;

namespace FareEngineAssessment
{
    public enum TripStatus {Pending, Paid, Failed}

    public class InvalidTripException : Exception
    {
        public InvalidTripException(string message) : base(message){ }
    }

    public interface IPromotion
    {
        decimal ApplyDiscount(decimal currentFare);
    }

    public interface IPaymentService
    {
        bool ProcessPayment(string passengerId, decimal amount);
    }

    public record Passenger(string PassengerId, string Name);

    public abstract class Vehicle
    {
        public string LicensePlate { get; private set; }
        public decimal BaseFare { get; private set; }

        public abstract decimal PerKmRate { get; }
        public abstract decimal PerMinuteRate { get; }

        protected Vehicle(string licensePlate, decimal baseFare)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
                throw new ArgumentException("License plate cannot be empty.");

            if (baseFare < 0)
                throw new ArgumentException("Base fare cannot be negative.");

            LicensePlate = licensePlate;
            BaseFare = baseFare;
        }

        public virtual decimal CalculateFare(decimal distanceKms, int durationMinutes)
        {
            decimal fare =
                BaseFare +
                (distanceKms * PerKmRate) +
                (durationMinutes * PerMinuteRate);

            return fare;
        }
    }

    public class StandardCar : Vehicle
    {
        public override decimal PerKmRate => 10m;
        public override decimal PerMinuteRate => 2m;

        public StandardCar(string licensePlate, decimal baseFare)
            : base(licensePlate, baseFare){ }
    }

    public class LuxurySedan : Vehicle
    { public decimal LuxuryTax { get; private set; }

        public override decimal PerKmRate => 20m;
        public override decimal PerMinuteRate => 5m;

        public LuxurySedan(string licensePlate, decimal baseFare, decimal luxuryTax)
            : base(licensePlate, baseFare)
        {
            if (luxuryTax < 0)
                throw new ArgumentException("Luxury tax cannot be negative.");

            LuxuryTax = luxuryTax;
        }

        public override decimal CalculateFare(decimal distanceKms, int durationMinutes)
        {
            decimal fare = base.CalculateFare(distanceKms, durationMinutes);
            return fare + LuxuryTax;
        }
    }

    public class PercentageDiscount : IPromotion
    {
        public decimal Percentage { get; private set; }
        public PercentageDiscount(decimal percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentException("Percentage must be between 0 and 100.");
            Percentage = percentage;
        }

        public decimal ApplyDiscount(decimal currentFare)
        {
            decimal discount = currentFare * (Percentage / 100m);
            return currentFare - discount;
        }
    }
    public class FlatDiscount : IPromotion
    {
        public decimal DiscountAmount { get; private set; }
        public FlatDiscount(decimal discountAmount)
        {
            if (discountAmount < 0)
                throw new ArgumentException("Discount amount cannot be negative.");
            DiscountAmount = discountAmount;
        }

        public decimal ApplyDiscount(decimal currentFare)
        {
            return currentFare - DiscountAmount;
        }
    }

    public class CreditCardPaymentService : IPaymentService
    {
        public bool ProcessPayment(string passengerId, decimal amount)
        {
            Console.WriteLine($"Processing payment for Passenger ID: {passengerId}");
            Console.WriteLine($"Amount: {amount:C}");

            return true;
        }
    }

    public class Trip
    {
        public Vehicle Vehicle { get; private set; }
        public Passenger Passenger { get; private set; }
        public decimal DistanceKms { get; private set; }
        public int DurationMinutes { get; private set; }
        public TripStatus Status { get; private set; }
        public IPromotion? Promotion { get; private set; }
        public Trip(
            Vehicle vehicle,
            Passenger passenger,
            decimal distanceKms,
            int durationMinutes,
            IPromotion? promotion = null)
        {
            Vehicle = vehicle ?? throw new InvalidTripException("Vehicle cannot be null.");
            Passenger = passenger ?? throw new InvalidTripException("Passenger cannot be null.");

            if (distanceKms < 0)
                throw new InvalidTripException("Distance cannot be negative.");

            if (durationMinutes <= 0)
                throw new InvalidTripException("Duration must be greater than zero.");

            DistanceKms = distanceKms;
            DurationMinutes = durationMinutes;
            Promotion = promotion;
            Status = TripStatus.Pending;
        }
        public decimal CalculateFinalFare()
        {
            if (Vehicle == null)
                throw new InvalidOperationException("No vehicle assigned.");
            decimal fare = Vehicle.CalculateFare(DistanceKms, DurationMinutes);
            if (Promotion != null)
            {
                fare = Promotion.ApplyDiscount(fare);
            }
            if (fare < Vehicle.BaseFare)
            {
                fare = Vehicle.BaseFare;
            }
            return fare;
        }

        public void CompleteTrip(IPaymentService paymentService)
        {
            if (paymentService == null)
                throw new ArgumentNullException(nameof(paymentService));
            decimal finalFare = CalculateFinalFare();
            bool paymentSuccess =
                paymentService.ProcessPayment(
                    Passenger.PassengerId,
                    finalFare);
            Status = paymentSuccess
                ? TripStatus.Paid
                : TripStatus.Failed;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Passenger passenger =
                    new Passenger("P101", "Hasan");
                Vehicle standardCar =
                    new StandardCar("DHAKA-1234", 50m);
                Vehicle luxurySedan =
                    new LuxurySedan("DHAKA-9999", 100m, 80m);
                IPromotion percentagePromo =
                    new PercentageDiscount(10);
                IPromotion flatPromo =
                    new FlatDiscount(50);

                IPaymentService paymentService =
                    new CreditCardPaymentService();
                Trip standardTrip =
                    new Trip(standardCar,passenger,12, 20, percentagePromo);

                decimal standardFare =
                    standardTrip.CalculateFinalFare();

                Console.WriteLine("STANDARD TRIP -----------");
                Console.WriteLine($"Final Fare: {standardFare:C}");

                standardTrip.CompleteTrip(paymentService);

                Console.WriteLine($"Trip Status: {standardTrip.Status}");
                Console.WriteLine();

                Trip luxuryTrip =
                    new Trip( luxurySedan,passenger,15,30, flatPromo);

                decimal luxuryFare =
                    luxuryTrip.CalculateFinalFare();

                Console.WriteLine(" LUXURY TRIP--------");
                Console.WriteLine($"Final Fare: {luxuryFare:C}");

                luxuryTrip.CompleteTrip(paymentService);

                Console.WriteLine($"Trip Status: {luxuryTrip.Status}");
                Console.WriteLine();

                Console.WriteLine("INVALID TRIP TEST ------------");

                Trip invalidTrip =
                    new Trip(standardCar,passenger,-5,10);
            }
            catch (InvalidTripException ex)
            {
                Console.WriteLine($"Invalid Trip Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
        }
    }
}