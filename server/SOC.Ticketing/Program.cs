using SOC.Conductor.Client.Generated;
//using SOC.Ticketing;
using SOC.Ticketing.Services;


TicketingService ticketingService = new TicketingService();
InputCreateCase inputCreateCase = new InputCreateCase()
{
    Title = "Test",
    Description = "Test"
   /* CustomFields = new Dictionary<string, object>
    {
        { "Field1", "Value1" }, // Prilagodite ključeve i vrednosti prema vašim potrebama.
        { "Field2", 123 },
        { "Field3", true },
        { "Field4", DateTime.Now },
        // Dodajte ostala prilagođena polja i njihove vrednosti.
    }*/
};


//await ticketingService.CreateCaseAsync(inputCreateCase);
//await ticketingService.GetCaseAsync("~8114408");

InputUpdateCase inputUpdateCase = new InputUpdateCase()
{
    Title = "Test2",
    Description = "Test2"
};

//await ticketingService.UpdateCaseAsync("~8114408", inputUpdateCase);
var b = await ticketingService.DeleteCaseAsync("~8114408");

/*InputCreateAlert inputCreateAlert = new InputCreateAlert();
inputCreateAlert.Title = "alert11";
inputCreateAlert.Description = "desc1";
inputCreateAlert.Type = "string";
inputCreateAlert.Source = "string";
inputCreateAlert.SourceRef = "string";

await ticketingService.CreateAlertAsync(inputCreateAlert);*/

//var a = await ticketingService.GetAlertAsync("~4116592");
//Console.WriteLine(a);

//var b = await ticketingService.DeleteAlertAsync("~4116592");
//Console.WriteLine(b);

/*InputUpdateAlert inputUpdateAlert = new InputUpdateAlert();
inputUpdateAlert.Title = "alert";
inputUpdateAlert.Description = "desc1";
inputUpdateAlert.Type = "string";
inputUpdateAlert.Source = "aaaaaaaaaaaaa";
inputUpdateAlert.SourceRef = "string";

await ticketingService.UpdateAlertAsync("~4411528", inputUpdateAlert);*/