using SOC.Ticketing.Generated;
using SOC.Ticketing.Services;


TicketingService ticketingService = new TicketingService();
var inputCreateAlert = new InputCreateAlert();
inputCreateAlert.Title = "alert111";
inputCreateAlert.Description = "desc1";
inputCreateAlert.Type = "string";
inputCreateAlert.Source = "string";
inputCreateAlert.SourceRef = "string";
InputCreateTask inputCreateTask = new InputCreateTask();

/*inputCreateTask.Title = "task";
inputCreateTask.Description = "desc";
//var createdAlert = await ticketingService.CreateAsync<InputCreateAlert, OutputAlert>( inputCreateAlert, "alert");

var createOutput = await ticketingService.CreateTaskAsync(inputCreateTask, "~8106216");
Console.WriteLine("id " + createOutput.Id);*/

/*var getOutput = await ticketingService.GetTaskAsync("~4214816");
Console.WriteLine(getOutput.Id);

//var b = await ticketingService.DeleteAlertAsync("~4116592");
//Console.WriteLine(b);

InputUpdateAlert inputUpdateAlert = new InputUpdateAlert();
inputUpdateAlert.Title = "alert1";
inputUpdateAlert.Description = "desc1";
inputUpdateAlert.Type = "string";
inputUpdateAlert.Source = "aaaaaaaaaaaaa";
inputUpdateAlert.SourceRef = "string";

//var updateAlert = await ticketingService.UpdateAsync<InputUpdateAlert>("~4079760", inputUpdateAlert, "alert");
//var getAlert = await ticketingService.GetAsync<OutputAlert>("~4079760", "alert");
//var deleteAlert = await ticketingService.DeleteAsync("~4079760", "alert");


// Poziv metode CreateAsync za Case
/*var caseInput = new InputCreateCase
{
    Title = "Test",
    Description = "Test"
    // Dodajte ostale atribute prema potrebi
};*/

var caseInput = new InputCreateCase
{
    Title = "Test13",
    Description = "Test12"
    // Dodajte ostale atribute prema potrebi
};

var createdCase = await ticketingService.CreateAsync<InputCreateCase, OutputCase>( caseInput, "case");

var caseUpdateInput = new InputUpdateCase
{
    Title = "Test11-update",
    Description = "Test11-update"
    // Dodajte ostale atribute prema potrebi
};

//var updatedCase = await ticketingService.UpdateAsync<InputUpdateCase>("~4120688", caseUpdateInput, "case");

//var getCase = await ticketingService.GetAsync<OutputCase>("~4120688", "case");
//var deleteCase = await ticketingService.DeleteAsync("~4120688", "case");