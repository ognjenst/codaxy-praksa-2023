using SOC.Conductor.Client.Generated;
//using SOC.Ticketing;
using SOC.Ticketing.Services;


TicketingService ticketingService = new TicketingService();
InputCreateCase inputCreateCase = new InputCreateCase()
{
    Title = "Test",
    Description = "Test",
    CustomFields = new Dictionary<string, object>
    {
        { "Field1", "Value1" }, // Prilagodite ključeve i vrednosti prema vašim potrebama.
        { "Field2", 123 },
        { "Field3", true },
        { "Field4", DateTime.Now },
        // Dodajte ostala prilagođena polja i njihove vrednosti.
    }
};


await ticketingService.CreateCaseAsync(inputCreateCase);

