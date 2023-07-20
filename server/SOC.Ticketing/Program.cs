using SOC.Conductor.Client.Generated;
//using SOC.Ticketing;
using SOC.Ticketing.Services;


TicketingService ticketingService = new TicketingService();
InputCreateAlert inputCreateAlert = new InputCreateAlert();
inputCreateAlert.Title = "alert";
inputCreateAlert.Description = "desc1";
inputCreateAlert.Type = "string";
inputCreateAlert.Source = "string";
inputCreateAlert.SourceRef = "string";

await ticketingService.CreateAlertAsync(inputCreateAlert);

//var a = await ticketingService.GetAlertAsync("~4116592");
//Console.WriteLine(a);

//var b = await ticketingService.DeleteAlertAsync("~4116592");
//Console.WriteLine(b);
/*
InputUpdateAlert inputUpdateAlert = new InputUpdateAlert();
inputUpdateAlert.Title = "alert";
inputUpdateAlert.Description = "desc1";
inputUpdateAlert.Type = "string";
inputUpdateAlert.Source = "aaaaaaaaaaaaa";
inputUpdateAlert.SourceRef = "string";

await ticketingService.UpdateAlertAsync("~4411528", inputUpdateAlert);*/