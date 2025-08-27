public class Booking
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required int NumberOfTickets { get; set; }
    public required DateTime BookingDate { get; set; }
    public required int MovieShowId { get; set; }
}
